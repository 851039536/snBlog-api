using Snblog.IService.IService.Diarys;

namespace Snblog.Service.Service.Diarys;

public class DiaryTypeService : IDiaryTypeService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;


    private const string Name = "diaryType_";
    private string _cacheKey;

    public DiaryTypeService(SnblogContext service,ServiceHelper serviceHelper)
    {
        _service = service;
        _serviceHelper = serviceHelper;
    }

    # region 查询总数

    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return await _service.DiaryTypes.CountAsync();
        });
    }

    #endregion

    #region 主键查询

    public async Task<DiaryType> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await _service.DiaryTypes.FindAsync(id));
    }

    #endregion

    #region 类别查询

    public async Task<DiaryType> GetTypeAsync(int type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{type}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return await _service.DiaryTypes.FirstAsync(s => s.Id == type);
        });
    }

    #endregion

    #region 分页查询

    public async Task<List<DiaryType>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await QPaging(pageIndex,pageSize,isDesc));
    }

    private async Task<List<DiaryType>> QPaging(int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc)
        {
            return await _service.DiaryTypes
                                 .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        return await _service.DiaryTypes.OrderBy(c => c.Id)
                             .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    #endregion

    public async Task<bool> AddAsync(DiaryType entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        await _service.DiaryTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        _cacheKey = $"{Name}{ServiceConfig.Del}{id}";
        Log.Information(_cacheKey);
        var ret = await _service.DiaryTypes.FindAsync(id);

        if(ret == null) return false;

        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> UpdateAsync(DiaryType entity)
    {
        _cacheKey = $"{Name}{ServiceConfig.Up}{entity.Id}";
        Log.Information(_cacheKey);
        _service.DiaryTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}