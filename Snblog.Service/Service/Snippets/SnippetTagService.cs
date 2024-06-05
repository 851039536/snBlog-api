using Snblog.IService.IService.Snippets;

namespace Snblog.Service.Service.Snippets;

public class SnippetTagService : ISnippetTagService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;

    private readonly IMapper _mapper;

    private const string Name = "snippetTag_";

    public SnippetTagService(SnblogContext service,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
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
        Log.Information($"{Name}{ServiceConfig.Sum}{cache}");

        string cacheKey = $"{Name}{ServiceConfig.Sum}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => await _service.SnippetTags.AsNoTracking().CountAsync());
    }

    #endregion

    #region 查询所有

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public async Task<List<SnippetTagDto>> GetAllAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.All}{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags.AsNoTracking().ToListAsync()));
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    public async Task<SnippetTagDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<SnippetTagDto>(await _service.SnippetTags.FindAsync(id)));
    }

    #endregion

    #region 根据名称查询

    public async Task<SnippetTagDto> GetByTitle(string name,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{name}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            var ret = await _service.SnippetTags.Where(w => w.Name == name).FirstAsync();
            return _mapper.Map<SnippetTagDto>(ret);
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
    public async Task<List<SnippetTagDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            if(isDesc)
            {
                return _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags
                                                                      .OrderByDescending(c => c.Id)
                                                                      .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                                                      .ToListAsync());
            }
            return _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags.OrderBy(c => c.Id)
                                                                  .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                                                  .ToListAsync());
        });
    }

    #endregion

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");

        var snippetTag = await _service.SnippetTags.FindAsync(id);
        if(snippetTag == null) return false;
        _service.SnippetTags.Remove(snippetTag);
        return await _service.SaveChangesAsync() > 0;
    }


    /// <summary>
    ///  添加 
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    public async Task<bool> AddAsync(SnippetTag entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity.Id}");
        await _service.SnippetTags.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    public async Task<bool> UpdateAsync(SnippetTag entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity.Id}");
        _service.SnippetTags.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}