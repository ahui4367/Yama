/***************************************************************************************
 *
 * 功能说明：简单缓存类（线程安全）
 *
 * 当前文件：ThreadSafeCacheStrategy
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 17:41 2012/11/8
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching.Strategy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ThreadSafeCacheStrategy<TKey, TValue> : ICache<TKey, TValue>
    {
        #region Fields

        Hashtable _storage = Hashtable.Synchronized(new Hashtable());

        #endregion Fields

        #region Methods

        public bool Add(TKey key, TValue value)
        {
            bool result = false;

            if (key != null && !_storage.ContainsKey(key))
            {
                _storage.Add(key, value);
                result = true;
            }

            return result;
        }

        public bool Add(TKey key, TValue value, 
            IDictionary<string, object> properties)
        {
            return Add(key, value);
        }

        public bool Exists(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        public bool FlushAll()
        {
            _storage.Clear();
            return true;
        }

        public TValue Get(TKey key, TValue defaultValue = default(TValue))
        {
            TValue result = defaultValue;
            if (key != null)
            {
                TryGet(key, out result);
            }
            return result;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new ThreadSafeCacheStrategy<TKey, TValue>.Enumerator(
                _storage);
        }

        public bool Remove(TKey key)
        {
            _storage.Remove(key);
            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGet(TKey key, out TValue value)
        {
            bool result = false;

            value = default(TValue);

            if (_storage.ContainsKey(key))
            {
                value = (TValue)_storage[key];
                result = true;
            }

            return result;
        }

        #endregion Methods

        #region Nested Types

        public class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
        {
            #region Fields

            int _index = 0;
            object[] _keys;
            Hashtable _table;

            #endregion Fields

            #region Constructors

            public Enumerator(Hashtable table)
            {
                _table = table;
                _keys = new object[_table.Keys.Count];
                _table.Keys.CopyTo(_keys, 0);
            }

            #endregion Constructors

            #region Properties

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(
                        CurrentKey, (TValue)_table[CurrentKey]);
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            private TKey CurrentKey
            {
                get
                {
                    return (TKey)_keys[_index];
                }
            }

            #endregion Properties

            #region Methods

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index = _index < _table.Count ? _index + 1 : _index;

                return _index < _table.Count;
            }

            public void Reset()
            {
                this._index = 0;
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}