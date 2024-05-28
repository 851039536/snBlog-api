using Snblog.IService.IService.Diarys;

namespace Snblog.Service.Service.Diarys;

public class DiaryTypeService : IDiaryTypeService
{
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;
    private int _rInt;

    private readonly EntityData<DiaryType> _ret = new();

    private const string Name = "diaryType_";
    private string _cacheKey;

    public DiaryTypeService(SnblogContext service, ICacheUtil cache)
    {
        _service = service;
        _cache = (CacheUtils)cache;
    }

    public async Task<bool> AddAsync(DiaryType entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");

        await _service.DiaryTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> GetSumAsync(bool cache)
    {
        _cacheKey = $"{Name}{ServiceConfig.Sum}{cache}";
        Log.Information(_cacheKey);


        if (cache)
        {
            _rInt = _cache.GetValue<int>(_cacheKey);
            if (_rInt != 0) return _rInt;
        }

        _rInt = await _service.DiaryTypes.CountAsync();
        _cache.SetValue(_cacheKey, _rInt);
        return _rInt;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _cacheKey = $"{Name}{ServiceConfig.Del}{id}";
        Log.Information(_cacheKey);

        var ret = await _service.DiaryTypes.FindAsync(id);

        if (ret == null) return false;

        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<DiaryType>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        _cacheKey = $"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";
        Log.Information(_cacheKey);

        if (cache)
        {
            _ret.EntityList = _cache.GetValue<List<DiaryType>>(_cacheKey);
            if (_ret.EntityList != null) return _ret.EntityList;
        }

        await QPaging(pageIndex, pageSize, isDesc);
        _cache.SetValue(_cacheKey, _ret.EntityList);
        return _ret.EntityList;
    }

    private async Task QPaging(int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            _ret.EntityList = await _service.DiaryTypes
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        else
        {
            _ret.EntityList = await _service.DiaryTypes.OrderBy(c => c.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }

    public async Task<DiaryType> GetByIdAsync(int id, bool cache)
    {
        _cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";
        Log.Information(_cacheKey);
        DiaryType diaryType;

        if (cache)
        {
            diaryType = _cache.GetValue<DiaryType>(_cacheKey);
            if (diaryType != null) return diaryType;
        }

        diaryType = await _service.DiaryTypes.FindAsync(id);
        _cache.SetValue(_cacheKey, diaryType);
        return diaryType;
    }

    public async Task<DiaryType> GetTypeAsync(int type, bool cache)
    {
        _cacheKey = $"{Name}{ServiceConfig.Bid}{type}_{cache}";
        Log.Information(_cacheKey);

        DiaryType diaryType;
        if (cache)
        {
            diaryType = _cache.GetValue<DiaryType>(_cacheKey);
            if (diaryType != null) return diaryType;
        }

        diaryType = await _service.DiaryTypes.FirstAsync(s => s.Id == type);
        _cache.SetValue(_cacheKey, diaryType);
        return diaryType;
    }

    public async Task<bool> UpdateAsync(DiaryType entity)
    {
        _cacheKey = $"{Name}{ServiceConfig.Up}{entity.Id}";
        Log.Information(_cacheKey);

        _service.DiaryTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}