namespace Snblog.Service.Service
{
    public class SnNavigationService : ISnNavigationService
    {
        const string NAME = "Navigation_";
        private readonly snblogContext _service; //DB
        private readonly CacheUtil _cache;
        private readonly EntityData<SnNavigation> _ret = new();
        private readonly EntityDataDto<SnNavigationDto> _rDto = new();
        private readonly IMapper _mapper;

        public SnNavigationService(snblogContext service, ICacheUtil cache, IMapper mapper)
        {
            _service = service;
            _cache = (CacheUtil)cache;
            _mapper = mapper;
        }

        public async Task<List<SnNavigationDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,
            string ordering, bool isDesc, bool cache)
        {
            Common.CacheInfo(
                $"{NAME}{Common.Paging}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}");
            if (cache)
            {
                _rDto.EntityList = _cache.GetValue<List<SnNavigationDto>>(Common.CacheKey);
                if (_rDto.EntityList != null) return _rDto.EntityList;
            }

            switch (identity) //查询条件
            {
                case 0:
                    await GetPagingAll(pageIndex, pageSize, ordering, isDesc);
                    break;

                case 1:
                    await GetFyType(type, pageIndex, pageSize, ordering, isDesc);
                    break;

                case 2:
                    await GetFyUser(type, pageIndex, pageSize, ordering, isDesc);
                    break;
            }

            _ret.EntityList = _cache.SetValue(Common.CacheKey, _ret.EntityList);
            return _rDto.EntityList;
        }

        private async Task GetFyUser(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc) //降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        private async Task GetFyType(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc) //降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        private async Task GetPagingAll(int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc) //降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());

                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                }
            }
            else //升序
            {
                switch (ordering) //排序
                {
                    case "id":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }

        /// <summary>
        /// DEL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information("SnNavigation删除数据=>" + id);
            SnNavigation todoItem = await _service.SnNavigations.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            _service.SnNavigations.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnNavigationDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            Log.Information("SnNavigation条件查询=>" + type + identity + cache);
            _rDto.EntityList = _cache.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache,
                _rDto.EntityList, cache);
            if (_rDto.EntityList == null)
            {
                switch (identity)
                {
                    case 1:
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.Type.Title == type).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .Where(w => w.User.Name == type).AsNoTracking().ToListAsync());
                        break;
                }

                _cache.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache, _rDto.EntityList, cache);
            }

            return _rDto.EntityList;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnNavigation entity)
        {
            Log.Information("SnNavigation添加数据=>" + entity);
            entity.TimeCreate = DateTime.Now;
            entity.TimeModified = DateTime.Now;
            await _service.SnNavigations.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            Log.Information("SnNavigation修改数据=>" + entity);
            entity.TimeModified = DateTime.Now; //更新时间
            var res = await _service.SnNavigations.Where(w => w.Id == entity.Id).Select(
                s => new
                {
                    s.TimeCreate,
                }
            ).AsNoTracking().ToListAsync();
            entity.TimeCreate = res[0].TimeCreate; //赋值表示时间不变
            _service.SnNavigations.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {
            Log.Information("SnNavigation查询总数=>" + identity + type + cache);
            _ret.EntityCount = _cache.CacheNumber("GetCountAsync_SnNavigation" + identity + type + cache,
                _ret.EntityCount, cache);
            if (_ret.EntityCount == 0)
            {
                switch (identity)
                {
                    case 0:
                        _ret.EntityCount = await _service.SnNavigations.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        _ret.EntityCount = await _service.SnNavigations.Where(w => w.Type.Title == type).AsNoTracking()
                            .CountAsync();
                        break;
                    case 2:
                        _ret.EntityCount = await _service.SnNavigations.Where(w => w.User.Name == type).AsNoTracking()
                            .CountAsync();
                        break;
                }

                _cache.CacheNumber("GetCountAsync_SnNavigation" + identity + type + cache, _ret.EntityCount, cache);
            }

            return _ret.EntityCount;
        }

        public async Task<List<SnNavigationDto>> GetAllAsync(bool cache)
        {
            Log.Information("SnNavigation查询所有=>" + cache);
            _rDto.EntityList = _cache.CacheString("GetAllAsync_SnNavigation" + cache, _rDto.EntityList, cache);
            if (_rDto.EntityList == null)
            {
                _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.ToListAsync());
                _cache.CacheString("GetAllAsync_SnNavigation" + cache, _rDto.EntityList, cache);
            }

            return _rDto.EntityList;
        }

        public async Task<SnNavigationDto> GetByIdAsync(int id, bool cache)
        {
            Log.Information("SnNavigation主键查询=>" + id + cache);
            _rDto.Entity = _cache.CacheString("GetByIdAsync_SnNavigation" + id + cache, _rDto.Entity, cache);
            if (_rDto.Entity == null)
            {
                _rDto.Entity = _mapper.Map<SnNavigationDto>(await _service.SnNavigations.FindAsync(id));
                _cache.CacheString("GetByIdAsync_SnNavigation" + id + cache, _ret.EntityList, cache);
            }

            return _rDto.Entity;
        }

        public async Task<List<SnNavigationDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {
            Log.Information($"SnNavigationDto模糊查询=>{type}{name}{cache}");

            _rDto.EntityList =
                _cache.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, _rDto.EntityList, cache);
            if (_rDto.EntityList == null)
            {
                switch (identity)
                {
                    case 0:
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .Where(l => l.Title.Contains(name)).OrderByDescending(c => c.Id).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .Where(l => l.Title.Contains(name) && l.Type.Title == type)
                                .OrderByDescending(c => c.Id).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        _rDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                                .Where(l => l.Title.Contains(name) && l.User.Name == type)
                                .OrderByDescending(c => c.Id).Include(z => z.Type
                                ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                }

                _cache.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, _rDto.EntityList, cache);
            }

            return _rDto.EntityList;
        }
    }
}