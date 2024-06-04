

namespace Snblog.Cache.CacheUtil;

public class CacheUtils 
{
    //创建内存缓存对象
    private readonly CacheManager _cache;
    public CacheUtils(ICacheManager cache)
    {
        _cache = cache as CacheManager;
    }
 
    /// <summary>
    /// 读取缓存
    /// </summary>
    /// <typeparam name="T">传入返回格式</typeparam>
    /// <param name="key">键</param>
    /// <returns>返回传入的格式数据</returns>
    public T GetValue<T>(string key)
    {
        var ret = _cache.Get<T>(key);
        return ret;
    }

    /// <summary>
    /// 设置缓存，使用绝对时间过期策略。一旦缓存被设置，它将在指定的时间段后自动过期，无论期间是否有访问。
    /// </summary>
    /// <typeparam name="T">传入返回格式</typeparam>
    /// <param name="key">用于检索缓存的唯一标识符</param>
    /// <param name="value">要存储在缓存中的数据</param>
    /// <returns></returns>
    public void SetValue<T>(string key,T value)
    {
        _cache.Set_AbsoluteExpire(key,value,_cache.AbsoluteExpiration);
    }
}