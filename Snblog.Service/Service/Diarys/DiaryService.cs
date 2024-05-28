using Snblog.IService.IService.Diarys;

namespace Snblog.Service.Service.Diarys;

public class DiaryService : IDiaryService
{
    private string _cacheKey;
    const string Name = "diary_";

    private readonly ServiceHelper _serviceHelper;

    private readonly EntityData<Diary> _ret = new();
    private readonly EntityDataDto<DiaryDto> _rDto = new();

    private readonly CacheUtils _cache;
    private readonly SnblogContext _service;


    public DiaryService(SnblogContext service,ICacheUtil cache,ServiceHelper serviceHelper)
    {
        _service = service;
        _serviceHelper = serviceHelper;
        _cache = (CacheUtils)cache;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
    /// <param name="type">条件(identity为0则null) </param>
    /// <param name="cache"></param>
    /// <returns>int</returns>
    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{identity}_{type}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await GetDiaryCountAsync(),
                1 => await GetDiaryCountAsync(w => w.Type.Name == type),
                2 => await GetDiaryCountAsync(w => w.User.Name == type),
                _ => throw new NotImplementedException(),
            };
        });
    }

    /// <summary>
    /// 获取文章的数量
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    /// <returns>返回文章的数量</returns>
    private async Task<int> GetDiaryCountAsync(Expression<Func<Diary,bool>> predicate = null)
    {
        var query = _service.Diaries.AsNoTracking();
        if(predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync();
    }

    #endregion

    #region 模糊查询

    public async Task<List<DiaryDto>> GetContainsAsync(int identity,string type,string name,bool cache)
    {
        string upNames = name.ToUpper();

        string cacheKey = $"{Name}{ServiceConfig.Contains}{identity}_{type}_{name}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
                1 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.Type.Name == type),
                2 => await GetDiaryContainsAsync(l => l.Name.ToUpper().Contains(name) && l.User.Name == type),
                _ => null,
            };
        });
    }

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="predicate">筛选条件</param>
    private async Task<List<DiaryDto>> GetDiaryContainsAsync(Expression<Func<Diary,bool>> predicate)
    {
        var query = _service.Diaries.AsNoTracking();
        return await query.Where(predicate).SelectDiary().ToListAsync();
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    public async Task<DiaryDto> GetByIdAsync(int id,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return await _service.Diaries.SelectDiary()
                                 .SingleOrDefaultAsync(b => b.Id == id);
        });
    }

    #endregion

    #region 统计字符数量

    public async Task<int> GetSumAsync(string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{type}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () => await GetSum(type));
    }
    private async Task<int> GetSum(string type)
    {
        int num = 0;
        //按类型查询
        switch(type)
        {
        case "read":
            var read = await _service.Diaries.Select(c => c.Read).ToListAsync();
            num += read.Sum();
            break;
        case "text":
            var text = await _service.Diaries.Select(c => c.Text).ToListAsync();
            num += text.Sum(t => t.Length);
            break;
        case "give":
            var give = await _service.Diaries.Select(c => c.Give).ToListAsync();
            num += give.Sum();
            break;
        }

        return num;
    }
    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
    /// <param name="type">类别参数, identity 0 可不填</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序[true/false]</param>
    /// <param name="cache">是否开启缓存</param>
    /// <param name="ordering">排序条件[data:时间 read:阅读 give:点赞 按id排序]</param>
    public async Task<List<DiaryDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
                                                     string ordering,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await GetPaging(pageIndex,pageSize,ordering,isDesc),
                1 => await GetPaging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type),
                2 => await GetPaging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type),
                _ => await GetPaging(pageIndex,pageSize,ordering,isDesc)
            };
        });
    }

    private async Task<List<DiaryDto>> GetPaging(int pageIndex,int pageSize,string ordering,bool isDesc,
                                                 Expression<Func<Diary,bool>> predicate = null)
    {
        var diary = _service.Diaries.AsQueryable();

        if(predicate != null)
        {
            diary = diary.Where(predicate);
        }

        switch(ordering)
        {
        case "id":
            diary = isDesc ? diary.OrderByDescending(c => c.Id) : diary.OrderBy(c => c.Id);
            break;
        case "data":
            diary = isDesc ? diary.OrderByDescending(c => c.TimeCreate) : diary.OrderBy(c => c.TimeCreate);
            break;
        case "read":
            diary = isDesc ? diary.OrderByDescending(c => c.Read) : diary.OrderBy(c => c.Read);
            break;
        case "give":
            diary = isDesc ? diary.OrderByDescending(c => c.Give) : diary.OrderBy(c => c.Give);
            break;
        }

        var data = await diary.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                              .SelectDiary().ToListAsync();
        return data;
    }

    #endregion

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DelAsync(int id)
    {
        _cacheKey = $"{Name}{ServiceConfig.Del}{id}";
        Log.Information(_cacheKey);

        var result = await _service.Diaries.FindAsync(id);
        if(result == null) return false;
        _service.Diaries.Remove(result);
        return await _service.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(Diary entity)
    {
        _cacheKey = $"{Name}{ServiceConfig.Add}{entity}";
        Log.Information(_cacheKey);
        _service.Diaries.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Diary entity)
    {
        _cacheKey = $"{Name}{ServiceConfig.Up}{entity}";
        Log.Information(_cacheKey);
        _service.Diaries.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePortionAsync(Diary entity,string typeName)
    {
        _cacheKey = $"{Name}{ServiceConfig.UpPortiog}{typeName}_{entity}";
        Log.Information(_cacheKey);

        var result = await _service.Diaries.FindAsync(entity.Id);
        if(result == null) return false;

        switch(typeName)
        {
        //指定字段进行更新操作
        case "give":
            //date.Property("OneGive").IsModified = true;
            result.Give = entity.Give;
            break;
        case "read":
            //date.Property("OneRead").IsModified = true;
            result.Read = entity.Read;
            break;
        }

        return await _service.SaveChangesAsync() > 0;
    }
}