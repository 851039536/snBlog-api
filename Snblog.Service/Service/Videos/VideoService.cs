using Snblog.IService.Videos;

namespace Snblog.Service.Service.Videos;

public class VideoService : IVideoService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;
    private readonly IMapper _mapper;

    private const string Name = "video_";

    public VideoService(SnblogContext service,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{identity}_{type}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await _service.Videos.AsNoTracking().CountAsync(),
                1 => await _service.Videos.Where(w => w.Type.Name == type).AsNoTracking().CountAsync(),
                2 => await _service.Videos.Where(w => w.User.Name == type).AsNoTracking().CountAsync(),
                var _ => -1
            };
        });
    }

    #endregion

    #region 查询所有

    public async Task<List<VideoDto>> GetAllAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.All}{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<List<VideoDto>>(await _service.Videos.AsNoTracking().ToListAsync()));
    }

    #endregion

    #region 模糊查询

    public async Task<List<VideoDto>> GetContainsAsync(int identity,string type,string name,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Contains}{identity}{type}{name}{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => _mapper.Map<List<VideoDto>>(await _service.Videos.Where(l => l.Name.Contains(name))
                                                               .AsNoTracking()
                                                               .ToListAsync()),
                1 => _mapper.Map<List<VideoDto>>(await _service.Videos.Where(l => l.Name.Contains(name) && l.Type.Name == type)
                                                               .AsNoTracking()
                                                               .ToListAsync()),
                2 => _mapper.Map<List<VideoDto>>(await _service.Videos.Where(l => l.Name.Contains(name) && l.User.Name == type)
                                                               .AsNoTracking()
                                                               .ToListAsync()),
                var _ => null
            };
        });
    }

    #endregion

    #region 主键查询

    public async Task<VideoDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<VideoDto>(await _service.Videos.FindAsync(id)));
    }

    #endregion

    #region 条件查询

    public async Task<List<VideoDto>> GetTypeAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Types}{identity}{type}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                1 => _mapper.Map<List<VideoDto>>(await _service.Videos.Where(s => s.Type.Name == type)
                                                               .AsNoTracking()
                                                               .ToListAsync()),
                2 => _mapper.Map<List<VideoDto>>(await _service.Videos.Where(s => s.User.Name == type)
                                                               .AsNoTracking()
                                                               .ToListAsync()),
                var _ => null
            };
        });
    }

    #endregion

    #region 分页查询

    public async Task<List<VideoDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
                                                     bool isDesc,bool cache)
    {
        string key = $"{Name}{ServiceConfig.Paging}{identity}{pageIndex}{pageSize}{isDesc}{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(key,cache,async () =>
        {
            return identity switch
            {
                0 => await QPagingAll(pageIndex,pageSize,isDesc),
                1 => await GetFyType(type,pageIndex,pageSize,isDesc),
                2 => await GetUser(type,pageIndex,pageSize,isDesc),
                var _ => null
            };
        });
    }

    private async Task<List<VideoDto>> GetUser(string type,int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc) //降序
        {
            return _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
                                                             .OrderByDescending(c => c.Id)
                                                             .Skip((pageIndex - 1) * pageSize)
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
        } //升序

        return _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.User.Name == type)
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

    private async Task<List<VideoDto>> GetFyType(string type,int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc) //降序
        {
            return _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
                                                             .OrderByDescending(c => c.Id)
                                                             .Skip((pageIndex - 1) * pageSize)
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
        } //升序

        return _mapper.Map<List<VideoDto>>(await _service.Videos.Where(w => w.Type.Name == type)
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

    private async Task<List<VideoDto>> QPagingAll(int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc) //降序
        {
            return _mapper.Map<List<VideoDto>>(
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
        } //升序

        return _mapper.Map<List<VideoDto>>(
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

    #endregion

    public async Task<bool> AddAsync(Video entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}_{entity}");
        await _service.Videos.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Video entity)
    {
        Log.Information($"{Name}{ServiceConfig.Del}_{entity}");
        _service.Videos.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}_{id}");
        var todoItem = await _service.Videos.FindAsync(id);
        if(todoItem == null)
        {
            return false;
        }

        _service.Videos.Remove(todoItem);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await GetSum());
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
}