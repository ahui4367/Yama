/***************************************************************************************
 *
 * 功能说明：基于LRU算法的缓存类，高并发情况下性能不一定最佳。
 *
 * 当前文件：LruCacheStrategy.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 17:21 2012/11/8
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching.Strategy
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class LruCacheStrategy<TKey, TValue> : ICache<TKey, TValue>
    {
        #region Fields

        private readonly ReaderWriterLockSlim g_lock = new ReaderWriterLockSlim();
        private readonly Dictionary<TKey, NodeInfo> _cachedNodesDictionary = new Dictionary<TKey, NodeInfo>();
        private readonly LinkedList<NodeInfo> _lruLinkedList = new LinkedList<NodeInfo>();
        private readonly uint _maxSize;
        private readonly TimeSpan _timeOut;

        private Timer _cleanupTimer;

        #endregion Fields

        #region Constructors

        public LruCacheStrategy()
            : this(SR.DEFAULT_CACHE_EXPIRE,
                   SR.DEFAULT_CACHE_MAX_SIZE,
                   1000)
        {
        }

        public LruCacheStrategy(TimeSpan itemExpireTimeout,
            uint maxCacheSize,
            uint clearupInterval)
        {
            _timeOut = itemExpireTimeout;
            _maxSize = maxCacheSize;
            _cleanupTimer = new Timer(RemoveExpiredElements,
                new AutoResetEvent(false), 0, clearupInterval);
        }

        #endregion Constructors

        #region Methods

        public bool Add(TKey key, TValue value)
        {
            bool result = false;
            if (key == null)
            {
                result = false;
            }
            else
            {
                g_lock.EnterWriteLock();

                NodeInfo ni;
                try
                {
                    if (this._cachedNodesDictionary.TryGetValue(key, out ni))
                    {
                        this.Delete(ni);
                    }

                    this.ShrinkToSize(this._maxSize - 1);
                    this.CreateNodeandAddtoList(key, value);
                    result = true;
                }
                finally
                {
                    g_lock.ExitWriteLock();
                }
            }
            return result;
        }

        public bool Add(TKey key, TValue value, IDictionary<string, object> properties)
        {
            bool result = false;
            if (key == null)
            {
                result = false;
            }
            else
            {
                g_lock.EnterWriteLock();

                NodeInfo ni;
                try
                {
                    if (this._cachedNodesDictionary.TryGetValue(key, out ni))
                    {
                        this.Delete(ni);
                    }

                    this.ShrinkToSize(this._maxSize - 1);
                    if (properties != null && properties.ContainsKey("expire"))
                    {
                        TimeSpan timeout = (TimeSpan)properties["expire"];
                        this.CreateNodeandAddtoList(key, value,
                            timeout > DateTime.MaxValue.Subtract(DateTime.Now) ?
                                DateTime.MaxValue : DateTime.Now.Add(timeout));
                    }
                    else
                    {
                        this.CreateNodeandAddtoList(key, value);
                    }

                    result = true;
                }
                finally
                {
                    g_lock.ExitWriteLock();
                }
            }
            return result;
        }

        public bool Exists(TKey key)
        {
            NodeInfo node;
            return this._cachedNodesDictionary.TryGetValue(key, out node);
        }

        public bool FlushAll()
        {
            bool result = true;
            g_lock.EnterWriteLock();

            try
            {
                this._cachedNodesDictionary.Clear();
                this._lruLinkedList.Clear();
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                g_lock.ExitWriteLock();
            }

            return result;
        }

        public TValue Get(TKey key, TValue defaultValue = default(TValue))
        {
            TValue result = default(TValue);

            if (!TryGet(key, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            bool result = false;

            g_lock.EnterWriteLock();
            try
            {
                NodeInfo node;
                if (this._cachedNodesDictionary.TryGetValue(key, out node))
                {
                    this._lruLinkedList.Remove(node);
                    this._cachedNodesDictionary.Remove(key);
                    result = true;
                }
            }
            finally
            {
                g_lock.ExitWriteLock();
            }

            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGet(TKey key, out TValue value)
        {
            bool result = false;
            value = default(TValue);

            if (key == null)
            {
                result = false;
            }
            else
            {
                NodeInfo node;
                g_lock.EnterReadLock();

                try
                {
                    if (this._cachedNodesDictionary.TryGetValue(key, out node))
                    {
                        result = true;
                        Interlocked.Increment(ref node._accessCount);
                        value = node.Value;

                        if (node.AccessCount > 20)
                        {
                            ThreadPool.QueueUserWorkItem(this.AddBeforeFirstNode, key);
                        }
                    }
                }
                finally
                {
                    g_lock.ExitReadLock();
                }
            }

            return result;
        }

        private void AddBeforeFirstNode(object stateinfo)
        {
            g_lock.EnterWriteLock();
            try
            {
                TKey key = (TKey)stateinfo;
                NodeInfo nodeInfo;
                if (this._cachedNodesDictionary.TryGetValue(key, out nodeInfo))
                {
                    if (nodeInfo != null && !nodeInfo.IsExpired() && nodeInfo.AccessCount > 20)
                    {
                        if (nodeInfo.LLNode != this._lruLinkedList.First)
                        {
                            this._lruLinkedList.Remove(nodeInfo.LLNode);
                            nodeInfo.LLNode = this._lruLinkedList.AddBefore(this._lruLinkedList.First, nodeInfo);
                            nodeInfo.AccessCount = 0;
                        }
                    }
                }
            }
            finally
            {
                g_lock.ExitWriteLock();
            }
        }

        private void CreateNodeandAddtoList(TKey userKey, TValue cacheObject)
        {
            CreateNodeandAddtoList(userKey, cacheObject,
                (this._timeOut > DateTime.MaxValue.Subtract(DateTime.Now) ?
                    DateTime.MaxValue : DateTime.Now.Add(this._timeOut)));
        }

        private void CreateNodeandAddtoList(TKey userKey, TValue cacheObject, DateTime expire)
        {
            NodeInfo node = new NodeInfo(userKey, cacheObject, expire);

            node.LLNode = this._lruLinkedList.AddFirst(node);
            this._cachedNodesDictionary[userKey] = node;
        }

        private void Delete(NodeInfo node)
        {
            this._lruLinkedList.Remove(node.LLNode);
            this._cachedNodesDictionary.Remove(node.Key);
        }

        private void RemoveExpiredElements(object stateInfo)
        {
            g_lock.EnterWriteLock();
            try
            {
                while (this._lruLinkedList.Last != null)
                {
                    NodeInfo node = this._lruLinkedList.Last.Value;
                    if (node != null && node.IsExpired())
                    {
                        this.Delete(node);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                g_lock.ExitWriteLock();
            }
        }

        private void RemoveLeastValuableNode()
        {
            if (this._lruLinkedList.Last != null)
            {
                NodeInfo node = this._lruLinkedList.Last.Value;
                this.Delete(node);
            }
        }

        private void ShrinkToSize(uint desiredSize)
        {
            while (this._cachedNodesDictionary.Count > desiredSize)
            {
                this.RemoveLeastValuableNode();
            }
        }

        #endregion Methods

        #region Nested Types

        private class NodeInfo
        {
            #region Fields

            public int _accessCount;

            private readonly DateTime timeOutTime;

            #endregion Fields

            #region Constructors

            internal NodeInfo(TKey key, TValue value, DateTime timeouttime)
            {
                this.Key = key;
                this.Value = value;
                this.timeOutTime = timeouttime;
            }

            #endregion Constructors

            #region Properties

            internal int AccessCount
            {
                get { return _accessCount; }
                set { _accessCount = value; }
            }

            internal TKey Key
            {
                get; private set;
            }

            internal LinkedListNode<NodeInfo> LLNode
            {
                get; set;
            }

            internal TValue Value
            {
                get; private set;
            }

            #endregion Properties

            #region Methods

            internal bool IsExpired()
            {
                return DateTime.UtcNow >= this.timeOutTime;
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}