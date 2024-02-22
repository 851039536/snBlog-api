namespace Snblog.Cache.CacheUtil
{
    public interface ICacheUtil
    {
        /// <summary>
        /// 设置并返回缓存值(值类型)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="value">要缓存的值</param>
        /// <returns>result</returns>
        public T CacheNumber1<T>(string key, T value);

        public T CacheNumber<T>(string key,T value,bool cache);
        /// <summary>
        /// 设置并返回缓存值(字符串)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="value">要缓存的值</param>
        public T CacheString1<T>(string key, T value);

        /// <summary>
        /// 设置并返回缓存值(字符串)
        /// </summary>
        /// <param name="key">缓存键值</param>
        /// <param name="value">要缓存的值</param>
        /// <param name="cache"></param>
        /// <typeparam name="T">返回类型</typeparam>
        /// <returns></returns>
        public T CacheString<T>(string key,T value,bool cache);

    }
}
