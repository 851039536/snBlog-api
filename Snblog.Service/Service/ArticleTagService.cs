namespace Snblog.Service.Service;

public class ArticleTagService : IArticleTagService
{
    const string NAME = "articleTag_";

    private readonly EntityData<ArticleTag> _ret = new();
    private readonly EntityDataDto<ArticleTagDto> _rDto = new();

    private readonly SnblogContext _service;
    private readonly CacheUtil _cache;
    private readonly IMapper _mapper;


    public ArticleTagService(SnblogContext service, ICacheUtil cache, IMapper mapper)
    {
        _service = service;
        _cache = (CacheUtil)cache;
        _mapper = mapper;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        Common.CacheInfo($"{NAME}{Common.Del}{id}");

        ArticleTag result = await _service.ArticleTags.FindAsync(id);

        if (result == null) return false;

        _service.ArticleTags.Remove(result);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<ArticleTagDto> GetByIdAsync(int id, bool cache)
    {

        Common.CacheInfo($"{NAME}{Common.Bid}{id}_{cache}");
        if (cache)
        {
            _rDto.Entity = _cache.GetValue<ArticleTagDto>(Common.CacheKey);
            if (_ret.Entity != null) return _rDto.Entity;
        }

        _rDto.Entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
        _cache.SetValue(Common.CacheKey, _rDto.Entity);
        return _rDto.Entity;
    }

    public async Task<bool> AddAsync(ArticleTag entity)
    {
        Common.CacheInfo($"{NAME}{Common.Add}{entity}");
        _service.ArticleTags.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ArticleTag entity)
    {
        Common.CacheInfo( $"{NAME}{Common.Up}{entity}");

        _service.ArticleTags.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public async Task<List<ArticleTagDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        Common.CacheInfo($"{NAME}{Common.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<ArticleTagDto>>(Common.CacheKey);
            if (_ret.EntityList != null) return _rDto.EntityList;
        }

        await QPaging(pageIndex, pageSize, isDesc);
        _cache.SetValue(Common.CacheKey, _rDto.EntityList);
        return _rDto.EntityList;
    }

    private async Task QPaging(int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc)
        {
            _rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }
        else
        {
            _rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderBy(c => c.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }


    public async Task<int> GetSumAsync(bool cache)
    {
        Common.CacheInfo($"{NAME}{Common.Sum}{cache}");
        if (cache)
        {
            _ret.EntityCount = _cache.GetValue<int>(Common.CacheKey);
            if (_ret.EntityCount != 0) return _ret.EntityCount;
        }

        _ret.EntityCount = await _service.ArticleTags.AsNoTracking().CountAsync();
        _cache.SetValue(Common.CacheKey, _ret.EntityCount);
        return _ret.EntityCount;
    }
}