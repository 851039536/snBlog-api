using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnInterfaceService : ISnInterfaceService
    {
        private readonly ILogger<SnInterfaceService> _logger;
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly IMapper _mapper;
        private readonly ResDto<SnInterfaceDto> resDto = new();
        public SnInterfaceService(snblogContext service, ICacheUtil cacheutil, IMapper mapper, ILogger<SnInterfaceService> logger = null)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<SnInterfaceDto>> GetTypeAsync(int identity, string users, string type, bool cache)
        {
            resDto.entityList = _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity)
                {
                    case 0:
                        resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.Type.Name == type && s.User.Name == users).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.User.Name == users).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnInterfaceDto>> GetAllAsync(bool cache)
        {
            resDto.entityList = _cacheutil.CacheString("GetAllAsync_SnInterface", resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Include(i => i.Type).Include(i => i.User).AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnInterface", resDto.entityList, cache);
            }
            return resDto.entityList;
        }

        public async Task<List<SnInterfaceDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询_SnInterfaceDto" + identity + type + pageIndex + pageSize + isDesc + cache);
            resDto.entityList = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, resDto.entityList, cache);
            if (resDto.entityList == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(
                            await _service.SnInterfaces.Where(s => true).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(
                            await _service.SnInterfaces.Where(s => true).Include(i => i.Type).Include(i => i.User)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 1:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.Type.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.Type.Name == type).Include(i => i.Type).Include(i => i.User)
                        .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    resDto.entityList = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.User.Name == type).Include(i => i.Type).Include(i => i.User)
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, resDto.entityList, cache);
            }
            return resDto.entityList;
        }
    }
}
