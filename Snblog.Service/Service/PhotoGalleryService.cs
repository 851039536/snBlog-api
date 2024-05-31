namespace Snblog.Service.Service;

public class PhotoGalleryService : IPhotoGalleryService
{
    // 常量字符串。这些常量字符串可以在代码中多次使用，而不必担心它们的值会被修改。
    private const string Name = "PhotoGallery_";

    private readonly EntityDataDto<PhotoGalleryDto> _retDto = new();

    //服务
    private readonly SnblogContext _service;
    private readonly CacheUtils _cache;

    /// <summary>
    /// 使用了IServiceProvider接口来获取所需的服务
    ///它定义了一个方法GetRequiredService，该方法可以用于获取指定类型的服务
    /// </summary>
    /// <param name="serviceProvider"></param>
    public PhotoGalleryService(IServiceProvider serviceProvider)
    {
        // 获取服务提供程序中的实例
        _service = serviceProvider.GetRequiredService<SnblogContext>();
        _cache = (CacheUtils)serviceProvider.GetRequiredService<ICacheUtil>();
    }

    public async Task<bool> DelAsync(int id)
    {
        // 设置缓存键,记录日志
        Log.Information($"{Name}{ServiceConfig.Del}{id}");

        // 通过id查找文章
        var ret = await _service.PhotoGalleries.FindAsync(id);
        // 如果文章不存在，返回false
        if (ret == null) return false;
        _service.PhotoGalleries.Remove(ret); //删除单个
        _service.Remove(ret); //直接在context上Remove()方法传入model，它会判断类型
        // 保存更改
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<PhotoGalleryDto> GetByIdAsync(int id, bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.Bid}{id}_{cache}");
        if (cache)
        {
            _retDto.Entity = _cache.GetValue<PhotoGalleryDto>(ServiceConfig.CacheKey);
            if (_retDto.Entity != null) return _retDto.Entity;
        }

        _retDto.Entity = await _service.PhotoGalleries
            .SelectPhotoGallery()
            .SingleOrDefaultAsync(b => b.Id == id);
        _cache.SetValue(ServiceConfig.CacheKey, _retDto.Entity);
        return _retDto.Entity;
    }
        

    public async Task<bool> AddAsync(PhotoGallery entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");

        entity.TimeCreate = entity.TimeModified = DateTime.Now;
        //此方法中的异步添加改为同步添加，因为 SaveChangesAsync 方法已经是异步的，不需要再使用异步添加
        _service.PhotoGalleries.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(PhotoGallery entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity}");

        entity.TimeModified = DateTime.Now; //更新时间

        var res = await _service.Articles.Where(w => w.Id == entity.Id).Select(
            s => new
            {
                s.TimeCreate,
            }
        ).ToListAsync();
        entity.TimeCreate = res[0].TimeCreate; //赋值表示更新时间不变
        _service.PhotoGalleries.Update(entity);
        return await _service.SaveChangesAsync() > 0;

        //待更新此优化
        //var res = await _service.Articles.Where(w => w.Id == entity.Id)
        //                     .Select(s => s.TimeCreate)
        //                     .FirstOrDefaultAsync();
        //entity.TimeCreate = res;  //赋值表示更新时间不变
        //_service.Articles.Update(entity);
        //return await _service.SaveChangesAsync() > 0;
        // 优化说明：
        // 1. 将查询 TimeCreate 的代码简化为只查询一个字段。
        // 2. 使用 FirstOrDefaultAsync 方法代替 ToListAsync 方法，因为只需要查询一个字段。
    }


    public async Task<int> GetSumAsync(int identity, string type, bool cache)
    {
        Log.Information($"{Name}{ServiceConfig.Sum}{identity}_{type}_{cache}");

        if (cache)
        {
            int sum = _cache.GetValue<int>(ServiceConfig.CacheKey);
            if (sum != 0)
            {
                //通过entityInt 值是否为 0 判断结果是否被缓存
                return sum;
            }
        }

        return identity switch
        {
            // case 
            0 => await GetArticleCountAsync(), // 读取文章数量，无需筛选条件
            1 => await GetArticleCountAsync(c => c.Type.Name == type),
            2 => await GetArticleCountAsync(c => c.Tag == type),
            3 => await GetArticleCountAsync(c => c.User.Name == type),
            _ => -1, //default
        };
    }


