using Microsoft.Extensions.Logging;

namespace Snblog.Service.Service
{
    public class SnNavigationService : ISnNavigationService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private readonly EntityData<SnNavigation> res = new();
        private readonly EntityDataDto<SnNavigationDto> resDto = new();
        private readonly ILogger<SnNavigationService> _logger;
        private readonly IMapper _mapper;
        public SnNavigationService(snblogContext service, ICacheUtil cacheutil, ILogger<SnNavigationService> logger, IMapper mapper)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<SnNavigationDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            Log.Information("SnNavigation分页查询=>" + identity + pageIndex + pageSize + isDesc + type);
            resDto.EntityList = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + identity, resDto.EntityList, cache);
            if (resDto.EntityList == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        await GetFyAll(pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 1:
                        await GetFyType(type, pageIndex, pageSize, ordering, isDesc);
                        break;

                    case 2:
                        await GetFyUser(type, pageIndex, pageSize, ordering, isDesc);
                        break;
                }
                res.EntityList = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + identity, res.EntityList, cache);
            }
            return resDto.EntityList;
        }

        private async Task GetFyUser(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
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
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
               .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
               .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        private async Task GetFyType(string type, int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type)
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
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
              .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
              .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type).Include(i => i.Type).Include(i => i.User)
            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).AsNoTracking().ToListAsync());
                        break;
                }
            }
        }
        private async Task GetFyAll(int pageIndex, int pageSize, string ordering, bool isDesc)
        {
            if (isDesc)//降序
            {
                switch (ordering) //排序
                {
                    case "id":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                await _service.SnNavigations
                .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Include(z => z.Type
                    ).Include(i => i.User).AsNoTracking().ToListAsync());

                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
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
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                     await _service.SnNavigations
                     .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                     .Take(pageSize).Include(z => z.Type
                         ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case "data":
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
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
            resDto.EntityList = _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache, resDto.EntityList, cache);
            if (resDto.EntityList == null)
            {
                switch (identity)
                {
                    case 1:
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache, resDto.EntityList, cache);
            }
            return resDto.EntityList;
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
            entity.TimeCreate = res[0].TimeCreate;  //赋值表示时间不变
            _service.SnNavigations.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<int> GetCountAsync(int identity, string type, bool cache)
        {

            Log.Information("SnNavigation查询总数=>" + identity + type + cache);
            res.EntityCount = _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + identity + type + cache, res.EntityCount, cache);
            if (res.EntityCount == 0)
            {
                switch (identity)
                {
                    case 0:
                        res.EntityCount = await _service.SnNavigations.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        res.EntityCount = await _service.SnNavigations.Where(w => w.Type.Title == type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        res.EntityCount = await _service.SnNavigations.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
                        break;
                }
                _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + identity + type + cache, res.EntityCount, cache);
            }
            return res.EntityCount;
        }
        public async Task<List<SnNavigationDto>> GetAllAsync(bool cache)
        {
            Log.Information("SnNavigation查询所有=>" + cache);
            resDto.EntityList = _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, resDto.EntityList, cache);
            if (resDto.EntityList == null)
            {
                resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, resDto.EntityList, cache);
            }
            return resDto.EntityList;
        }
        public async Task<SnNavigationDto> GetByIdAsync(int id, bool cache)
        {
            Log.Information("SnNavigation主键查询=>" + id + cache);
            resDto.Entity = _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, resDto.Entity, cache);
            if (resDto.Entity == null)
            {
                resDto.Entity = _mapper.Map<SnNavigationDto>(await _service.SnNavigations.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, res.EntityList, cache);
            }
            return resDto.Entity;
        }
        public async Task<List<SnNavigationDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {

            Log.Information( $"SnNavigationDto模糊查询=>{type}{name}{cache}");

            resDto.EntityList = _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, resDto.EntityList, cache);
            if (resDto.EntityList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                  await _service.SnNavigations
                 .Where(l => l.Title.Contains(name)).OrderByDescending(c => c.Id).Include(z => z.Type
                    ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                   await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.Type.Title == type)
                 .OrderByDescending(c => c.Id).Include(z => z.Type
                    ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.EntityList = _mapper.Map<List<SnNavigationDto>>(
                  await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.User.Name == type)
                 .OrderByDescending(c => c.Id).Include(z => z.Type
                    ).Include(i => i.User).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, resDto.EntityList, cache);
            }
            return resDto.EntityList;
        }

    }
}
