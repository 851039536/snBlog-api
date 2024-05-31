using Snblog.IService.IService.Snippets;

namespace Snblog.Service.Service.Snippets;

public class SnippetService : ISnippetService
{
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;
    private const string Name = "snippet_";

    public SnippetService(SnblogContext coreDbContext,ServiceHelper serviceHelper)
    {
        _service = coreDbContext;
        _serviceHelper = serviceHelper;
    }

    #region 查询总数

    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Sum}{identity}_{type}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await _service.Snippets.AsNoTracking().CountAsync(),
                1 => await _service.Snippets.AsNoTracking().CountAsync(c => c.Type.Name == type),
                2 => await _service.Snippets.AsNoTracking().CountAsync(c => c.Tag.Name == type),
                3 => await _service.Snippets.AsNoTracking().CountAsync(c => c.User.Name == type),
                var _ => -1
            };
        });
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询 
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    public async Task<SnippetDto> GetByIdAsync(int id,bool cache)
    {
       

        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return await _service.Snippets.SelectSnippet().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);
        });
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4|标题:5</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">缓存</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <returns>list-entity</returns>
    public async Task<List<SnippetDto>> GetContainsAsync(int identity,string type,string name,bool cache,
                                                         int pageIndex,int pageSize)
    {
        string uppercaseName = name.ToUpper();
        string cacheKey = $"{Name}{ServiceConfig.Contains}{identity}_{type}_{name}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => //所有 查分类,标题,内容
                    await Contains(pageIndex,pageSize,
                        w => w.Name.ToUpper().Contains(uppercaseName) || w.TypeSub.Name.ToUpper().Contains(uppercaseName) ||
                             w.Text.ToUpper().Contains(uppercaseName)),
                1 => await Contains(pageIndex,pageSize,l => l.Name.ToUpper().Contains(uppercaseName) && l.Type.Name == type),
                2 => await Contains(pageIndex,pageSize,l => l.Name.ToUpper().Contains(uppercaseName) && l.Tag.Name == type),
                3 => await Contains(pageIndex,pageSize,l => l.Name.ToUpper().Contains(uppercaseName) && l.User.Name == type),
                4 => await Contains(pageIndex,pageSize,l => l.Text.ToUpper().Contains(uppercaseName)),
                5 => await Contains(pageIndex,pageSize,l => l.Name.ToUpper().Contains(uppercaseName)),
                _ => await Contains(pageIndex,pageSize,
                    w => w.Name.ToUpper().Contains(uppercaseName) || w.TypeSub.Name.ToUpper().Contains(uppercaseName) ||
                         w.Text.ToUpper().Contains(uppercaseName))
            };
        });
    }

    private async Task<List<SnippetDto>> Contains(int pageIndex,int pageSize,Expression<Func<Snippet,bool>> predicate)
    {
        return await _service.Snippets.Where(predicate).OrderByDescending(c => c.Id)
                             .Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize)
                             .SelectSnippet().ToListAsync();
    }

    #endregion

    #region 内容统计

    /// <summary>
    /// 内容统计
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
    /// <param name="name">查询参数</param>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    public async Task<int> GetStrSumAsync(int identity,string name,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Statistics}_{identity}_{name}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            switch(identity)
            {
            case 0:
                var all = await _service.Snippets.Select(c => c.Text).ToListAsync();
                return all.Sum(t => t.Length);

            case 1:
                var types = await _service.Snippets.Where(w => w.Type.Name == name).Select(c => c.Text)
                                          .ToListAsync();
                return types.Sum(t => t.Length);
            case 2:
                var tags = await _service.Snippets.Where(w => w.Tag.Name == name)
                                         .Select(c => c.Text).ToListAsync();
                return tags.Sum(t => t.Length);
            case 3:
                var users = await _service.Snippets.Where(w => w.User.Name == name).Select(c => c.Text)
                                          .ToListAsync();
                return users.Sum(t => t.Length);
            }

            return -1;
        });
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|用户名:3|子标签:4</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public async Task<List<SnippetDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            switch(identity)
            {
            case 0:
                // 调用通用分页方法
                return await Paging(pageIndex,pageSize,isDesc);
            case 1:
                // 根据类型和排序方式获取片段列表
                if(isDesc)
                {
                    return await _service.Snippets
                                         .Where(w => w.Type.Name == type)
                                         .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                         .Take(pageSize).SelectSnippet().ToListAsync();
                }

                return await _service.Snippets
                                     .Where(w => w.Type.Name == type)
                                     .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                     .Take(pageSize).SelectSnippet().ToListAsync();

            case 3:
               return await PagingUser(type,pageIndex,pageSize,isDesc);
            case 4:
                if(isDesc)
                {
                    return await _service.Snippets
                                         .Where(w => w.TypeSub.Name == type)
                                         .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                         .Take(pageSize).SelectSnippet().ToListAsync();
                }

                return await _service.Snippets
                                     .Where(w => w.TypeSub.Name == type)
                                     .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                     .Take(pageSize).SelectSnippet().ToListAsync();
            }

            return null;
        });
    }

    private async Task<List<SnippetDto>> PagingUser(string type,int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc)
        {
           return await _service.Snippets.Where(w => w.User.Name == type)
                                             .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                             .Take(pageSize).SelectSnippet().ToListAsync();
        }
        return await _service.Snippets.Where(w => w.User.Name == type)
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).SelectSnippet().ToListAsync();
    }

    private async Task<List<SnippetDto>> Paging(int pageIndex,int pageSize,bool isDesc)
    {
        if(isDesc)
        {
            return await _service.Snippets.OrderByDescending(c => c.Id)
                                 .Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize).SelectSnippet().ToListAsync();
        }

        return await _service.Snippets
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).SelectSnippet().ToListAsync();
    }

    #endregion

    public async Task<bool> DeleteAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");

        var snippet = await _service.Snippets.FindAsync(id);
        if(snippet == null) return false;
        _service.Snippets.Remove(snippet); //删除单个
        _service.Remove(snippet); //直接在context上Remove()方法传入model，它会判断类型
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> AddAsync(Snippet entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        await _service.Snippets.AddAsync(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Snippet entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity.Id}_{entity}");
        //entity.TimeModified = DateTime.Now; //更新时间
        _service.Snippets.Update(entity);
        return await _service.SaveChangesAsync() > 0;
    }


    public async Task<bool> UpdatePortionAsync(Snippet entity,string type)
    {
        Log.Information($"{Name}{ServiceConfig.Paging} {entity.Id}_{type}");
        var ret = await _service.Snippets.FindAsync(entity.Id);
        if(ret == null) return false;
        switch(type)
        {
        //指定字段进行更新操作
        case "text":
            //修改属性，被追踪的league状态属性就会变为Modify
            ret.Text = entity.Text;
            break;
        case "name":
            ret.Name = entity.Name;
            break;
        }

        //执行数据库操作
        return await _service.SaveChangesAsync() > 0;
    }
}