    /// <summary>
    /// 获取文章的数量
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    /// <returns>返回文章的数量</returns>
    private async Task<int> GetArticleCountAsync(Expression<Func<PhotoGallery, bool>> predicate = null)
    {
        var query = _service.PhotoGalleries.AsNoTracking();
        //如果有筛选条件
        if (predicate != null) query = query.Where(predicate);

        try
        {
            int count = await query.CountAsync();
            _cache.SetValue(ServiceConfig.CacheKey, count); //设置缓存
            return count;
        }
        catch
        {
            return -1;
        }
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <param name="ordering">排序规则 data:时间|read:阅读|give:点赞|id:主键</param>
    public async Task<List<PhotoGalleryDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
        string ordering, bool isDesc, bool cache)
    {
        Log.Information(
            $"{Name}{ServiceConfig.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}");

        if (cache)
        {
            _retDto.EntityList = _cache.GetValue<List<PhotoGalleryDto>>(ServiceConfig.CacheKey);
            if (_retDto.EntityList != null)
            {
                return _retDto.EntityList;
            }
        }

        switch (identity)
        {
            case 0:
                return await GetPaging(pageIndex, pageSize, ordering, isDesc);
            case 1:
                return await GetPaging(pageIndex, pageSize, ordering, isDesc, w => w.Type.Name == type);
            case 2:
                return await GetPaging(pageIndex, pageSize, ordering, isDesc, w => w.Tag == type);
            case 3:
                return await GetPaging(pageIndex, pageSize, ordering, isDesc, w => w.User.Name == type);
            case 4:
                _retDto.Name = type.Split(',');
                return await GetPaging(pageIndex, pageSize, ordering, isDesc, w =>
                    w.Tag == _retDto.Name[0]
                    && w.User.Name == _retDto.Name[1]);
            default:
                return await GetPaging(pageIndex, pageSize, ordering, isDesc);
        }
    }

    private async Task<List<PhotoGalleryDto>> GetPaging(int pageIndex, int pageSize, string ordering, bool isDesc,
        Expression<Func<PhotoGallery, bool>> predicate = null)
    {
        var articles = _service.PhotoGalleries.AsQueryable();

        // 查询条件,如果为空则无条件查询
        if (predicate != null)
        {
            articles = articles.Where(predicate);
        }

        switch (ordering)
        {
            case "id":
                articles = isDesc ? articles.OrderByDescending(c => c.Id) : articles.OrderBy(c => c.Id);
                break;
            case "data":
                articles = isDesc
                    ? articles.OrderByDescending(c => c.TimeCreate)
                    : articles.OrderBy(c => c.TimeCreate);
                break;
            case "give":
                articles = isDesc ? articles.OrderByDescending(c => c.Give) : articles.OrderBy(c => c.Give);
                break;
        }

        var data = await articles.Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .SelectPhotoGallery().ToListAsync();
        _cache.SetValue(ServiceConfig.CacheKey, _retDto.EntityList);
        return data;
    }


    public async Task<bool> UpdatePortionAsync(PhotoGallery entity, string type)
    {
        Log.Information($"{Name}{ServiceConfig.UpPortiog}{entity.Id}_{type}");
        var ret = await _service.PhotoGalleries.FindAsync(entity.Id);
        if (ret == null) return false;

        switch (type)
        {
            //指定字段进行更新操作
            case "Give":
                ret.Give = entity.Give;
                break;
            default:
                return false;
        }

        //执行数据库操作
        await _service.SaveChangesAsync();
        //await _service.SaveChangesAsync() > 0;
        return true;
    }

    public async Task<List<PhotoGalleryDto>> GetContainsAsync(int identity, string type, string name, bool cache)
    {
        string upNames = name.ToUpper();

        Log.Information($"{Name}{ServiceConfig.Contains}{identity}_{type}_{name}_{cache}");

        if (cache)
        {
            _retDto.EntityList = _cache.GetValue<List<PhotoGalleryDto>>(ServiceConfig.CacheKey);
            if (_retDto.EntityList != null)
            {
                return _retDto.EntityList;
            }
        }

        return identity switch
        {
            0 => await GetContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
            1 => await GetContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Type.Name == type),
            2 => await GetContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Tag == type),
            _ => await GetContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
        };
    }

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    private async Task<List<PhotoGalleryDto>> GetContainsAsync(Expression<Func<PhotoGallery, bool>> predicate = null)
    {
        if (predicate == null)
        {
            return _retDto.EntityList;
        }

        _retDto.EntityList = await _service.PhotoGalleries.Where(predicate).SelectPhotoGallery().ToListAsync();
        _cache.SetValue(ServiceConfig.CacheKey, _retDto.EntityList); //设置缓存
        return _retDto.EntityList;
    }
}