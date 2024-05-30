using Snblog.IService.IService.Snippets;

namespace Snblog.Service.Service.Snippets;

public class SnippetTagService : ISnippetTagService
{
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;

    private readonly EntityData<SnippetTag> _ret = new();
    private readonly EntityDataDto<SnippetTagDto> _rDto = new();
    private readonly IMapper _mapper;

    private const string Name = "SnippetTag_";

    public SnippetTagService(SnblogContext service, ICacheUtil cache, IMapper mapper)
    {
        _service = service;
        _cache = (CacheUtils)cache;
        _mapper = mapper;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Del}{id}");

        var snippetTag = await _service.SnippetTags.FindAsync(id);
        if (snippetTag == null) return false;
        _service.SnippetTags.Remove(snippetTag);
        return await _service.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    public async Task<SnippetTagDto> GetByIdAsync(int id, bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Bid}{id}");
        if (cache)
        {
            _rDto.Entity = _cache.GetValue<SnippetTagDto>(ServiceConfig.CacheKey);
            if (_rDto.Entity != null) return _rDto.Entity;
        }

        _rDto.Entity = _mapper.Map<SnippetTagDto>(await _service.SnippetTags.FindAsync(id));
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.Entity);
        return _rDto.Entity;
    }     
        
    public async Task<SnippetTagDto> GetByTitle(string name, bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Bid}{name}");
        if (cache)
        {
            _rDto.Entity = _cache.GetValue<SnippetTagDto>(ServiceConfig.CacheKey);
            if (_rDto.Entity != null) return _rDto.Entity;
        }

        var ret = await _service.SnippetTags.Where(w => w.Name == name).FirstAsync();
        _rDto.Entity = _mapper.Map<SnippetTagDto>(ret);
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.Entity);
        return _rDto.Entity;
    }

    /// <summary>
    ///  添加 
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    public async Task<bool> AddAsync(SnippetTag entity)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Add}{entity.Id}");
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
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Up}{entity.Id}");

        _service.SnippetTags.Update(entity);
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
    public async Task<List<SnippetTagDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<SnippetTagDto>>(ServiceConfig.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        if (isDesc)
        {
            _rDto.EntityList = _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }
        else
        {
            _rDto.EntityList = _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags.OrderBy(c => c.Id)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);
        return _rDto.EntityList;
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public async Task<List<SnippetTagDto>> GetAllAsync(bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.All}{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<SnippetTagDto>>(ServiceConfig.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        _rDto.EntityList =
            _mapper.Map<List<SnippetTagDto>>(await _service.SnippetTags.AsNoTracking().ToListAsync());
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);
        return _rDto.EntityList;
    }

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    public async Task<int> GetSumAsync(bool cache)
    {
        ServiceConfig.CacheInfo($"{Name}{ServiceConfig.Sum}{cache}");
        if (cache)
        {
            _ret.EntityCount = _cache.GetValue<int>(ServiceConfig.CacheKey);
            if (_ret.EntityCount != 0) return _ret.EntityCount;
        }

        _ret.EntityCount = await _service.SnippetTags.AsNoTracking().CountAsync();
        _cache.SetValue(ServiceConfig.CacheKey, _ret.EntityCount);
        return _ret.EntityCount;
    }
}