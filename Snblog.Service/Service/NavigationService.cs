using System.Collections.Generic;
using System.Xml.Linq;

namespace Snblog.Service.Service;

public class NavigationService : INavigationService
{
    const string Name = "Navigation_";

    private readonly EntityData<Navigation> _ret = new();
    private readonly EntityDataDto<NavigationDto> _rDto = new();


    private readonly SnblogContext _service; //DB
    private readonly CacheUtil _cache;

    private readonly IMapper _mapper;

    public NavigationService(SnblogContext service,ICacheUtil cache,IMapper mapper)
    {
        _service = service;
        _cache = (CacheUtil)cache;
        _mapper = mapper;
    }

    public async Task<List<NavigationDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
        string ordering,bool isDesc,bool cache)
    {
        Common.CacheInfo(
            $"{Name}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}");
        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<NavigationDto>>(Common.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        switch (identity) //查询条件
        {
            case 0:
                return await Paging(pageIndex,pageSize,ordering,isDesc);

            case 1:
                return await Paging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type);

            case 2:
                return await Paging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type);
        }

        _ret.EntityList = _cache.SetValue(Common.CacheKey,_ret.EntityList);
        return _rDto.EntityList;
    }

    private async Task<List<NavigationDto>> Paging(int pageIndex,int pageSize,string ordering,bool isDesc,
        Expression<Func<Navigation,bool>> predicate = null)
    {
        IQueryable<Navigation> navigation = _service.Navigations.AsQueryable();

        // 查询条件,如果为空则无条件查询
        if (predicate != null)
        {
            navigation = navigation.Where(predicate);
        }

        switch (ordering)
        {
            case "id":
                navigation = isDesc ? navigation.OrderByDescending(c => c.Id) : navigation.OrderBy(c => c.Id);
                break;
            case "data":
                navigation = isDesc
                    ? navigation.OrderByDescending(c => c.TimeCreate)
                    : navigation.OrderBy(c => c.TimeCreate);
                break;
        }

        _rDto.EntityList = await navigation.Skip((pageIndex - 1) * pageSize).Take(pageSize).SelectNavigation().ToListAsync();
        _cache.SetValue(Common.CacheKey,_rDto.EntityList);
        return _rDto.EntityList;
    }


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(int id)
    {

        Common.CacheInfo($"{Name}{Common.Del}{id}");
        Navigation ret = await _service.Navigations.FindAsync(id);
        if (ret == null) return false;
        _service.Navigations.Remove(ret);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<List<NavigationDto>> GetTypeAsync(int identity,string type,bool cache)
    {
        Common.CacheInfo($"{Name}{Common.Condition}{identity}_{type}_{cache}");

        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<NavigationDto>>(Common.CacheKey);
            if (_rDto.EntityList != null) return _rDto.EntityList;
        }

        switch (identity)
        {
            case 1:
                _rDto.EntityList = _mapper.Map<List<NavigationDto>>(await _service.Navigations
                    .Where(w => w.Type.Name == type).SelectNavigation().AsNoTracking().ToListAsync());
                break;
            case 2:
                _rDto.EntityList = _mapper.Map<List<NavigationDto>>(await _service.Navigations
                    .Where(w => w.User.Name == type).SelectNavigation().AsNoTracking().ToListAsync());
                break;
        }

        _cache.SetValue(Common.CacheKey,_rDto.EntityList);

        return _rDto.EntityList;
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(Navigation entity)
    {
        Common.CacheInfo($"{Name}{Common.Add}{entity}");

        entity.TimeCreate = DateTime.Now;
        entity.TimeModified = DateTime.Now;
        await _service.Navigations.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Navigation entity)
    {
        Common.CacheInfo($"{Name}{Common.Up}{entity}");

        entity.TimeModified = DateTime.Now; //更新时间
        var ret = await _service.Navigations.Where(w => w.Id == entity.Id).Select(
            s => new
            {
                s.TimeCreate,
            }
        ).AsNoTracking().ToListAsync();
        entity.TimeCreate = ret[0].TimeCreate; //赋值表示时间不变
        _service.Navigations.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }


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
        foreach (var id in allIds)
        {
            // 根据记录ID获取实体对象
            var entity = _service.Navigations.FirstOrDefault(e => e.Id == id);
            //c#生成1到10的随机数，每次生成更新Navigations中的img
            Random random = new();
            if (entity != null)
            {
                var num = random.Next(minValue,maxValue); // 生成1到10的随机数
                // 更新"Img"字段的值
                entity.Img = num + ".jpg";
                // 保存更改
                await _service.SaveChangesAsync();
            }
        }
        return true;
    }
    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        Common.CacheInfo($"{Name}{Common.Sum}{identity}_{type}_{cache}");

        IQueryable<Navigation> ret = _service.Navigations.AsQueryable().AsNoTracking();

        if (cache)
        {
            int sum = _cache.GetValue<int>(Common.CacheKey);
            if (sum != 0)
            {
                //通过entityInt 值是否为 0 判断结果是否被缓存
                return sum;
            }
        }

        switch (identity)
        {
            case 0:
                _ret.EntityCount = await ret.CountAsync();
                break;
            case 1:
                _ret.EntityCount = await ret.Where(w => w.Type.Name == type).CountAsync();

                break;
            case 2:
                _ret.EntityCount = await ret.Where(w => w.User.Name == type).CountAsync();

                break;
        }
        _cache.SetValue(Common.CacheKey,_ret.EntityCount); //设置缓存

        return _ret.EntityCount;
    }


    public async Task<NavigationDto> GetByIdAsync(int id,bool cache)
    {
        Common.CacheInfo($"{Name}{Common.Bid}{id}_{cache}");

        if (cache)
        {
            _rDto.Entity = _cache.GetValue<NavigationDto>(Common.CacheKey);
            if (_rDto.Entity != null) return _rDto.Entity;
        }
        _rDto.Entity = _mapper.Map<NavigationDto>(await _service.Navigations.FindAsync(id));
        _cache.SetValue(Common.CacheKey,_rDto.Entity);
        return _rDto.Entity;
    }

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
        var upNames = name.ToLower();

        Common.CacheInfo($"{Name}{Common.Contains}{identity}_{type}_{name}_{cache}");

        if (cache)
        {
            _rDto.EntityList = _cache.GetValue<List<NavigationDto>>(Common.CacheKey);
            if (_rDto.EntityList != null)
            {
                return _rDto.EntityList;
            }
        }
        switch (identity)
        {
            case 0:
                return await ContainsAsync(l => l.Name.ToLower().Contains(name) || l.Describe.ToLower().Contains(name) || l.Url.ToLower().Contains(name));
            case 1:
                return await ContainsAsync(l => l.Name.ToLower().Contains(name) && l.Type.Name == type);
            case 2:
                return await ContainsAsync(l => l.Name.ToLower().Contains(name) && l.User.Name == type);
        }
        return _rDto.EntityList;
    }


    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    private async Task<List<NavigationDto>> ContainsAsync(Expression<Func<Navigation,bool>> predicate = null)
    {
        if (predicate == null)
        {
            return _rDto.EntityList;
        }

        _rDto.EntityList = await _service.Navigations.Where(predicate).SelectNavigation().AsNoTracking().ToListAsync();
        _cache.SetValue(Common.CacheKey,_rDto.EntityList); //设置缓存
        return _rDto.EntityList;
    }
}