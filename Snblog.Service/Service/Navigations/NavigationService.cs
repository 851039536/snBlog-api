using Snblog.IService.IService.Navigations;

namespace Snblog.Service.Service.Navigations;

public class NavigationService : INavigationService
{
    private const string Name = "Navigation_";
    private readonly ServiceHelper _serviceHelper;
    private readonly SnblogContext _service;

    private readonly IMapper _mapper;

    public NavigationService(SnblogContext service,IMapper mapper,ServiceHelper serviceHelper)
    {
        _service = service;
        _mapper = mapper;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{identity}_{type}_{cache}";
        var ret = _service.Navigations.AsQueryable().AsNoTracking();
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await ret.CountAsync(),
                1 => await ret.Where(w => w.Type.Name == type).CountAsync(),
                2 => await ret.Where(w => w.User.Name == type).CountAsync(),
                var _ => -1
            };
        });
    }

    #endregion

    #region 主键查询

    public async Task<NavigationDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,
            async () => _mapper.Map<NavigationDto>(await _service.Navigations.FindAsync(id)));
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">匹配描述，标题，URL:0 || 分类:1 || 用户:2</param>
    /// <param name="type">查询条件:用户||分类</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    public async Task<List<NavigationDto>> GetContainsAsync(int identity,string type,string name,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Contains}{identity}_{type}_{name}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await ContainsAsync(
                    l => l.Name.ToLower().Contains(name) || l.Describe.ToLower().Contains(name) || l.Url.ToLower().Contains(name)),
                1 => await ContainsAsync(l => l.Name.ToLower().Contains(name) && l.Type.Name == type),
                2 => await ContainsAsync(l => l.Name.ToLower().Contains(name) && l.User.Name == type),
                var _ => null
            };
        });
    }


    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    private async Task<List<NavigationDto>> ContainsAsync(Expression<Func<Navigation,bool>> predicate)
    {
        return await _service.Navigations.Where(predicate).SelectNavigation().AsNoTracking().ToListAsync();
    }

    #endregion

    #region 条件查询

    public async Task<List<NavigationDto>> GetTypeAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Condition}{identity}_{type}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                1 => _mapper.Map<List<NavigationDto>>(await _service.Navigations.Where(w => w.Type.Name == type)
                                                                    .SelectNavigation()
                                                                    .AsNoTracking()
                                                                    .ToListAsync()),
                2 => _mapper.Map<List<NavigationDto>>(await _service.Navigations.Where(w => w.User.Name == type)
                                                                    .SelectNavigation()
                                                                    .AsNoTracking()
                                                                    .ToListAsync()),
                var _ => null
            };
        });
    }

    #endregion

    #region 分页查询

    public async Task<List<NavigationDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
                                                          string ordering,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await Paging(pageIndex,pageSize,ordering,isDesc),
                1 => await Paging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type),
                2 => await Paging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type),
                _ => null
            };
        });
    }

    private async Task<List<NavigationDto>> Paging(int pageIndex,int pageSize,string ordering,bool isDesc,
                                                   Expression<Func<Navigation,bool>> predicate = null)
    {
        var navigation = _service.Navigations.AsQueryable();

        // 查询条件,如果为空则无条件查询
        if(predicate != null)
        {
            navigation = navigation.Where(predicate);
        }

        navigation = ordering switch
        {
            "id" => isDesc ? navigation.OrderByDescending(c => c.Id) : navigation.OrderBy(c => c.Id),
            "data" => isDesc ? navigation.OrderByDescending(c => c.TimeCreate) : navigation.OrderBy(c => c.TimeCreate),
            var _ => navigation
        };
        return await navigation.Skip((pageIndex - 1) * pageSize).Take(pageSize).SelectNavigation().ToListAsync();
    }

    #endregion

    #region 生成随机数

    /// <summary>
    /// 生成随机数
    /// </summary>
    /// <param name="minValue">1</param>
    /// <param name="maxValue">11</param>
    /// <returns></returns>
    public async Task<bool> RandomImg(int minValue,int maxValue)
    {
        // 获取所有记录ID
        var allIds = _service.Navigations.Select(entity => entity.Id).ToList();
        foreach(int id in allIds)
        {
            // 根据记录ID获取实体对象
            var entity = _service.Navigations.FirstOrDefault(e => e.Id == id);
            //c#生成1到10的随机数，每次生成更新Navigations中的img
            Random random = new();
            if(entity != null)
            {
                int num = random.Next(minValue,maxValue); // 生成1到10的随机数
                // 更新"Img"字段的值
                entity.Img = num + ".jpg";
                // 保存更改
                await _service.SaveChangesAsync();
            }
        }

        return true;
    }

    #endregion

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");
        var ret = await _service.Navigations.FindAsync(id);
        if(ret == null) return false;
        _service.Navigations.Remove(ret);
        return await _service.SaveChangesAsync() > 0;
    }


    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(Navigation entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");

        entity.TimeCreate = DateTime.Now;
        entity.TimeModified = DateTime.Now;
        await _service.Navigations.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Navigation entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity}");

        entity.TimeModified = DateTime.Now; //更新时间
        var ret = await _service.Navigations.Where(w => w.Id == entity.Id).Select(
            s => new { s.TimeCreate, }
        ).AsNoTracking().ToListAsync();
        entity.TimeCreate = ret[0].TimeCreate; //赋值表示时间不变
        _service.Navigations.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }
}