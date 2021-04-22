using System;
using System.Collections.Generic;

namespace Snblog.Cache.Cache
{
    public interface ICacheManager
    {
        /// <summary>
        /// 判断是否在缓存中
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public bool IsInCache(string key);
        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllKeys();
        /// <summary>
        /// 获取所有的缓存值
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllValues<T>();
        /// <summary>
        /// 取得缓存数据
        /// </summary>
        /// <typeparam name="T">类型值</typeparam>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public T Get<T>(string key);
        /// <summary>
        /// 设置缓存(永不过期)
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">缓存值</param>
        public void Set_NotExpire<T>(string key, T value);

        /// <summary>
        /// 设置缓存(滑动过期:超过一段时间不访问就会过期,一直访问就一直不过期)
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">缓存值</param>
        /// <param name="span"></param>
        public void Set_SlidingExpire<T>(string key, T value, TimeSpan span);

        /// <summary>
        /// 设置缓存(绝对时间过期:从缓存开始持续指定的时间段后就过期,无论有没有持续的访问)
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">缓存值</param>
        /// <param name="span"></param>
        public void Set_AbsoluteExpire<T>(string key, T value, TimeSpan span);
        /// <summary>
        /// 设置缓存(绝对时间过期+滑动过期:比如滑动过期设置半小时,绝对过期时间设置2个小时，那么缓存开始后只要半小时内没有访问就会立马过期,如果半小时内有访问就会向后顺延半小时，但最多只能缓存2个小时)
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">缓存值</param>
        /// <param name="slidingSpan"></param>
        /// <param name="absoluteSpan"></param>
        public void Set_SlidingAndAbsoluteExpire<T>(string key, T value, TimeSpan slidingSpan, TimeSpan absoluteSpan);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">关键字</param>
        public void Remove(string key);
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose();

    }
}
