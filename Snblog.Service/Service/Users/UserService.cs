using Snblog.IService.Users;

namespace Snblog.Service.Service.Users;

public class UserService : IUserService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;

    // 创建一个字段来存储mapper对象
    private readonly IMapper _mapper;

    private const string Name = "user_";

    public UserService(SnblogContext service,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await _service.Users.CountAsync());
    }

    #endregion

    #region 主键查询

    public async Task<UserDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<UserDto>(await _service.Users.FindAsync(id)));
    }

    #endregion

    #region 模糊查询

    public async Task<List<UserDto>> GetContainsAsync(string name,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Contains}{name}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            var ret = await _service
                            .Users.Where(u => u.Name.Contains(name) || u.Nickname.Contains(name))
                            .AsNoTracking()
                            .ToListAsync();
            return _mapper.Map<List<UserDto>>(ret);
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
    public async Task<List<UserDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            if(isDesc)
            {
                return _mapper.Map<List<UserDto>>(await _service.Users
                                                                .OrderByDescending(c => c.Id)
                                                                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                                                .ToListAsync());
            }

            return _mapper.Map<List<UserDto>>(await _service.Users.OrderBy(c => c.Id)
                                                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                                                            .ToListAsync());
        });
    }

    #endregion

    public async Task<bool> DelAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");

        // 通过id查找文章
        var user = await _service.Users.FindAsync(id);

        // 如果文章不存在，返回false
        if(user == null)
            return false;

        _service.Users.Remove(user); //删除单个
        _service.Remove(user); //直接在context上Remove()方法传入model，它会判断类型

        // 保存更改
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<int> AddAsync(User entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        await _service.Users.AddAsync(entity);
        return await _service.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity}");
        _service.Users.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}