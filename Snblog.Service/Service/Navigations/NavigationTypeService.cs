using Snblog.IService.IService.Navigations;

namespace Snblog.Service.Service.Navigations;

public class NavigationTypeService : INavigationTypeService
{
    private readonly SnblogContext _service;
    private const string Name = "navigationType_";
    private readonly ServiceHelper _serviceHelper;

    public NavigationTypeService(SnblogContext service,ServiceHelper serviceHelper)
    {
        _service = service;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => await _service.NavigationTypes.CountAsync());
    }

    #endregion

    #region 查询所有

    public async Task<List<NavigationType>> GetAllAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.All}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => await _service.NavigationTypes.ToListAsync());
    }

    #endregion

    #region 主键查询

    public async Task<NavigationType> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}_{id}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => await _service.NavigationTypes.FindAsync(id));
    }

    #endregion
    
    #region 分页查询

    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="isDesc"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public async Task<List<NavigationType>> GetPagingAsync(string type,int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await GetPaging(type,pageIndex,pageSize,isDesc));
    }

    private async Task<List<NavigationType>> GetPaging(string type, int pageIndex, int pageSize, bool isDesc)
    {
        // 创建一个IQueryable来存储查询结果
        IQueryable<NavigationType> query = _service.NavigationTypes;

        // 根据type参数决定查询条件
        if (!type.Equals("all"))
        {
            query = query.Where(s => s.Name == type);
        }
        // 根据isDesc参数决定排序方式
        query = isDesc ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id);
        // 应用分页逻辑
        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        // 执行查询并返回结果
        return await query.ToListAsync();
    }


    #endregion

    public async Task<bool> AddAsync(NavigationType entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}");
        await _service.NavigationTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}_{id}");
        var ret = await _service.NavigationTypes.FindAsync(id);
        if(ret != null) _service.NavigationTypes.Remove(ret);
        return await _service.SaveChangesAsync() > 0;
    }


    

    public async Task<bool> UpdateAsync(NavigationType entity)
    {
         Log.Information($"{Name}{ServiceConfig.Up}_{entity}");
        _service.NavigationTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}