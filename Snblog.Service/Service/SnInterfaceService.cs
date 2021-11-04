using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
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
        private List<SnInterfaceDto> result_ListDto = default;
        private readonly IMapper _mapper;
        public SnInterfaceService(snblogContext service, ICacheUtil cacheutil, IMapper mapper, ILogger<SnInterfaceService> logger = null)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<SnInterfaceDto>> GetTypeAsync(int identity, int users, int type, bool cache)
        {
            result_ListDto = _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                switch (identity)
                {
                    case 0:
                        result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type && s.UserId == users).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.UserId == users).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type).AsNoTracking().ToListAsync());
                        break;
                    default:
                        break;
                }
                _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, result_ListDto, cache);
            }
            return result_ListDto;
        }

        public async Task<List<SnInterfaceDto>> GetAllAsync(bool cache)
        {
            result_ListDto = _cacheutil.CacheString("GetAllAsync_SnInterface", result_ListDto, cache);
            if (result_ListDto == null)
            {
                result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnInterface", result_ListDto, cache);
            }
            return result_ListDto;
        }

        public async Task<List<SnInterfaceDto>> GetFyAsync(int identity, int type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询_SnInterfaceDto" + identity + type + pageIndex + pageSize + isDesc + cache);
            result_ListDto = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(
                            await _service.SnInterfaces.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(
                            await _service.SnInterfaces.Where(s => true)
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
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.TypeId == type)
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
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.TypeId == type)
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
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.UserId == type)
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
                                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.UserId == type)
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, result_ListDto, cache);
            }
            return result_ListDto;
        }
        private async Task<List<SnInterfaceDto>> GetTypefy(int userId, int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (userId == 00)
            {
                if (isDesc)
                {
                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                }
                else
                {
                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                }
            }
            else
            {
                if (isDesc)
                {
                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type && s.UserId == userId).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                }
                else
                {
                    result_ListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type && s.UserId == userId).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                }
            }
            return result_ListDto;
        }
    }
}
