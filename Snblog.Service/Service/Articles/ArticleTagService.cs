using Snblog.IService.IService.Articles;
using Snblog.Util.Paginated;

namespace Snblog.Service.Service.Articles;

public class ArticleTagService : IArticleTagService
{
    private const string Name = "articleTag_";
    private readonly ServiceHelper _serviceHelper;
    private readonly SnblogContext _service;
    private readonly IMapper _mapper;

    public ArticleTagService(SnblogContext service,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            int ret = await _service.ArticleTags.AsNoTracking().CountAsync();
            return ret;
        });
    }

    #endregion

    #region 主键查询

    public async Task<ArticleTagDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            var ret = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
            return ret;
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
    public async Task<List<ArticleTagDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await QPaging(pageIndex,pageSize,isDesc));
    }

    #endregion

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");
        var ret = await _service.ArticleTags.FindAsync(id);
        if(ret == null) return false;
        _service.ArticleTags.Remove(ret);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddAsync(ArticleTag entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        _service.ArticleTags.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ArticleTag entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity}");

        _service.ArticleTags.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<PaginatedList<ArticleTagDto>> GetPagingTest(int pageIndex,int pageSize)
    {
        return await _mapper.ProjectTo<ArticleTagDto>(_service.ArticleTags
                                                              .OrderBy(c => c.Id)).ToPaginatedListAsync(pageIndex,pageSize);
    }

    private async Task<List<ArticleTagDto>> QPaging(int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc)
        {
           return _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags
                                                                              .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                                                              .Take(pageSize).ToListAsync());
        }
        return   _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderBy(c => c.Id)
                                                                                  .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
    }
}