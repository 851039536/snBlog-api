namespace Snblog.Service;

/// <summary>
/// 常量字符
/// </summary>
public class ServiceHelper
{
    private readonly CacheUtils _cache;
    public ServiceHelper(CacheUtils cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// 检查是否需要缓存
    /// </summary>
    /// <param name="cacheKey">key值</param>
    /// <param name="useCache">是否缓存</param>
    /// <param name="fetchDataAsync">执行api</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> CheckAndExecuteCacheAsync<T>(string cacheKey, bool useCache, Func<Task<T>> fetchDataAsync)
    {
        // 如果需要缓存，先尝试从缓存中获取数据
        if (useCache)
        {
            var cachedData = _cache.GetValue<T>(cacheKey);
            // 检查 cachedData 是否不为 null 且不等于 0
            if (cachedData != null && !EqualityComparer<T>.Default.Equals(cachedData, default(T)))
            {
                return cachedData;
            }
        }
        // 如果缓存中没有数据，或者不需要缓存，则异步获取数据
        var data = await fetchDataAsync();
        // 如果需要缓存，将数据存入缓存
        if (useCache)
        {
            _cache.SetValue(cacheKey, data);
        }
        Log.Information(cacheKey);
        return data;
    }

}