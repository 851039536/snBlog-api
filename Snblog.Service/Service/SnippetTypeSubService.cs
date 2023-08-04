namespace Snblog.Service.Service
{
    public class SnippetTypeSubService : ISnippetTypeSubService
    {
        private readonly SnblogContext _service;
        private readonly CacheUtil _cache;

        private readonly EntityData<SnippetTypeSub> _ret = new();
        private readonly EntityDataDto<SnippetTypeSubDto> _rDto = new();
        private readonly IMapper _mapper;

        const string NAME = "SnippetTypeSub_";

        public SnippetTypeSubService(SnblogContext service,ICacheUtil cache,IMapper mapper)
        {
            _service = service;
            _cache = (CacheUtil)cache;
            _mapper = mapper;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            Common.CacheInfo($"{NAME}{Common.Del}{id}");

            var ret = await _service.SnippetTypes.FindAsync(id);
            if (ret == null) return false;
            _service.SnippetTypes.Remove(ret);
            return await _service.SaveChangesAsync() > 0;
        }


        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<SnippetTypeSubDto> GetByIdAsync(int id,bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Bid}{id}_{cache}");
            if (cache)
            {
                _rDto.Entity = _cache.GetValue<SnippetTypeSubDto>(Common.CacheKey);
                if (_rDto.Entity != null) return _rDto.Entity;
            }
            var ret = await _service.SnippetTypeSubs.FindAsync(id);
            _rDto.Entity = _mapper.Map<SnippetTypeSubDto>(ret);
            _cache.SetValue(Common.CacheKey,_rDto.Entity);
            return _rDto.Entity;
        }


        /// <summary>
        ///  添加 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(SnippetTypeSub entity)
        {
            Common.CacheInfo($"{NAME}{Common.Add}{entity}");

            await _service.SnippetTypeSubs.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(SnippetTypeSub entity)
        {
            Common.CacheInfo($"{NAME}{Common.Up}{entity}");
            _service.SnippetTypeSubs.Update(entity);
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
        public async Task<List<SnippetTypeSubDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Paging}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetTypeSubDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            if (isDesc)
            {
                _rDto.EntityList = _mapper.Map<List<SnippetTypeSubDto>>(await _service.SnippetTypeSubs
                    .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            } else
            {
                _rDto.EntityList = _mapper.Map<List<SnippetTypeSubDto>>(await _service.SnippetTypeSubs.OrderBy(c => c.Id)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            _cache.SetValue(Common.CacheKey,_rDto.EntityList);
            return _rDto.EntityList;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetTypeSubDto>> GetAllAsync(bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.All}{cache}");

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetTypeSubDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            _rDto.EntityList =
                _mapper.Map<List<SnippetTypeSubDto>>(await _service.SnippetTypeSubs.AsNoTracking().ToListAsync());
            _cache.SetValue(Common.CacheKey,_rDto.EntityList);
            return _rDto.EntityList;
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Sum}{cache}");

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue<int>(Common.CacheKey);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            _ret.EntityCount = await _service.SnippetTypeSubs.AsNoTracking().CountAsync();
            _cache.SetValue(Common.CacheKey,_ret.EntityCount);

            return _ret.EntityCount;
        }

        /// <summary>
        /// 根据主表类别id查询
        /// </summary>
        /// <param name="snippetTypeId"></param>
        /// <returns></returns>
        public async Task<List<SnippetTypeSubDto>> GetCondition(int snippetTypeId,bool cache)
        {
            Common.CacheInfo($"{NAME}{Common.Condition}{cache}");

            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetTypeSubDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            _rDto.EntityList =
                _mapper.Map<List<SnippetTypeSubDto>>(await _service.SnippetTypeSubs.Where(w => w.TypeId == snippetTypeId).AsNoTracking().ToListAsync());
            _cache.SetValue(Common.CacheKey,_rDto.EntityList);
            return _rDto.EntityList;
        }
    }
}