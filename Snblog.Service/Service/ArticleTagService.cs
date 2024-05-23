using Snblog.Util.Paginated;

namespace Snblog.Service.Service;

public class ArticleTagService : IArticleTagService
{
    private const string Name = "articleTag_";

    private readonly EntityData<ArticleTag> _ret = new();
    private readonly EntityDataDto<ArticleTagDto> _rDto = new();

    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;
    private readonly IMapper _mapper;

    public ArticleTagService(SnblogContext service, ICacheUtil cache, IMapper mapper)
    {
        _service = service;
        _cache = (CacheUtils)cache;
        _mapper = mapper;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Del}{id}");

        var result = await _service.ArticleTags.FindAsync(id);

        if (result == null) return false;

        _service.ArticleTags.Remove(result);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<ArticleTagDto> GetByIdAsync(int id, bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Bid}{id}_{cache}");
        if (cache)
        {
            _rDto.Entity = _cache.GetValue<ArticleTagDto>(ServiceConfig.CacheKey);
            if (_ret.Entity != null) return _rDto.Entity;
        }

        _rDto.Entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.Entity);
        return _rDto.Entity;
    }

    public async Task<bool> AddAsync(ArticleTag entity)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Add}{entity}");
        _service.ArticleTags.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ArticleTag entity)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Up}{entity}");

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
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<ArticleTagDto>>(ServiceConfig.CacheKey);
            if (_ret.EntityList != null) return _rDto.EntityList;
        }

        await QPaging(pageIndex, pageSize, isDesc);
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);
        return _rDto.EntityList;
    }

    public async Task<PaginatedList<ArticleTagDto>> GetPagingTest(int pageIndex, int pageSize)
    {
        return await _mapper.ProjectTo<ArticleTagDto>(_service.ArticleTags
            .OrderBy(c => c.Id)).ToPaginatedListAsync(pageIndex, pageSize);
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
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Sum}{cache}");
        if (cache)
        {
            _ret.EntityCount = _cache.GetValue<int>(ServiceConfig.CacheKey);
            if (_ret.EntityCount != 0) return _ret.EntityCount;
        }

        _ret.EntityCount = await _service.ArticleTags.AsNoTracking().CountAsync();
        _cache.SetValue(ServiceConfig.CacheKey, _ret.EntityCount);
        return _ret.EntityCount;
    }
}