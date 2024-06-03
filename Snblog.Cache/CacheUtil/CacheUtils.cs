namespace Snblog.Cache.CacheUtil;

public class CacheUtils 
{
    //创建内存缓存对象
    private readonly CacheManager _cache;
    public CacheUtils(ICacheManager cache)
    {
        _cache = (CacheManager)cache;
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
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">传入返回格式</typeparam>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public T SetValue<T>(string key,T value)
    {
        T ret = default;
        _cache.Set_AbsoluteExpire(key,value,_cache.Time);
        return ret;
    }
    
}