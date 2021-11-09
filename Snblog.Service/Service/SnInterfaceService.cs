using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
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
        //private List<SnInterfaceDto> result_ListDto = default;
        private readonly IMapper _mapper;

        Tool<SnInterface> data = new Tool<SnInterface>();
        Tool<SnInterfaceDto> datas = new Tool<SnInterfaceDto>();
        public SnInterfaceService(snblogContext service, ICacheUtil cacheutil, IMapper mapper, ILogger<SnInterfaceService> logger = null)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<SnInterfaceDto>> GetTypeAsync(int identity, string users, string type, bool cache)
        {
            datas.resultListDto = _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity)
                {
                    case 0:
                        datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.Type.Name == type && s.User.Name == users).AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.User.Name == users).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.Type.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetTypeAsync_SnInterfaceDto" + identity + users + type + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }

        public async Task<List<SnInterfaceDto>> GetAllAsync(bool cache)
        {
            datas.resultListDto = _cacheutil.CacheString("GetAllAsync_SnInterface", datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.AsNoTracking().ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnInterface", datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }

        public async Task<List<SnInterfaceDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询_SnInterfaceDto" + identity + type + pageIndex + pageSize + isDesc + cache);
            datas.resultListDto = _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(
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
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(
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
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.Type.Name == type)
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
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.Type.Name == type)
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
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.User.Name == type)
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
                                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(w => w.User.Name == type)
                             .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                _cacheutil.CacheString("GetFyAsync_SnArticle" + identity + type + pageIndex + pageSize + isDesc + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }
        private async Task<List<SnInterfaceDto>> GetTypefy(int userId, int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (userId == 00)
            {
                if (isDesc)
                {
                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                }
                else
                {
                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).AsNoTracking().ToListAsync());
                }
            }
            else
            {
                if (isDesc)
                {
                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type && s.UserId == userId).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                }
                else
                {
                    datas.resultListDto = _mapper.Map<List<SnInterfaceDto>>(await _service.SnInterfaces.Where(s => s.TypeId == type && s.UserId == userId).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                }
            }
            return datas.resultListDto;
        }
    }
}
