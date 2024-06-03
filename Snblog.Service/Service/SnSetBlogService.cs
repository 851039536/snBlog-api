namespace Snblog.Service.Service;

public class SnSetBlogService : ISnSetBlogService
{
    private readonly SnblogContext _service;
    private readonly CacheUtils _cacheutil;
    readonly EntityData<SnSetblog> _res = new();
    readonly EntityDataDto<SnSetblogDto> _resDto = new();
    private readonly IMapper _mapper;
    public SnSetBlogService(CacheUtils cacheUtil, SnblogContext coreDbContext, IMapper mapper)
    {
        _service = coreDbContext;
        _cacheutil = cacheUtil;
        _mapper = mapper;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information("删除数据_SnSetBlogs" + id);
        var reslult = await _service.SnSetblogs.FindAsync(id);
        if (reslult == null) return false;
        _service.SnSetblogs.Remove(reslult);//删除单个
        _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
        return await _service.SaveChangesAsync() > 0;
    }

    public Task<List<Article>> GetTypeIdAsync(int sortId,bool cache)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SnSetblogDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
    {
        Log.Information("SnSetBlogDto分页查询=>" + type + pageIndex + pageSize + isDesc + cache);
        // _resDto.EntityList = _cacheutil.CacheString("GetfyAsync_SnSetBlogDto" + type + pageIndex + pageSize + isDesc + cache, _resDto.EntityList, cache);

        if (_resDto.EntityList == null)
        {
            switch (identity) //查询条件
            {
                case 0:
                    if (isDesc)//降序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(
                                    await _service.SnSetblogs
                                        .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    else //升序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(
                                    await _service.SnSetblogs
                                        .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    break;

                case 1:
                    if (isDesc)//降序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.Type.Name == type)
                                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    else //升序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.Type.Name == type)
                                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    break;

                case 2:
                    if (isDesc)//降序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.User.Name == type)
                                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    else //升序
                    {
                        switch (ordering) //排序
                        {
                            case "id":
                                _resDto.EntityList = _mapper.Map<List<SnSetblogDto>>(await _service.SnSetblogs.Where(w => w.User.Name == type)
                                    .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize).AsNoTracking().ToListAsync());
                                break;
                        }
                    }
                    break;
            }
            // _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + pageIndex + pageSize + ordering + isDesc + cache, _resDto.EntityList, cache);
        }
        return _resDto.EntityList;
    }

    public Task<List<Article>> GetfySortTestAsync(int type,int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetFyAsync(int type,int pageIndex,int pageSize,string name,bool isDesc,bool cache)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetFyTitleAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetTagAsync(int tag,bool isDesc,bool cache)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(SnSetblogDto entity)
    {
        Log.Information("添加数据_SnSetBlog" + entity);
        await _service.SnSetblogs.AddAsync(_mapper.Map<SnSetblog>(entity));
        return await _service.SaveChangesAsync() > 0;

    }

    public async Task<bool> UpdateAsync(SnSetblogDto entity)
    {
        Log.Information("更新数据_SnArticle" + entity);
        _service.SnSetblogs.Update(_mapper.Map<SnSetblog>(entity));
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> UpdatePortionAsync(SnSetblogDto entity, string type)
    {
        //var res= _mapper.Map<SnSetBlog>(entity);
        var resulet = await _service.SnSetblogs.FindAsync(entity.Id);
        if (resulet == null) return false;
        switch (type)
        {    //指定字段进行更新操作
            case "type":
                //修改属性，被追踪的Read状态属性就会变为Modify
                resulet.Isopen = entity.Isopen;
                break;
        }
        ////执行数据库操作
        return await _service.SaveChangesAsync() > 0;
    }

    public Task<List<SnSetblog>> GetAllAsync(bool cache)
    {
        throw new NotImplementedException();
    }

    public Task<List<ArticleDto>> GetContainsAsync(string name, bool cache)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetSumAsync(string type, bool cache)
    {
        throw new NotImplementedException();
    }

    public async Task<SnSetblogDto> GetByIdAsync(int id, bool cache)
    {
        Log.Information("SnSetBlogDto主键查询=>" + id + cache);
        // _resDto.Entity = _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, _resDto.Entity, cache);
        if (_resDto.Entity == null)
        {
            _resDto.Entity = _mapper.Map<SnSetblogDto>(await _service.SnSetblogs.FindAsync(id));
            // _cacheutil.CacheString("GetByIdAsync_SnSetBlogDto" + id + cache, _resDto.Entity, cache);
        }
        return _resDto.Entity;
    }

    

    public Task<int> GetConutSortAsync(int type, bool cache)
    {
        throw new NotImplementedException();
    }

    public int GetTypeCountAsync(int type, bool cache)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCountAsync(int identity, string type, bool cache)
    {
        Log.Information("SnSetBlogDto查询总数=>" + cache);
        // _res.EntityCount = _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, _res.EntityCount, cache);
        if (_res.EntityCount == 0)
        {
            switch (identity)
            {
                case 0:
                    _res.EntityCount = await _service.SnSetblogs.AsNoTracking().CountAsync();
                    break;
                case 1:
                    _res.EntityCount = await _service.SnSetblogs.Where(w => w.Type.Name == type).AsNoTracking().CountAsync();
                    break;
                case 2:
                    _res.EntityCount = await _service.SnSetblogs.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                    break;
            }
            // _cacheutil.CacheNumber("CountAsync_SnSetBlogDto" + cache, _res.EntityCount, cache);
        }
        return _res.EntityCount;
    }
}