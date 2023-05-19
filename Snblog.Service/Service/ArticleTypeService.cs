using Microsoft.Extensions.Logging;

namespace Snblog.Service.Service
{
    public class ArticleTypeService : BaseService, IArticleTypeService
        {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;

        private readonly EntityData<ArticleType> res = new();
        private readonly EntityDataDto<ArticleTypeDto> resDto = new();
        private readonly ILogger<ArticleType> _logger;
        private readonly IMapper _mapper;

        const string NAME = "ArticleType_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public ArticleTypeService(IRepositoryFactory repositoryFactory,IConcardContext mydbcontext,snblogContext service,ICacheUtil cacheutil,ILogger<ArticleType> logger,IMapper mapper) : base(repositoryFactory,mydbcontext)
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
            var result = await _service.ArticleTypes.FindAsync(id);
            if (result == null) {
                return false;
                }
            _service.ArticleTypes.Remove(result);
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
        public async Task<ArticleTypeDto> GetByIdAsync(int id,bool cache)
            {
            Log.Information($"{NAME}{BYID}{id}{cache}");
            resDto.Entity = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}{id}",resDto.Entity,cache);
            if (res.Entity != null) return resDto.Entity;
            resDto.Entity = _mapper.Map<ArticleTypeDto>(await _service.ArticleTypes.FindAsync(id));
            _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}",resDto.Entity,cache);
            return resDto.Entity;
            }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(ArticleType entity)
            {
            await _service.ArticleTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<bool> UpdateAsync(ArticleType entity)
            {
            _service.ArticleTypes.Update(entity);
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
        public async Task<List<ArticleTypeDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
            {
            Log.Information($"{NAME}{PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            resDto.EntityList = _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",resDto.EntityList,cache);
            if (res.EntityList != null) return resDto.EntityList;
            await QPaging(pageIndex,pageSize,isDesc);
            _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",resDto.EntityList,cache);
            return resDto.EntityList;
            }

        private async Task QPaging(int pageIndex,int pageSize,bool isDesc)
            {
            if (isDesc) {
                resDto.EntityList = _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
                } else {
                resDto.EntityList = _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
                }
            }

        public async Task<List<ArticleTypeDto>> GetAllAsync(bool cache)
            {
            Log.Information($"{NAME}{ALL}",cache);
            resDto.EntityList = _cacheutil.CacheString($"{NAME}{SUM}{cache}",resDto.EntityList,cache);
            if (resDto.EntityList != null) return resDto.EntityList;
            resDto.EntityList = _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.AsNoTracking().ToListAsync());
            _cacheutil.CacheString($"{NAME}{SUM}{cache}",resDto.EntityList,cache);
            return resDto.EntityList;
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
            res.EntityCount = await _service.ArticleTypes.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber($"{NAME}{SUM}{cache}",res.EntityCount,cache);
            return res.EntityCount;
            }
        }
    }
