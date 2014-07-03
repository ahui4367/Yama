/***************************************************************************************
 *
 * 功能说明：缓存类的Provider，内置三种缓存类，并提供扩展缓存类的接口
 *
 * 当前文件：CacheProvider.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching
{
    using System;
    using System.Reflection;

    using YesHJ.Fx.Caching.Strategy;
    using YesHJ.Fx.Error;
    using YesHJ.Fx.Pattern;

    #region Delegates

    public delegate ICache<TKey, TValue> ResolveCache<TKey, TValue>(string strategyName);

    #endregion Delegates

    //#pragma warning disable 414
    public sealed class CacheProvider : Singleton<CacheProvider>
    {
        #region Fields

        public static readonly string LRU_CACHE = "lru";
        public static readonly string REDIS_CACHE = "redis";
        public static readonly string RUNTIME_CACHE = "runtime";
        public static readonly string SIMPLE_CACHE = "simple";
        public static readonly string THREADSAFE_CACHE = "threadsafe";

        static readonly ICache<string, Func<Type>> _cacheStorage = 
            new SimpleCacheStrategy<string, Func<Type>>();

        #endregion Fields

        #region Constructors

        private CacheProvider()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 添加扩展的缓存实现，需要实现ICache<TKey, TValue>接口
        /// </summary>
        /// <param name="strategyName"></param>
        /// <param name="cacheType"></param>
        public static void AddExtCacheStrategy(string strategyName, Type cacheType)
        {
            if (_cacheStorage.Exists(strategyName))
            {
                return;
            }

            if (cacheType.IsGenericType)
            {
                var parms = cacheType.GetGenericArguments();
                if (parms.Length == 2)
                {
                    _cacheStorage.Add(strategyName, () =>
                    {
                        return cacheType;
                    });
                }
            }
        }

        /// <summary>
        /// 获取缓存类实例
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        public ICache<TKey, TValue> GetCache<TKey, TValue>(string strategyName)
        {
            Assert.ThrowIfNullOrEmpty("strategyName", strategyName);

            ICache<TKey, TValue> result = null;
            //由于是内部默认支持的缓存，所以直接创建，提高性能
            if (LRU_CACHE.Equals(strategyName, StringComparison.OrdinalIgnoreCase))
            {
                result = new LruCacheStrategy<TKey, TValue>();
            } else if (SIMPLE_CACHE.Equals(strategyName, StringComparison.OrdinalIgnoreCase))
            {
                result = new SimpleCacheStrategy<TKey, TValue>();
            }
            else if (THREADSAFE_CACHE.Equals(strategyName, StringComparison.OrdinalIgnoreCase))
            {
                result = new ThreadSafeCacheStrategy<TKey, TValue>();
            }
            else if (REDIS_CACHE.Equals(strategyName, StringComparison.OrdinalIgnoreCase))
            {
                //result = new RedisCacheStrategy<TKey, TValue>();
            }
            else if (RUNTIME_CACHE.Equals(strategyName, StringComparison.OrdinalIgnoreCase))
            {
                result = new RuntimeCacheStrategy<TKey, TValue>();
            }
            else
            {
                result = RetrieveFromExt<TKey, TValue>(strategyName);
            }

            return result;
        }

        /// <summary>
        /// 如果不是默认的缓存，则在扩展中找
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="strategyName"></param>
        /// <returns></returns>
        private ICache<TKey, TValue> RetrieveFromExt<TKey, TValue>(string strategyName)
        {
            ICache<TKey, TValue> result = null;

            if (_cacheStorage.Exists(strategyName))
            {
                Type type = _cacheStorage.Get(strategyName)();
                ConstructorInfo ctor = type.GetConstructor(new Type[]{ typeof(TKey), typeof(TValue)});
                result = (ICache<TKey, TValue>)Activator.CreateInstance(
                    type.MakeGenericType(typeof(TKey), typeof(TValue)));
            }

            return result;
        }

        #endregion Methods
    }
}