namespace Snblog.Service.Service;

public class SnPictureTypeService : ISnPictureTypeService
{
    private const string Name = "PictureType_";
    private readonly ServiceHelper _serviceHelper;
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;
    private List<SnPictureType> _retList;
    public SnPictureTypeService(SnblogContext service, ICacheUtil cache,ServiceHelper serviceHelper)
    {
        _service = service;
        _serviceHelper = serviceHelper;
        _cache = (CacheUtils)cache;
    }
    public async Task<int> CountAsync()
    {
        const string cacheKey = $"{Name}{ServiceConfig.Sum}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,false,async () =>
        {
            int sum= await _service.SnPictureTypes.CountAsync();
            _cache.SetValue(cacheKey,sum);
            return sum;
        });


        // _resultInt = _cache.CacheNumber("SnPictureType_CountAsync", _resultInt,true);
        // if (_resultInt != 0)
        // {
        //     return _resultInt;
        // }
        // _resultInt = await _service.SnPictureTypes.CountAsync();
        // _cache.CacheNumber("SnPictureType_CountAsync", _resultInt,true);
        // return _resultInt;
    }

    public async Task<bool> AddAsync(SnPictureType entity)
    {
        await _service.SnPictureTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<SnPictureType>> GetAllAsync()
    {
        const string key = "SnPictureType_GetAllAsync";
        _retList = _cache.CacheString(key, _retList,true);
        if (_retList != null) return _retList;
            
        _retList = await _service.SnPictureTypes.ToListAsync();
        _cache.CacheString(key, _retList,true);
        return _retList;
    }

   

    public async Task<bool> DeleteAsync(int id)
    {
        // _service.SnPictureType.Remove(Entity);
        //return await _service.SaveChangesAsync()>0;
        var todoItem = await _service.SnPictureTypes.FindAsync(id);
        _service.SnPictureTypes.Remove(todoItem);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<SnPictureType> GetByIdAsync(int id)
    {
        SnPictureType pictureType;
        pictureType = await _service.SnPictureTypes.FindAsync(id);
        return pictureType;
    }

    public async Task<bool> UpdateAsync(SnPictureType entity)
    {
        _service.SnPictureTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
    {
        _retList = await GetFyAll(pageIndex, pageSize, isDesc);
        return _retList;
    }

    private async Task<List<SnPictureType>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            return await _service.SnPictureTypes.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        else
        {
            return await _service.SnPictureTypes.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }

    public Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
    {
        throw new NotImplementedException();
    }
}