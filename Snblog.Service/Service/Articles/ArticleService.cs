using Snblog.IService.IService.Articles;
using Snblog.Service.pollys;

namespace Snblog.Service.Service.Articles;

public class ArticleService : IArticleService
{
    #region 服务

    // 常量字符串。这些常量字符串可以在代码中多次使用，而不必担心它们的值会被修改。
    private const string Name = "Article_";
    private readonly SnblogContext _service;
    private readonly ServiceHelper _serviceHelper;
    private readonly RetryPolicyService _retryPolicyService;

    #endregion

    #region 构造函数

    ///  <summary>
    ///  使用了IServiceProvider接口来获取所需的服务
    /// 它定义了一个方法GetRequiredService，该方法可以用于获取指定类型的服务
    ///  </summary>
    ///  <param name="serviceProvider"></param>
    ///  <param name="serviceHelper"></param>
    ///  <param name="retryPolicyService"></param>
    public ArticleService(IServiceProvider serviceProvider,ServiceHelper serviceHelper,RetryPolicyService retryPolicyService)
    {
        _serviceHelper = serviceHelper;
        _retryPolicyService = retryPolicyService;
        // 获取服务提供程序中的实例
        _service = serviceProvider.GetRequiredService<SnblogContext>();
    }

    #endregion

    #region 查询总数
    
   

