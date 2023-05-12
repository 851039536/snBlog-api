namespace Snblog.Service.Service
{
    public class ArticleService : IArticleService
    {
        // 常量字符串。这些常量字符串可以在代码中多次使用，而不必担心它们的值会被修改。
        const string NAME = "article_";

        /// <summary>
        /// 缓存Key
        /// </summary>
        private string cacheKey;
        private readonly EntityData<Article> res = new();
        private readonly EntityDataDto<ArticleDto> resDto = new();

        //服务
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private readonly IMapper _mapper;

        /// <summary>
        /// 使用了IServiceProvider接口来获取所需的服务
        ///它定义了一个方法GetRequiredService，该方法可以用于获取指定类型的服务
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ArticleService(IServiceProvider serviceProvider)
        {
            _service = serviceProvider.GetRequiredService<snblogContext>();
            _cache = (CacheUtil)serviceProvider.GetRequiredService<ICacheUtil>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<bool> DelAsync(int id)
        {
            cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(cacheKey);

            Article reslult = await _service.Articles.FindAsync(id);

            if (reslult == null) return false;

            _service.Articles.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型

            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<ArticleDto> GetByIdAsync(int id,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.BYID}{id}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.Entity = _cache.GetValue(cacheKey,resDto.Entity);
                if (resDto.Entity != null) return resDto.Entity;
            }

            resDto.Entity = await _service.Articles
                .SelectArticle()
                .SingleOrDefaultAsync(b => b.Id == id);
            _cache.SetValue(cacheKey,resDto.Entity);
            return resDto.Entity;
        }
        public async Task<List<ArticleDto>> GetTypeAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{identity}_{type}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList != null) {
                    return resDto.EntityList;
                }
            }

            if (identity == 1) { //分类
                resDto.EntityList = await _service.Articles
                    .SelectArticle()
                    .Where(s => s.Type.Name == type)
                    .ToListAsync();
            } else { //2 标签
                resDto.EntityList = await _service.Articles
                    .SelectArticle()
                    .Where(s => s.Tag.Name == type)
                    .ToListAsync();
            }
            _cache.SetValue(cacheKey,resDto.EntityList);
            return resDto.EntityList;
        }

        public async Task<bool> AddAsync(Article entity)
        {
            Log.Information($"{NAME}{ConstantString.ADD}{entity}");

            entity.TimeCreate = entity.TimeModified = DateTime.Now;
            //此方法中的异步添加改为同步添加，因为 SaveChangesAsync 方法已经是异步的，不需要再使用异步添加
            _service.Articles.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Article entity)
        {

            Log.Information($"{NAME}{ConstantString.UP}{entity}");

            entity.TimeModified = DateTime.Now; //更新时间

            var res = await _service.Articles.Where(w => w.Id == entity.Id).Select(
                s => new {
                    s.TimeCreate,
                }
                ).ToListAsync();
            entity.TimeCreate = res[0].TimeCreate;  //赋值表示更新时间不变
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


        public async Task<int> GetSumAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.SUM}{identity}_{type}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                int sum =  _cache.GetValue(cacheKey,0);
                if (sum != 0) {  //通过entityInt 值是否为 0 判断结果是否被缓存
                    return sum;
                }
            }

            return identity switch { // case 
                0 => await GetArticleCountAsync(),// 读取文章数量，无需筛选条件
                1 => await GetArticleCountAsync(c => c.Type.Name == type),
                2 => await GetArticleCountAsync(c => c.Tag.Name == type),
                3 => await GetArticleCountAsync(c => c.User.Name == type),
                _ => -1, //default
            };
        }


        /// <summary>
        /// 获取文章的数量
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        /// <returns>返回文章的数量</returns>
        private async Task<int> GetArticleCountAsync(Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> query = _service.Articles.AsNoTracking();

            //如果有筛选条件
            if (predicate != null) query = query.Where(predicate);

            try {
                int count = await query.CountAsync();
                _cache.SetValue(cacheKey,count); //设置缓存
                return count;
            } catch {
                return -1;
            }
        }

        public async Task<List<ArticleDto>> GetAllAsync(bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.ALL}{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList != null) return resDto.EntityList;
            }
            var data = await _service.Articles.SelectArticle().ToListAsync();
            _cache.SetValue(cacheKey,resDto.EntityList);
            return data;
        }

        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetStrSumAsync(int identity,int type,string name,bool cache)
        {
            cacheKey = $"{NAME}统计{identity}_{type}_{name}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                res.EntityCount = _cache.GetValue(cacheKey,res.EntityCount);
                if (res.EntityCount != 0) {
                    return res.EntityCount;
                }
            }

            switch (identity) {
                case 0:
                res.EntityCount = await GetStatistic(type);
                break;
                case 1:
                res.EntityCount = await GetStatistic(type,c => c.Type.Name == name);
                break;
                case 2:
                res.EntityCount = await GetStatistic(type,c => c.Tag.Name == name);
                break;
                case 3:
                res.EntityCount = await GetStatistic(type,c => c.User.Name == name);
                break;
            }
            _cache.SetValue(cacheKey,res.EntityCount);
            return res.EntityCount;
        }

        /// <summary>
        /// 读取内容数量
        /// </summary>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <returns></returns>
        private async Task<int> GetStatistic(int type,Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> query = _service.Articles;

            if (predicate != null) {
                query = query.Where(predicate);
            }

            return type switch {
                1 => await query.SumAsync(c => c.Text.Length),
                2 => await query.SumAsync(c => c.Read),
                3 => await query.SumAsync(c => c.Give),
                _ => 0,
            };
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
        public async Task<List<ArticleDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList != null) {
                    return resDto.EntityList;
                }
            }
            switch (identity) {
                case 0:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc);
                case 1:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type);
                case 2:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Tag.Name == type);
                case 3:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type);
                case 4:
                resDto.Name = type.Split(',');
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Tag.Name == resDto.Name[0]
                    && w.User.Name == resDto.Name[1]);
                default:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc);
            }
        }

        private async Task<List<ArticleDto>> GetArticlePaging(int pageIndex,int pageSize,string ordering,bool isDesc,Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> articles = _service.Articles.AsQueryable();

            if (predicate != null) {
                articles = articles.Where(predicate);
            }
            switch (ordering) {
                case "id":
                articles = isDesc ? articles.OrderByDescending(c => c.Id) : articles.OrderBy(c => c.Id);
                break;
                case "data":
                articles = isDesc ? articles.OrderByDescending(c => c.TimeCreate) : articles.OrderBy(c => c.TimeCreate);
                break;
                case "read":
                articles = isDesc ? articles.OrderByDescending(c => c.Read) : articles.OrderBy(c => c.Read);
                break;
                case "give":
                articles = isDesc ? articles.OrderByDescending(c => c.Give) : articles.OrderBy(c => c.Give);
                break;
            }
            var data = await articles.Skip(( pageIndex - 1 ) * pageSize).Take(pageSize)
            .SelectArticle().ToListAsync();
            _cache.SetValue(cacheKey,resDto.EntityList);
            return data;
        }


        public async Task<bool> UpdatePortionAsync(Article entity,string type)
        {
            Log.Information("Article更新部分参数");
            var result = await _service.Articles.FindAsync(entity.Id);
            if (result == null) return false;

            switch (type) {    //指定字段进行更新操作
                case "Read":
                //修改属性，被追踪的league状态属性就会变为Modify
                result.Read = entity.Read;
                break;
                case "Give":
                result.Give = entity.Give;
                break;
                case "Comment":
                result.CommentId = entity.CommentId;
                break;
                default:
                return false;
            }
            //执行数据库操作
            await _service.SaveChangesAsync();
             //await _service.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<List<ArticleDto>> GetContainsAsync(int identity,string type,string name,bool cache)
        {
            var upNames = name.ToUpper();
            cacheKey = $"{NAME}{ConstantString.CONTAINS}{identity}_{type}_{name}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                resDto.EntityList = _cache.GetValue(cacheKey,resDto.EntityList);
                if (resDto.EntityList != null) {
                    return resDto.EntityList;
                }
            }
           
            return identity switch {
                0 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
                1 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Type.Name == type),
                2 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Tag.Name == type),
                _ => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
            };
        }
    
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        private async Task<List<ArticleDto>> GetArticleContainsAsync(Expression<Func<Article,bool>> predicate = null)
        {
            if (predicate != null) {
                resDto.EntityList = await _service.Articles.Where(predicate).SelectArticle().ToListAsync();
                _cache.SetValue(cacheKey,resDto.EntityList); //设置缓存
            }
            return resDto.EntityList;
        }
         
    }
}
