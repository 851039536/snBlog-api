namespace Snblog.Service.Service
{
    public class ArticleTagService : IArticleTagService
    {
        const string NAME = "articleTag_";

        /// <summary>
        /// 缓存Key
        /// </summary>
        private string _cacheKey;

        private readonly EntityData<ArticleTag> _res = new();
        private readonly EntityDataDto<ArticleTagDto> _rDto = new();

        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private readonly IMapper _mapper;


        public ArticleTagService(snblogContext service, ICacheUtil cacheutil, IMapper mapper)
        {
            _service = service;
            _cache = (CacheUtil)cacheutil;
            _mapper = mapper;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            _cacheKey = $"{NAME}{Common.Del}{id}";
            Log.Information(_cacheKey);

            ArticleTag result = await _service.ArticleTags.FindAsync(id);

            if (result == null) return false;

            _service.ArticleTags.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<ArticleTagDto> GetByIdAsync(int id, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Bid}{id}_{cache}";
            Log.Information($"{NAME}{Common.Bid}{id}{cache}");

            if (cache)
            {
                _rDto.Entity = _cache.GetValue(_cacheKey, _rDto.Entity);
                if (_res.Entity != null) return _rDto.Entity;
            }

            _rDto.Entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
            _cache.SetValue(_cacheKey, _rDto.Entity);
            return _rDto.Entity;
        }

        public async Task<bool> AddAsync(ArticleTag entity)
        {
            Log.Information(_cacheKey);

            _service.ArticleTags.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ArticleTag entity)
        {
            Log.Information($"{NAME}{Common.Up}{entity}");

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
        public async Task<List<ArticleTagDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _cacheKey = $"{NAME}{Common.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue(_cacheKey, _rDto.EntityList);
                if (_res.EntityList != null) return _rDto.EntityList;
            }

            await QPaging(pageIndex, pageSize, isDesc);
            _cache.SetValue(_cacheKey, _rDto.EntityList);
            return _rDto.EntityList;
        }

        private async Task QPaging(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
            else
            {
                _rDto.EntityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderBy(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
        }


        public async Task<int> GetSumAsync(bool cache)
        {
            _cacheKey = $"{NAME}{Common.Sum}{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _res.EntityCount = _cache.GetValue(_cacheKey, _res.EntityCount);
                if (_res.EntityCount != 0) return _res.EntityCount;
            }

            _res.EntityCount = await _service.ArticleTags.AsNoTracking().CountAsync();
            _cache.SetValue(_cacheKey, _res.EntityCount);
            return _res.EntityCount;
        }
    }
}