namespace Snblog.Service.Service;

public class VideoService : IVideoService
{
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;
    private readonly EntityData<Video> _ret = new();
    private readonly EntityDataDto<VideoDto> _rDto = new();
    private readonly IMapper _mapper;

    private const string Name = "video_";

    public VideoService(SnblogContext service, ICacheUtil cache, IMapper mapper)
    {
        _service = service;
        _cache = (CacheUtils)cache;
        _mapper = mapper;
    }

    public async Task<VideoDto> GetByIdAsync(int id, bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.Bid}{id}{cache}");
        if (cache)
        {
            _rDto.Entity = _cache.GetValue<VideoDto>(ServiceConfig.CacheKey);
            if (_rDto.Entity != null) return _rDto.Entity;
        }

        _rDto.Entity = _mapper.Map<VideoDto>(await _service.Videos.FindAsync(id));
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.Entity);
        return _rDto.Entity;
    }

    public async Task<List<VideoDto>> GetAllAsync(bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.All}{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<VideoDto>>(ServiceConfig.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos.AsNoTracking().ToListAsync());
        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);

        return _rDto.EntityList;
    }

    public async Task<List<Video>> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        Log.Information("分页查询 _SnVideo:" + type + pageIndex + pageSize + isDesc + cache);
        _ret.EntityList = _cache.CacheString("GetPagingAsync" + type + pageIndex + pageSize + isDesc + cache,
            _ret.EntityList, cache);
        if (_ret.EntityList == null)
        {
            _ret.EntityList = await GetPaging(type, pageIndex, pageSize, isDesc);
            _cache.CacheString("GetPagingAsync" + type + pageIndex + pageSize + isDesc + cache, _ret.EntityList,
                cache);
        }

        return _ret.EntityList;
    }

    private async Task<List<Video>> GetPaging(int type, int pageIndex, int pageSize, bool isDesc)
    {
        if (type == 9999)
        {
            if (isDesc)
            {
                _ret.EntityList = await _service.Videos.OrderByDescending(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            else
            {
                _ret.EntityList = await _service.Videos.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
        }
        else
        {
            if (isDesc)
            {
                _ret.EntityList = await _service.Videos.Where(s => s.TypeId == type).OrderByDescending(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
            else
            {
                _ret.EntityList = await _service.Videos.Where(s => s.TypeId == type).OrderBy(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
            }
        }

        return _ret.EntityList;
    }

    public async Task<int> GetSumAsync(int identity, string type, bool cache)
    {
        Log.Information("查询总数_SnVideo=>" + identity + cache + type + cache);
        _ret.EntityCount = _cache.CacheNumber("Count_SnVideo", _ret.EntityCount, cache);
        if (_ret.EntityCount == 0)
        {
            switch (identity)
            {
                case 0:
                    _ret.EntityCount = await _service.Videos.AsNoTracking().CountAsync();
                    break;
                case 1:
                    _ret.EntityCount = await _service.Videos.Where(w => w.Type.Name == type).AsNoTracking()
                        .CountAsync();
                    break;
                case 2:
                    _ret.EntityCount = await _service.Videos.Where(w => w.User.Name == type).AsNoTracking()
                        .CountAsync();
                    break;
            }

            _cache.CacheNumber("Count_SnVideo", _ret.EntityCount, cache);
        }

        return _ret.EntityCount;
    }

    public async Task<int> GetTypeCount(int type, bool cache)
    {
        Log.Information("条件查总数 :" + type);
        //读取缓存值
        _ret.EntityCount = _cache.CacheNumber("GetTypeCount_SnVideo" + type + cache, _ret.EntityCount, cache);
        if (_ret.EntityCount == 0)
        {
            _ret.EntityCount = await _service.Videos.CountAsync(c => c.TypeId == type);
            _cache.CacheNumber("GetTypeCount_SnVideo" + type + cache, _ret.EntityCount, cache);
        }

        return _ret.EntityCount;
    }

    public async Task<List<Video>> GetTypeAllAsync(int type, bool cache)
    {
        Log.Information("分类查询:_SnVideo" + type + cache);
        _ret.EntityList = _cache.CacheString("GetTypeAllAsync_SnVideo" + type + cache, _ret.EntityList, cache);
        if (_ret.EntityList == null)
        {
            _ret.EntityList = await _service.Videos.Where(s => s.TypeId == type).ToListAsync();
            _cache.CacheString("GetTypeAllAsync_SnVideo" + type + cache, _ret.EntityList, cache);
        }

        return _ret.EntityList;
    }

    public async Task<bool> AddAsync(Video entity)
    {
        Log.Information("添加数据_SnVideo :" + entity);
        await _service.Videos.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Video entity)
    {
        Log.Information("删除数据_SnVideo :" + entity);
        _service.Videos.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information("删除数据_SnVideo:" + id);
        var todoItem = await _service.Videos.FindAsync(id);
        if (todoItem == null)
        {
            return false;
        }

        _service.Videos.Remove(todoItem);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> GetSumAsync(bool cache)
    {
        Log.Information("统计标题数量_SnVideo：" + cache);
        _ret.EntityCount = _cache.CacheNumber("GetSumAsync_SnVideo" + cache, _ret.EntityCount, cache);
        if (_ret.EntityCount == 0)
        {
            _ret.EntityCount = await GetSum();
            _cache.CacheNumber("GetSumAsync_SnVideo" + cache, _ret.EntityCount, cache);
        }

        return _ret.EntityCount;
    }

    /// <summary>
    /// 统计字段数
    /// </summary>
    /// <returns></returns>
    private async Task<int> GetSum()
    {
        var text = await _service.Videos.Select(c => c.Name).ToListAsync();
        return text.Sum(t => t.Length);
    }

    public async Task<List<VideoDto>> GetContainsAsync(int identity, string type, string name, bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.Contains}{identity}{type}{name}{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<VideoDto>>(ServiceConfig.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        switch (identity)
        {
            case 0:
                _rDto.EntityList = _mapper.Map<List<VideoDto>>(
                    await _service.Videos
                        .Where(l => l.Name.Contains(name))
                        .AsNoTracking().ToListAsync());
                break;
            case 1:
                _rDto.EntityList = _mapper.Map<List<VideoDto>>(
                    await _service.Videos
                        .Where(l => l.Name.Contains(name) && l.Type.Name == type)
                        .AsNoTracking().ToListAsync());
                break;
            case 2:
                _rDto.EntityList = _mapper.Map<List<VideoDto>>(
                    await _service.Videos
                        .Where(l => l.Name.Contains(name) && l.User.Name == type)
                        .AsNoTracking().ToListAsync());
                break;
        }

        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);

        return _rDto.EntityList;
    }

    public async Task<List<VideoDto>> GetTypeAsync(int identity, string type, bool cache)
    {
        Log.Information($"SnVideoDto条件查询=>{identity}{type}{cache}");
        _rDto.EntityList = _cache.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache, _rDto.EntityList,
            cache);
        if (_rDto.EntityList == null)
        {
            switch (identity)
            {
                case 1:
                    _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos
                        .Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                    break;
                case 2:
                    _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos
                        .Where(s => s.User.Name == type).AsNoTracking().ToListAsync());
                    break;
            }

            _cache.CacheString("GetTypeAsync_SnVideoDto" + identity + type + cache, _rDto.EntityList, cache);
        }

        return _rDto.EntityList;
    }

    public async Task<List<VideoDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
        bool isDesc, bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.Paging}{identity}{pageIndex}{pageSize}{isDesc}{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<VideoDto>>(ServiceConfig.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        switch (identity) //查询条件
        {
            case 0:
                await QPagingAll(pageIndex, pageSize, isDesc);
                break;
            case 1:
                await GetFyType(type, pageIndex, pageSize, isDesc);
                break;
            case 2:
                await GetUser(type, pageIndex, pageSize, isDesc);
                break;
        }

        _cache.SetValue(ServiceConfig.CacheKey, _rDto.EntityList);


        return _rDto.EntityList;
    }

    private async Task GetUser(string type, int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc) //降序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new VideoDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    User = e.User,
                    Url = e.Url,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    Type = e.Type
                }).AsNoTracking().ToListAsync());
        }
        else //升序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new VideoDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    User = e.User,
                    Url = e.Url,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    Type = e.Type
                }).AsNoTracking().ToListAsync());
        }
    }

    private async Task GetFyType(string type, int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc) //降序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new VideoDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    User = e.User,
                    Url = e.Url,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    Type = e.Type
                }).AsNoTracking().ToListAsync());
        }
        else //升序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(e => new VideoDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Img = e.Img,
                    User = e.User,
                    Url = e.Url,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    Type = e.Type
                }).AsNoTracking().ToListAsync());
        }
    }

    private async Task QPagingAll(int pageIndex, int pageSize, bool isDesc)
    {
        if (isDesc) //降序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(
                await _service.Videos
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).Select(e => new VideoDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Img = e.Img,
                        User = e.User,
                        Url = e.Url,
                        TimeCreate = e.TimeCreate,
                        TimeModified = e.TimeModified,
                        Type = e.Type
                    }).AsNoTracking().ToListAsync());
        }
        else //升序
        {
            _rDto.EntityList = _mapper.Map<List<VideoDto>>(
                await _service.Videos
                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).Select(e => new VideoDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Img = e.Img,
                        User = e.User,
                        Url = e.Url,
                        TimeCreate = e.TimeCreate,
                        TimeModified = e.TimeModified,
                        Type = e.Type
                    }).AsNoTracking().ToListAsync());
        }
    }
}