    public async Task<int> GetSumAsync(int identity,string type,bool cache)
    {
        // 1.先赋值缓存key
        string cacheKey = $"{Name}{ServiceConfig.Sum}{identity}_{type}_{cache}";

        // 使用注入的重试策略
        var retryPolicy = _retryPolicyService.CreateRetryPolicy<int>();
      
            return await retryPolicy.ExecuteAsync(async () =>
            {
                // 2.调用通用方法：检查是否需要缓存，并执行相应的逻辑
                return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
                {
                    // 3.根据当前方法执行逻辑
                    return identity switch
                    {
                        // 读取文章数量，无需筛选条件
                        0 => await GetArticleCountAsync(),
                        1 => await GetArticleCountAsync(c => c.Type.Name == type),
                        2 => await GetArticleCountAsync(c => c.Tag.Name == type),
                        3 => await GetArticleCountAsync(c => c.User.Name == type),
                        _ => throw new InvalidOperationException("Invalid identity value."), // 更改为抛出具体异常以触发重试
                    };
                });
            });
      
    }

    /// <summary>
    /// 获取文章的数量
    /// </summary>
    /// <param name="predicate">筛选文章的条件</param>
    /// <returns>返回文章的数量</returns>
    private async Task<int> GetArticleCountAsync(Expression<Func<Article,bool>> predicate = null)
    {
        var query = _service.Articles.AsNoTracking();
        //如果有筛选条件
        if(predicate != null) query = query.Where(predicate);
        try
        {
            int count = await query.CountAsync();
            return count;
        }
        catch
        {
            return -1;
        }
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 执行模糊查询操作，根据不同的标识和类型筛选文章。
    /// </summary>
    /// <param name="identity">查询的标识类型，具体值含义：所有:0|分类:1|标签:2|用户:3|标签,用户:4</param>
    /// <param name="type">查询的类型参数，用于进一步筛选文章。多条件以','分割。</param>
    /// <param name="name">查询的关键字段，用于模糊匹配文章名称。</param>
    /// <param name="cache">是否使用缓存。如果为true，则优先从缓存中获取结果。</param>
    /// <returns>包含匹配文章的列表。</returns>
    public async Task<List<ArticleDto>> GetContainsAsync(int identity,string type,string name,bool cache)
    {
        string upNames = name.ToUpper();

        string cacheKey = $"{Name}{ServiceConfig.Contains}{identity}_{type}_{name}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            return identity switch
            {
                0 => await Contains(l => l.Name.ToUpper().Contains(upNames)),
                1 => await Contains(l => l.Name.ToUpper().Contains(upNames) && l.Type.Name == type),
                2 => await Contains(l => l.Name.ToUpper().Contains(upNames) && l.Tag.Name == type),
                var _ => await Contains(l => l.Name.ToUpper().Contains(upNames)),
            };
        });
    }

    /// <summary>
    /// 执行模糊查询操作，根据提供的条件筛选文章。
    /// </summary>
    /// <param name="predicate">筛选文章的条件表达式。如果为null，则返回所有文章。</param>
    private async Task<List<ArticleDto>> Contains(Expression<Func<Article,bool>> predicate)
    {
        var ret = await _service.Articles.Where(predicate).SelectArticle().ToListAsync();
        return ret;
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>ArticleDto对象</returns>
    public async Task<ArticleDto> GetByIdAsync(int id,bool cache)
    {
        // 生成缓存键，结合服务名称、配置中的Bid、主键id以及是否使用缓存的标志
        string cacheKey = $"{Name}{ServiceConfig.Bid}{id}_{cache}";

        // 调用服务助手的方法来检查并执行缓存逻辑
        // 如果cache为true，则检查缓存中是否存在该数据，如果不存在则从数据库查询并存入缓存
        // 如果cache为false，则直接从数据库查询，不使用缓存
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            // 使用服务层的Articles集合，通过SelectArticle方法选择需要的字段
            // 使用SingleOrDefaultAsync方法查询Id等于传入id的文章
            var ret = await _service.Articles
                                    .SelectArticle()
                                    .SingleOrDefaultAsync(b => b.Id == id);
            return ret;
        });
    }

    #endregion

    #region 类别查询

    public async Task<List<ArticleDto>> GetTypeAsync(int identity,string type,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Condition}{identity}_{type}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            if(identity == 1)
            {
                //分类,bug读取空值
                var ret = await _service.Articles
                                        .SelectArticle()
                                        .Where(s => s.Type.Name == type)
                                        .ToListAsync();
                return ret;
            } else
            {
                //2 标签
                var ret = await _service.Articles
                                        .SelectArticle()
                                        .Where(s => s.Tag.Name == type)
                                        .ToListAsync();
                return ret;
            }
        });
    }

    #endregion

    #region 内容统计

    /// <summary>
    /// 内容统计
    /// </summary>
    /// <param name="identity">统计的类型：所有:0|分类:1|标签:2|用户:3</param>
    /// <param name="type">统计的内容类型：内容:1|阅读:2|点赞:3</param>
    /// <param name="name">查询参数，如分类名、标签名或用户名</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>统计结果，整数类型</returns>
    public async Task<int> GetStrSumAsync(int identity,int type,string name,bool cache)
    {
        string cacheKey = $"{Name}统计{identity}_{type}_{name}_{cache}";
        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            int ret = identity switch
            {
                0 => await GetStatistic(type),
                1 => await GetStatistic(type,c => c.Type.Name == name),
                2 => await GetStatistic(type,c => c.Tag.Name == name),
                3 => await GetStatistic(type,c => c.User.Name == name),
                _ => 0
            };
            return ret;
        });
    }

    /// <summary>
    /// 读取内容数量
    /// </summary>
    /// <param name="type">内容:1|阅读:2|点赞:3</param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    private async Task<int> GetStatistic(int type,Expression<Func<Article,bool>> predicate = null)
    {
        IQueryable<Article> query = _service.Articles;

        if(predicate != null)
        {
            query = query.Where(predicate);
        }

        return type switch
        {
            1 => await query.SumAsync(c => c.Text.Length),
            2 => await query.SumAsync(c => c.Read),
            3 => await query.SumAsync(c => c.Give),
            _ => 0,
        };
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">查询的类型：所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
    /// <param name="type">查询参数，多条件以','分割</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="ordering">排序规则：data:时间|read:阅读|give:点赞|id:主键</param>
    /// <param name="isDesc">排序方式：true为降序，false为升序</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>分页查询结果，List ArticleDto类型</returns>
    public async Task<List<ArticleDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,
                                                       string ordering,bool isDesc,bool cache)
    {
        string cacheKey = $"{Name}{ServiceConfig.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";

        return await _serviceHelper.CheckAndExecuteCacheAsync(cacheKey,cache,async () =>
        {
            switch(identity)
            {
            case 0:
                return await GetPaging(pageIndex,pageSize,ordering,isDesc);
            case 1:
                return await GetPaging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type);
            case 2:
                return await GetPaging(pageIndex,pageSize,ordering,isDesc,w => w.Tag.Name == type);
            case 3:
                return await GetPaging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type);
            case 4:
                string[] name = type.Split(',');
                return await GetPaging(pageIndex,pageSize,ordering,isDesc,w =>
                    w.Tag.Name == name[0]
                    && w.User.Name == name[1]);
            default:
                return await GetPaging(pageIndex,pageSize,ordering,isDesc);
            }
        });
    }

    private async Task<List<ArticleDto>> GetPaging(int pageIndex,int pageSize,string ordering,bool isDesc,
                                                   Expression<Func<Article,bool>> predicate = null)
    {
        var article = _service.Articles.AsQueryable();
        if(predicate != null) article = article.Where(predicate);

        article = ordering switch
        {
            "id" => isDesc ? article.OrderByDescending(c => c.Id) : article.OrderBy(c => c.Id),
            "data" => isDesc ? article.OrderByDescending(c => c.TimeCreate) : article.OrderBy(c => c.TimeCreate),
            "read" => isDesc ? article.OrderByDescending(c => c.Read) : article.OrderBy(c => c.Read),
            "give" => isDesc ? article.OrderByDescending(c => c.Give) : article.OrderBy(c => c.Give),
            _ => article
        };

        var ret = await article.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                               .SelectArticle().ToListAsync();
        return ret;
    }

    #endregion

    #region 新增

    public async Task<bool> AddAsync(Article entity)
    {
        Log.Information($"{Name}{ServiceConfig.Add}{entity}");
        entity.TimeCreate = entity.TimeModified = DateTime.Now;
        _service.Articles.Add(entity);
        return await _service.SaveChangesAsync() > 0;
    }

    #endregion

    #region 更新

    public async Task<bool> UpdateAsync(Article entity)
    {
        Log.Information($"{Name}{ServiceConfig.Up}{entity}");

        entity.TimeModified = DateTime.Now; //更新时间

        var ret = await _service.Articles.Where(w => w.Id == entity.Id).Select(
            s => new { s.TimeCreate, }
        ).ToListAsync();
        entity.TimeCreate = ret[0].TimeCreate; //赋值表示更新时间不变
        _service.Articles.Update(entity);
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


    public async Task<bool> UpdatePortionAsync(Article entity,string type)
    {
        Log.Information($"{Name}{ServiceConfig.UpPortiog}{entity.Id}_{type}");
        var ret = await _service.Articles.FindAsync(entity.Id);
        if(ret == null) return false;

        switch(type)
        {
        //指定字段进行更新操作
        case "Read":
            //修改属性，被追踪的league状态属性就会变为Modify
            ret.Read = entity.Read;
            break;
        case "Give":
            ret.Give = entity.Give;
            break;
        case "Comment":
            ret.CommentId = entity.CommentId;
            break;
        default:
            return false;
        }

        //执行数据库操作
        await _service.SaveChangesAsync();
        //await _service.SaveChangesAsync() > 0;
        return true;
    }

    #endregion


    #region 删除

    public async Task<bool> DelAsync(int id)
    {
        Log.Information($"{Name}{ServiceConfig.Del}{id}");
        // 通过id查找文章
        var ret = await _service.Articles.FindAsync(id);
        // 如果文章不存在，返回false
        if(ret == null) return false;
        _service.Articles.Remove(ret); //删除单个
        _service.Remove(ret); //直接在context上Remove()方法传入model，它会判断类型
        // 保存更改
        return await _service.SaveChangesAsync() > 0;
    }

    #endregion
}