/***************************************************************************************
 *
 * 功能说明：简单缓存类(非线程安全)
 *
 * 当前文件：SimpleCacheStrategy.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 15:25 2012/11/8
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching.Strategy
{
    using System.Collections.Generic;

    public class SimpleCacheStrategy<TKey, TValue> : ICache<TKey, TValue>
    {
        #region Fields

        IDictionary<TKey, TValue> _storage;

        #endregion Fields

        #region Constructors

        public SimpleCacheStrategy()
        {
            _storage = new Dictionary<TKey, TValue>(SR.DEFAULT_CACHE_CAPACITY);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 简单添加缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 带参数添加缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool Add(TKey key, TValue value, 
            IDictionary<string, object> properties)
        {
            return Add(key, value);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public bool FlushAll()
        {
            _storage.Clear();
            return true;
        }

        /// <summary>
        /// 得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TValue Get(TKey key, TValue defaultValue = default(TValue))
        {
            TValue result = defaultValue;

            TryGet(key, out result);

            return result;
        }

        /// <summary>
        /// 迭代接口
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            _storage.Remove(key);
            return true;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        public bool TryGet(TKey key, out TValue value)
        {
            return _storage.TryGetValue(key, out value);
        }

        #endregion Methods
    }
}