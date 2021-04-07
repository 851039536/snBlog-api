using Snblog.Cache.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snblog.Cache.CacheUtil
{
    public class CacheUtil
    {

        //创建内存缓存对象
        private static CacheManager _cache = new CacheManager();
        /// <summary>
        /// 设置并返回缓存值(值类型)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="value">要缓存的值</param>
        /// <returns>result</returns>
        public T CacheNumber<T>(string key, T value)
        {
            T result = default;

            if (_cache.IsInCache(key)) //如果存在缓存取值
            {
                result = _cache.Get<T>(key);
            }
            else
            {
                if (!value.Equals(0))
                {
                _cache.Set_AbsoluteExpire<T>(key, value, _cache.time);
                }
            }
            return result;
        }

          /// <summary>
        /// 设置并返回缓存值(字符串)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键值</param>
        /// <param name="value">要缓存的值</param>
        public T CacheString<T>(string key, T value)
        {
            T result = default;
            if (_cache.IsInCache(key)) //如果存在缓存取值
            {
                result = _cache.Get<T>(key);
            }
            else
            {
                if (value != null)
                {
                    _cache.Set_AbsoluteExpire<T>(key, value, _cache.time);
                }
            }
            return result;
        }
    }
}
