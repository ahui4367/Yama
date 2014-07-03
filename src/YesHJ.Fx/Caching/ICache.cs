/***************************************************************************************
 *
 * 功能说明：缓存接口
 *
 * 当前文件：ICache.cs
 *
 * 作    者：Dellinger.Zhang
 *
 * 修改版本：Alpha 13:45 2012/11/8
 *
 * @Copyright by hujiang.com
 **************************************************************************************/
namespace YesHJ.Fx.Caching
{
    using System.Collections.Generic;

    public interface ICache<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region Methods

        /// <summary>
        /// 把某项增加到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Add(TKey key, TValue value);

        /// <summary>
        /// 把某项增加到缓存中，附加有属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        bool Add(TKey key, TValue value, IDictionary<string, object> properties);

        /// <summary>
        /// 判断指定值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(TKey key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        bool FlushAll();

        /// <summary>
        /// 获取Key键对应的缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue Get(TKey key, TValue defaultValue = default(TValue));

        /// <summary>
        /// 移除某项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(TKey key);

        /// <summary>
        /// 尝试获取Key键对应的缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGet(TKey key, out TValue value);

        #endregion Methods
    }
}