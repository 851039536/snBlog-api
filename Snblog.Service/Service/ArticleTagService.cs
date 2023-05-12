using Snblog.IService;

namespace Snblog.Service.Service
{
    public class ArticleTagService : IArticleTagService
    {
        const string NAME = "articleTag_";
        /// <summary>
        /// 缓存Key
        /// </summary>
        private string cacheKey;

        private readonly EntityData<ArticleTag> res = new();
        private readonly EntityDataDto<ArticleTagDto> rDto = new();

        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private readonly IMapper _mapper;


        public ArticleTagService(snblogContext service,ICacheUtil cacheutil,IMapper mapper) 
        {
            _service = service;
            _cache = (CacheUtil)cacheutil;
            _mapper = mapper;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(cacheKey);

            ArticleTag result = await _service.ArticleTags.FindAsync(id);
            if (result == null) return false;

            _service.ArticleTags.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<ArticleTagDto> GetByIdAsync(int id,bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.BYID}{id}_{cache}";
            Log.Information($"{NAME}{ConstantString.BYID}{id}{cache}");

            if (cache) {
                rDto.Entity = _cache.GetValue(cacheKey,rDto.Entity);
                if (res.Entity != null) return rDto.Entity;
            }

            rDto.Entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
            _cache.SetValue(cacheKey,rDto.Entity);
            return rDto.Entity;
        }

        public async Task<bool> AddAsync(ArticleTag entity)
        {
            Log.Information(cacheKey);

             _service.ArticleTags.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ArticleTag entity)
        {
            Log.Information($"{NAME}{ConstantString.UP}{entity}");

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
            cacheKey = $"{NAME}{ConstantString.PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}";
            Log.Information(cacheKey);

            if (cache) {
                rDto.EntityList = _cache.GetValue(cacheKey,rDto.EntityList);
                if (res.EntityList != null) return rDto.EntityList;
            }

            await QPaging(pageIndex,pageSize,isDesc);
            _cache.SetValue(cacheKey,rDto.EntityList);
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


        public async Task<int> GetSumAsync(bool cache)
        {
            cacheKey = $"{NAME}{ConstantString.SUM}{cache}";
            Log.Information(cacheKey);

            if (cache) {
                res.EntityCount = _cache.GetValue(cacheKey,res.EntityCount);
                if (res.EntityCount != 0) return res.EntityCount;
            }

            res.EntityCount = await _service.ArticleTags.AsNoTracking().CountAsync();
            _cache.SetValue(cacheKey,res.EntityCount);
            return res.EntityCount;
        }
    }
}
