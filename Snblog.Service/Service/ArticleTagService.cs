using Microsoft.Extensions.Logging;
using Snblog.IRepository;
using Snblog.IService;

namespace Snblog.Service.Service
{
    public class ArticleTagService : BaseService, IArticleTagService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly EntityData<ArticleTag> res = new();
        private readonly EntityDataDto<ArticleTagDto> rDto = new();
        private readonly ILogger<ArticleTag> _logger;
        private readonly IMapper _mapper;

        // 常量字符串。这些常量字符串可以在代码中多次使用，而不必担心它们的值会被修改。
        const string NAME = "ArticleTag_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";

        public ArticleTagService(IRepositoryFactory repositoryFactory,IConcardContext mydbcontext,snblogContext service,ICacheUtil cacheutil,ILogger<ArticleTag> logger,IMapper mapper) : base(repositoryFactory,mydbcontext)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _service.ArticleTags.FindAsync(id);
            if (result == null) {
                return false;
            }
            _service.ArticleTags.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<ArticleType>> AsyGetSort()
        {
            var data = CreateService<ArticleType>();
            return await data.GetAll().ToListAsync();
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<ArticleTagDto> GetByIdAsync(int id,bool cache)
        {
            Log.Information($"{NAME}{BYID}{id}{cache}");
            rDto.Entity = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}{id}",rDto.Entity,cache);
            if (res.Entity != null) return rDto.Entity;
            rDto.Entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
            _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}",rDto.Entity,cache);
            return rDto.Entity;
        }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(ArticleTag entity)
        {
            await _service.ArticleTags.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ArticleTag entity)
        {
            _service.ArticleTags.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<ArticleTagDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
        {
            Log.Information($"{NAME}{PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            rDto.EntityList = _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",rDto.EntityList,cache);
            if (res.EntityList != null) return rDto.EntityList;
            await QPaging(pageIndex,pageSize,isDesc);
            _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",rDto.EntityList,cache);
            return rDto.EntityList;
        }

        private async Task QPaging(int pageIndex,int pageSize,bool isDesc)
        {
            if (isDesc) {
                rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
            } else {
                rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
            }
        }

        public async Task<List<ArticleTagDto>> GetAllAsync(bool cache)
        {
            Log.Information($"{NAME}{ALL}",cache);
            rDto.EntityList = _cacheutil.CacheString($"{NAME}{SUM}{cache}",rDto.EntityList,cache);
            if (rDto.EntityList != null) return rDto.EntityList;
            rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.AsNoTracking().ToListAsync());
            _cacheutil.CacheString($"{NAME}{SUM}{cache}",rDto.EntityList,cache);
            return rDto.EntityList;
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
        {
            Log.Information($"{NAME}{SUM}" + cache);
            res.EntityCount = _cacheutil.CacheNumber($"{NAME}{SUM}{cache}",res.EntityCount,cache);
            if (res.EntityCount != 0) return res.EntityCount;
            res.EntityCount = await _service.ArticleTags.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber($"{NAME}{SUM}{cache}",res.EntityCount,cache);
            return res.EntityCount;
        }
    }
}
