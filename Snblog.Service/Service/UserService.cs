namespace Snblog.Service.Service;

public class UserService : IUserService
{
    private readonly CacheUtils _cache;
    private readonly SnblogContext _service;
    private int _rInt;
    private UserDto _rDto = default;
    private List<UserDto> _rListDto = default;

    // 创建一个字段来存储mapper对象
    private readonly IMapper _mapper;

    const string NAME = "user_";
    const string BYID = "BYID_";
    const string SUM = "SUM_";
    const string CONTAINS = "CONTAINS_";
    const string DEL = "DEL_";
    const string ADD = "ADD";
    const string UPDATE = "UPDATE_";

    public UserService(SnblogContext service, IMapper mapper, ICacheUtil cache)
    {
        _service = service;
        _mapper = mapper;
        _cache = (CacheUtils)cache;
    }

    public async Task<bool> DelAsync(int id)
    {
        ServiceConfig.CacheInfo($"{NAME}{ServiceConfig.Del}{id}");

        // 通过id查找文章
        var user = await _service.Users.FindAsync(id);

        // 如果文章不存在，返回false
        if (user == null)
            return false;

        _service.Users.Remove(user); //删除单个
        _service.Remove(user); //直接在context上Remove()方法传入model，它会判断类型

        // 保存更改
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<UserDto> GetByIdAsync(int id, bool cache)
    {
        Log.Information($"{NAME}{BYID}{id}_{cache}");
        _rDto = _cache.CacheString($"{NAME}{BYID}{id}_{cache}", _rDto, cache);
        if (_rDto == null)
        {
            _rDto = _mapper.Map<UserDto>(await _service.Users.FindAsync(id));
            _cache.CacheString($"{NAME}{BYID}{id}_{cache}", _rDto, cache);
        }
        return _rDto;
    }

    //todo 查询异常
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    public async Task<List<UserDto>> GetPagingAsync(int pageIndex, int pageSize)
    {
        var data = await _service.Users.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        return _mapper.Map<List<UserDto>>(data);
    }

    public async Task<int> AddAsync(User entity)
    {
        Log.Information($"{NAME}{ADD}{entity}");
        await _service.Users.AddAsync(entity);
        return await _service.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        ServiceConfig.CacheInfo($"{NAME}{ServiceConfig.Up}{entity}");
        _service.Users.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> GetSumAsync(bool cache)
    {
        Log.Information($"{NAME}{SUM}{cache}");
        _rInt = _cache.CacheString($"{NAME}{SUM}{cache}", _rInt, cache);
        if (_rInt == 0)
        {
            _rInt = await _service.Users.CountAsync();
            _cache.CacheString($"{NAME}{SUM}{cache}", _rInt, cache);
        }
        return _rInt;
    }

    public async Task<List<UserDto>> GetContainsAsync(string name, bool cache)
    {
        Log.Information($"{NAME}{CONTAINS}{name}{cache}");
        _rListDto = _cache.CacheString($"{NAME}{CONTAINS}{name}{cache}", _rListDto, cache);
        if (_rListDto == null)
        {
            var res = await _service.Users.Where(u => u.Name.Contains(name) || u.Nickname.Contains(name)).AsNoTracking().ToListAsync();
            _rListDto = _mapper.Map<List<UserDto>>(res);
            _cache.CacheString($"{NAME}{CONTAINS}{name}{cache}", _rListDto, cache);
        }
        return _rListDto;
    }
}
