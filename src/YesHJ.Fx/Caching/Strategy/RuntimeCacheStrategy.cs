#region Header

/***************************************************************************************
 *
 * 功能说明：System.Caching.Cache包装策略，提供.net内置缓存
 *
 * 当前文件：RuntimeCacheStrategy.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/

#endregion Header

namespace YesHJ.Fx.Caching.Strategy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Caching;

    using YesHJ.Fx.Extension;

    //using YesHJ.Fx.Utils;
    /// <summary>
    /// System.Caching.Cache包装策略，提供.net内置缓存
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class RuntimeCacheStrategy<TKey, TValue> : ICache<TKey, TValue>
    {
        #region Fields

        public static readonly int DEFAULT_SLIDING_EXPIRE_TIMESPAN = 5 * 60 * 1000;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 用于封装Cache类，方便以后复合型扩展
        /// </summary>
        private System.Web.Caching.Cache InnerCache
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 添加缓存项, 如果Tkey是引用型，请重载ToString方法
        /// Add方法是封装的Cache.Insert方法，默认情况是替换策略。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(TKey key, TValue value)
        {
            try
            {
                InnerCache.Insert(Convert.ToString(key), value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加缓存项, 如果Tkey是引用型，请重载ToString方法
        /// Add方法是封装的Cache.Insert方法，默认情况是替换策略。
        /// properties定义：
        /// key: expire 超时时间(s)
        ///      depObj 依赖对象 (=CacheDependency)
        ///      absDT  绝对超时时间 (=AbsoluteExpireDateTime)
        ///      sldTS  相对超时时间戳(秒) (=SlidingExpirationTimeSpan)
        ///      callback 更新或删除缓存时的回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool Add(TKey key, TValue value, IDictionary<string, object> properties)
        {
            bool result = false;
            System.Web.Caching.CacheDependency depObj = null;
            DateTime absDT = System.Web.Caching.Cache.NoAbsoluteExpiration;
            TimeSpan sldDT = System.Web.Caching.Cache.NoSlidingExpiration;
            System.Web.Caching.CacheItemUpdateCallback callback = null;

            if (properties != null)
            {
                if (properties.ContainsKey("expire"))
                {
                    depObj = properties["expire"] as System.Web.Caching.CacheDependency;
                }

                if (properties.ContainsKey("absDT"))
                {
                    //absDT = ValueParser.ParseDateTime(properties["absDT"], absDT);
                }

                if (properties.ContainsKey("sldTS"))
                {
                    int seconds =
                        properties["sldTS"].ToString().AsInteger(DEFAULT_SLIDING_EXPIRE_TIMESPAN);
                    if (seconds > 0)
                    {
                        sldDT = TimeSpan.FromSeconds(seconds);
                    }
                }

                if (properties.ContainsKey("callback"))
                {
                    callback = properties["callback"]
                        as System.Web.Caching.CacheItemUpdateCallback;
                }
            }

            if (callback == null)
                InnerCache.Insert(Convert.ToString(key),
                                  value,
                                  depObj,
                                  absDT,
                                  sldDT);
            else
                InnerCache.Insert(Convert.ToString(key),
                                  value,
                                  depObj,
                                  absDT,
                                  sldDT,
                                  callback);
            return result;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(TKey key)
        {
            return InnerCache.Get(Convert.ToString(key)) != null;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        public bool FlushAll()
        {
            bool result = true;

            IDictionaryEnumerator cacheEnum = InnerCache.GetEnumerator();
            try
            {
                while (cacheEnum.MoveNext())
                    InnerCache.Remove(cacheEnum.Key.ToString());
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 返回缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TValue Get(TKey key, TValue defaultValue = default(TValue))
        {
            var obj = InnerCache.Get(Convert.ToString(key));
            TValue result = defaultValue;

            if (obj is TValue)
            {
                result = (TValue)obj;
            }

            return result;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return InnerCache.Remove(Convert.ToString(key)) != null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return InnerCache.GetEnumerator();
        }

        /// <summary>
        /// 尝试获取缓存值，value值会被清空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet(TKey key, out TValue value)
        {
            value = default(TValue);
            value = Get(key, value);
            return value != null;
        }

        #endregion Methods
    }
}