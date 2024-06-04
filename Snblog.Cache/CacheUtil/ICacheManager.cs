namespace Snblog.Cache.CacheUtil;

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
    public void Set_NotExpire<T>(string key,T value);

    /// <summary>
    /// 设置缓存(滑动过期:超过一段时间不访问就会过期,一直访问就一直不过期)
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="value">缓存值</param>
    /// <param name="span"></param>
    public void Set_SlidingExpire<T>(string key,T value,TimeSpan span);

    /// <summary>
    /// 设置缓存，使用绝对时间过期策略。一旦缓存被设置，它将在指定的时间段后自动过期，无论期间是否有访问。
    /// </summary>
    /// <typeparam name="T">缓存值的类型</typeparam>
    /// <param name="key">用于检索缓存的唯一标识符</param>
    /// <param name="value">要存储在缓存中的数据</param>
    /// <param name="span">缓存的有效期，超过此时间段后缓存将自动失效</param>
    public void Set_AbsoluteExpire<T>(string key,T value,TimeSpan span);

    /// <summary>
    /// 设置具有滑动过期和绝对过期时间的缓存。
    /// 滑动过期时间（SlidingExpiration）定义了在最后一次访问后多长时间内缓存不过期，
    /// 而绝对过期时间（AbsoluteExpiration）定义了缓存的最长存活时间。
    /// 例如，如果滑动过期设置为半小时，绝对过期时间设置为2小时，
    /// 则缓存开始后，如果半小时内没有访问，缓存将立即过期；
    /// 如果半小时内有访问，过期时间将向后顺延半小时，但缓存最长只能存活2小时。
    /// </summary>
    /// <typeparam name="T">缓存值的类型。</typeparam>
    /// <param name="key">用于检索缓存的唯一键。</param>
    /// <param name="value">要存储在缓存中的值。</param>
    /// <param name="slidingSpan">滑动过期时间，定义了在最后一次访问后多长时间内缓存不过期。</param>
    /// <param name="absoluteSpan">绝对过期时间，定义了缓存的最长存活时间。</param>
    /// <exception cref="ArgumentNullException">当键为空或空白时抛出。</exception>
    public void Set_SlidingAndAbsoluteExpire<T>(string key,T value,TimeSpan slidingSpan,TimeSpan absoluteSpan);

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