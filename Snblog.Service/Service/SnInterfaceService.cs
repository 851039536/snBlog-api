using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnInterfaceService : ISnInterfaceService
    {
        private readonly SnblogContext _service;
        private readonly CacheUtil _cacheutil;
        private List<SnInterfaceDto> result_ListDto = default;
        private readonly IMapper _mapper;
        public SnInterfaceService(SnblogContext service, ICacheUtil cacheutil, IMapper mapper)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _mapper = mapper;
        }

        public async Task<List<SnInterfaceDto>> GetTypeAsync(int userId, int type, bool cache)
        {
            result_ListDto = _cacheutil.CacheString("GetTypeAsync_SnInterface" + userId + type, result_ListDto, cache);
            if (result_ListDto == null)
            {
                var result = await _service.SnInterface.Where(s => s.TypeId == type && s.UserId == userId).ToListAsync();
                result_ListDto = _mapper.Map<List<SnInterfaceDto>>(result);
                _cacheutil.CacheString("GetTypeAsync_SnInterface" + userId + type, result_ListDto, cache);
            }
            return result_ListDto;
        }
    }
}
