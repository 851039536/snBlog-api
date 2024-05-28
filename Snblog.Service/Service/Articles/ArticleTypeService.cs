using Snblog.IService.IService.Articles;

namespace Snblog.Service.Service.Articles;

public class ArticleTypeService : IArticleTypeService
{
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;
    private readonly ServiceHelper _serviceHelper;

    private readonly EntityData<ArticleType> _ret = new();
    private readonly EntityDataDto<ArticleTypeDto> _retDto = new();

    private readonly IMapper _mapper;

    /// <summary>
    /// 缓存Key
    /// </summary>
    private string _cacheKey;

    const string NAME = "ArticleType_";

    public ArticleTypeService(SnblogContext service,ICacheUtil cache,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
        _cache = (CacheUtils)cache;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{NAME}{ServiceConfig.Sum}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return await _service.ArticleTypes.AsNoTracking().CountAsync();
        });
    }

    #endregion
    
    #region 查询所有

    public async Task<List<ArticleTypeDto>> GetAllAsync(bool cache)
    {
        string cacheKey = $"{NAME}{ServiceConfig.All}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.AsNoTracking().ToListAsync());
        });
    }

    #endregion

    #region 主键查询

    public async Task<ArticleTypeDto> GetByIdAsync(int id,bool cache)
    {
        // 生成缓存键
        string cacheKey = $"{NAME}{ServiceConfig.Bid}{id}_{cache}";
        // 检查是否需要缓存，并执行相应的逻辑
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            // 异步获取数据
            var entity = await _service.ArticleTypes.FindAsync(id);
            // 映射到DTO
            var dto = _mapper.Map<ArticleTypeDto>(entity);
            return dto;
        });
    }

    #endregion
    
    #region 分页查询 

    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public async Task<List<ArticleTypeDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{NAME}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
           return await QPaging(pageIndex,pageSize,isDesc);
        });
    }
    private async Task<List<ArticleTypeDto>> QPaging(int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc)
        {
           return _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes
                                                                                 .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                                                                 .Take(pageSize).ToListAsync());
        }
        return _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.OrderBy(c => c.Id)
                                                               .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
    }
    #endregion

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{NAME}{ServiceConfig.Del}{id}");

        var ret = await _service.ArticleTypes.FindAsync(id);
        if(ret == null) return false;

        _service.ArticleTypes.Remove(ret);
        return await _service.SaveChangesAsync() > 0;
    }


    /// <summary>
    ///  添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>bool</returns>
    public async Task<bool> AddAsync(ArticleType entity)
    {
        Log.Information("{ArticleType}{Add}{@Entity}",NAME,ServiceConfig.Add,entity);

        await _service.ArticleTypes.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ArticleType entity)
    {
        Log.Information($"{NAME}{ServiceConfig.Up}{entity}");

        _service.ArticleTypes.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
    
   

   

   
}