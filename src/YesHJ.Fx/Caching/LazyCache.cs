/***************************************************************************************
 *
 * 功能说明：延迟加载的缓存基类
 *
 * 当前文件：LazyCache.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 12:00 2012/11/7
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 加载事件参数
    /// </summary>
    public class AcquireEventArgs<TKey, TValue> : EventArgs
    {
        #region Properties

        /// <summary>
        /// 如果Error不为空，代表获取失败
        /// </summary>
        public Exception Error
        {
            get; set;
        }

        /// <summary>
        /// 附加的参数
        /// </summary>
        public IDictionary<string, object> ExtParameters
        {
            get; set;
        }

        /// <summary>
        /// 缓存的Key
        /// </summary>
        public TKey Key
        {
            get;
            set;
        }

        /// <summary>
        /// 返回的结果
        /// </summary>
        public TValue Value
        {
            get;
            set;
        }

        #endregion Properties
    }

    /// <summary>
    /// 延迟加载的缓存基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    //#pragma warning disable 0067
    public abstract class LazyCache<TKey, TValue>
    {
        #region Fields

        ICache<TKey, TValue> _cache;

        #endregion Fields

        #region Constructors

        public LazyCache(ICache<TKey, TValue> cache)
        {
            _cache = cache;
        }

        #endregion Constructors

        #region Delegates

        public delegate void AcquireItemData(object sender, AcquireEventArgs<TKey, TValue> args);

        #endregion Delegates

        #region Events

        public event AcquireItemData OnAcquireItemData;

        #endregion Events

        #region Indexers

        /// <summary>
        /// 获得缓存的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                TValue result = default(TValue);

                TryGetValue(key, out result);

                return result;
            }
        }

        #endregion Indexers

        #region Methods

        public bool TryGetValue(TKey key, out TValue value)
        {
            bool result = false;

            value = default(TValue);

            if (!_cache.TryGet(key, out value))
            {
                var arg = new AcquireEventArgs<TKey, TValue>()
                {
                    Key = key,
                };

                OnAcquireItemData(this, arg);

                if (arg.Error == null)
                {
                    value = arg.Value;
                    result = true;
                }
            }

            return result;
        }

        #endregion Methods
    }
}