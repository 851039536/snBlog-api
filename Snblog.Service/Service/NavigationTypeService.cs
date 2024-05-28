namespace Snblog.Service.Service;

public class NavigationTypeService : INavigationTypeService
{
    private readonly SnblogContext _service;//DB
    private readonly CacheUtils _cacheutil;
    private int result_Int;
    private List<NavigationType> result_List;
    public NavigationTypeService(SnblogContext service, ICacheUtil cacheutil)
    {
        _service = service;
        _cacheutil = (CacheUtils)cacheutil;
    }

    public async Task<bool> AddAsync(NavigationType entity)
    {

        Log.Information("添加数据_NavigationType" + entity);
        await _service.NavigationTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> GetSumAsync(bool cache)
    {
        Log.Information("查询总数_NavigationType" + cache);
        result_Int = _cacheutil.CacheNumber("CountAsync_NavigationType" + cache, result_Int, cache);
        if (result_Int == 0)
        {
            result_Int = await _service.NavigationTypes.CountAsync();
            _cacheutil.CacheNumber("CountAsync_NavigationType" + cache, result_Int, cache);
        }
        return result_Int;
    }

    public async Task<bool> DeleteAsync(int id)
    {

        Log.Information("删除数据_NavigationType" + id);
        var todoItem = await _service.NavigationTypes.FindAsync(id);
        _service.NavigationTypes.Remove(todoItem);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<NavigationType>> GetAllAsync(bool cache)
    {
        Log.Information("查询所有_NavigationType" + cache);
        result_List = _cacheutil.CacheString("查询所有_NavigationType" + cache, result_List, cache);
        if (result_List == null)
        {
            result_List = await _service.NavigationTypes.ToListAsync();
            _cacheutil.CacheString("NavigationTypeGetAllAsync" + cache, result_List, cache);
        }
        return result_List;
    }

    public async Task<NavigationType> GetByIdAsync(int id, bool cache)
    {
        Log.Information("主键查询_NavigationType" + id + cache);
        NavigationType result = default;
        result = _cacheutil.CacheString("GetByIdAsync" + id + cache, result, cache);
        if (result == null)
        {
            result = await _service.NavigationTypes.FindAsync(id);
            _cacheutil.CacheString("GetByIdAsync" + id + cache, result, cache);
        }
        return result;
    }

    public async Task<List<NavigationType>> GetPagingAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        Log.Information("条件分页查询_NavigationType" + type + pageIndex + pageSize + isDesc, cache);
        result_List = _cacheutil.CacheString("GetFyTypeAllAsync_NavigationType" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
        if (result_List == null)
        {
            result_List = await GetFyTypeAll(type, pageIndex, pageSize, isDesc);
            _cacheutil.CacheString("GetFyTypeAllAsync_NavigationType" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
        }
        return result_List;
    }

    private async Task<List<NavigationType>> GetFyTypeAll(string type, int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            if (type.Equals("all"))
            {
                return await _service.NavigationTypes.Where(s => true).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.NavigationTypes.Where(s => s.Name == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }
        else
        {
            if (type.Equals("all"))
            {
                return await _service.NavigationTypes.Where(s => s.Name == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.NavigationTypes.Where(s => true).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }
    }

    public async Task<bool> UpdateAsync(NavigationType entity)
    {
        Log.Information("更新数据_NavigationType" + entity);
        _service.NavigationTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}