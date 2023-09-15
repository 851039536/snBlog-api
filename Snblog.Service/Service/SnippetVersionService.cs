namespace Snblog.Service.Service
{
    public class SnippetVersionService : ISnippetVersionService
    {
        private readonly SnblogContext _service;
        private readonly CacheUtil _cache;
        private readonly EntityData<SnippetVersion> _ret = new();
        private readonly EntityDataDto<SnippetVersionDto> _rDto = new();
        private readonly IMapper _mapper;
        const string name = "SnippetVersion_";

        public SnippetVersionService(ICacheUtil cacheUtil, SnblogContext coreDbContext, IMapper mapper)
        {
            _service = coreDbContext;
            _mapper = mapper;
            _cache = (CacheUtil)cacheUtil;
        }

        public async Task<List<SnippetVersionDto>> GetAllBySnId(int id, bool cache)
        {
            Common.CacheInfo($"{name}{Common.Condition}{id}{cache}");
            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnippetVersionDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }
            var ent = await _service.SnippetVersions.Where(s => s.SnippetId == id).AsNoTracking().ToListAsync();
            var ret = _mapper.Map<List<SnippetVersionDto>>(ent);
            _cache.SetValue(Common.CacheKey, _rDto.EntityList);
            return ret;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Common.CacheInfo($"{name}{Common.Del}{id}");

            var ret = await _service.SnippetVersions.FindAsync(id);
            if (ret == null) return false;
            _service.SnippetVersions.Remove(ret); //删除单个
            _service.Remove(ret); //直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddAsync(SnippetVersion entity)
        {
            Common.CacheInfo($"{name}{Common.Add}{entity}");
            var num = await GetSumAsync(1, entity.SnippetId, false);
            entity.Count = num += 1;
            entity.TimeCreate = DateTime.Now;
            await _service.SnippetVersions.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(SnippetVersion entity)
        {
            Common.CacheInfo($"{name}{Common.Up}{entity.Id}_{entity}");

            //entity.TimeModified = DateTime.Now; //更新时间
            _service.SnippetVersions.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<int> GetSumAsync(int identity, int snId, bool cache)
        {
            Common.CacheInfo($"{name}{Common.Sum}{identity}{cache}");

            if (cache)
            {
                _ret.EntityCount = _cache.GetValue<int>(Common.CacheKey);
                if (_ret.EntityCount != 0) return _ret.EntityCount;
            }

            if (identity != 1)
            {
                _ret.EntityCount = await _service.SnippetVersions.AsNoTracking().CountAsync();
            }
            else
            {
                _ret.EntityCount = await _service.SnippetVersions.AsNoTracking().CountAsync(c => c.SnippetId == snId);
            }

            _cache.SetValue(Common.CacheKey, _ret.EntityCount);
            return _ret.EntityCount;
        }


        public async Task<bool> UpdatePortionAsync(SnippetVersion entity, string type)
        {
            Common.CacheInfo($"{name}{Common.Paging} {entity.Id}_{type}");
            Snippet result = await _service.Snippets.FindAsync(entity.Id);
            if (result == null) return false;
            switch (type)
            {
                //指定字段进行更新操作
                case "text":
                    //修改属性，被追踪的league状态属性就会变为Modify
                    result.Text = entity.Text;
                    break;
                case "name":
                    result.Name = entity.Name;
                    break;
            }

            //执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnippetVersionDto> GetByIdAsync(int id, bool cache)
        {
            Common.CacheInfo($"{name}{Common.Bid}{id}_{cache}");
            if (cache)
            {
                _rDto.Entity = _cache.GetValue<SnippetVersionDto>(Common.CacheKey);
                if (_rDto.Entity != null) return _rDto.Entity;
            }

            _ret.Entity = await _service.SnippetVersions.AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);
            _rDto.Entity = _mapper.Map<SnippetVersionDto>(_ret.Entity);
            _cache.SetValue(Common.CacheKey, _rDto.Entity);
            return _rDto.Entity;
        }
    }
}