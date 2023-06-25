namespace Snblog.Service.Service
{
    public class ArticleTypeService : BaseService, IArticleTypeService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;

        private readonly EntityData<ArticleType> _ret = new();
        private readonly EntityDataDto<ArticleTypeDto> _retDto = new();

        private readonly IMapper _mapper;

        /// <summary>
        /// 缓存Key
        /// </summary>
        private string _cacheKey;

        const string NAME = "ArticleType_";

        public ArticleTypeService(IRepositoryFactory repositoryFactory, IConcardContext myContext,
            snblogContext service, ICacheUtil cache, IMapper mapper) : base(repositoryFactory, myContext)
        {
            _service = service;
            _cache = (CacheUtil)cache;
            _mapper = mapper;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            // 设置缓存键
            _cacheKey = $"{NAME}{ConstantString.DEL}{id}";
            Log.Information(_cacheKey);

            var ret = await _service.ArticleTypes.FindAsync(id);
            if (ret == null) return false;

            _service.ArticleTypes.Remove(ret);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<ArticleTypeDto> GetByIdAsync(int id, bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.BYID}{id}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _retDto.Entity = _cache.GetValue(_cacheKey, _retDto.Entity);
                if (_retDto != null) return _retDto.Entity;
            }

            _retDto.Entity = _mapper.Map<ArticleTypeDto>(await _service.ArticleTypes.FindAsync(id));
            _cache.SetValue(_cacheKey, _retDto.Entity);
            return _retDto.Entity;
        }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(ArticleType entity)
        {
            Log.Information("{ArticleType}{Add}{@Entity}", NAME, ConstantString.ADD, entity);

            await _service.ArticleTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ArticleType entity)
        {
            Log.Information($"{NAME}{ConstantString.UP}{entity}");

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
        public async Task<List<ArticleTypeDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _retDto.EntityList = _cache.GetValue(
                    _cacheKey,
                    _retDto.EntityList);
                if (_ret.EntityList != null) return _retDto.EntityList;
            }

            await QPaging(pageIndex, pageSize, isDesc);
            _cache.SetValue(_cacheKey, _retDto.EntityList);
            return _retDto.EntityList;
        }

        private async Task QPaging(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                _retDto.EntityList = _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
            else
            {
                _retDto.EntityList = _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.OrderBy(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
        }

        public async Task<List<ArticleTypeDto>> GetAllAsync(bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.ALL}{cache}";
            Log.Information(_cacheKey);

            if (cache)
            {
                _retDto.EntityList =
                    _cache.GetValue(_cacheKey, _retDto.EntityList);
                if (_retDto.EntityList != null) return _retDto.EntityList;
            }

            _retDto.EntityList =
                _mapper.Map<List<ArticleTypeDto>>(await _service.ArticleTypes.AsNoTracking().ToListAsync());

            _cache.SetValue(_cacheKey, _retDto.EntityList);
            return _retDto.EntityList;
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
        {
            _cacheKey = $"{NAME}{ConstantString.SUM}{cache}";
            Log.Information(_cacheKey);
            if (cache)
            {
                _ret.EntityCount = _cache.GetValue(_cacheKey, _ret.EntityCount);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            _ret.EntityCount = await _service.ArticleTypes.AsNoTracking().CountAsync();
            _cache.SetValue(_cacheKey, _ret.EntityCount);
            return _ret.EntityCount;
        }
    }
}