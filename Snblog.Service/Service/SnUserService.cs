using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnUserService : BaseService, ISnUserService
    {
        private readonly CacheUtil _cacheutil;
        private readonly snblogContext _service;
        private readonly ILogger<SnUserService> _logger;
        private int result_Int;
        private SnUserDto resultDto = default;
        private List<SnUserDto> result_ListDto = default;
        // 创建一个字段来存储mapper对象
        private readonly IMapper _mapper;
        public SnUserService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext service, IMapper mapper, ILogger<SnUserService> logger, ICacheUtil cacheutil) : base(repositoryFactory, mydbcontext)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<string> AsyDetUserId(int userId)
        {
            int da = await CreateService<SnUser>().DeleteAsync(userId);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnUserDto>> GetAllAsync(bool cache)
        {

            _logger.LogInformation("查询所有_SnUserDto" + cache);
            result_ListDto = _cacheutil.CacheString("GetAllAsync_SnUserDto" + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                result_ListDto = _mapper.Map<List<SnUserDto>>(await _service.SnUsers.ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnUserDto" + cache, result_ListDto, cache);
            }
            return result_ListDto;


        }

        public async Task<SnUserDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnUserDto" + id + cache);
            resultDto = _cacheutil.CacheString("GetByIdAsync_SnUserDto" + id + cache, resultDto, cache);
            if (resultDto == null)
            {
                resultDto = _mapper.Map<SnUserDto>(await _service.SnUsers.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnUserDto" + id + cache, resultDto, cache);
            }
            return resultDto;
        }

        public async Task<bool> AsyInsUser(SnUser test)
        {
            await _service.SnUsers.AddAsync(test);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<string> AysUpUser(SnUserDto user)
        {
            //转换成SnUser
            var model = _mapper.Map<SnUser>(user);
            int da = await CreateService<SnUser>().UpdateAsync(model);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        public List<SnUser> GetPagingUser(int label, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            var data = CreateService<SnUser>().Wherepage(s => true, c => c.Id, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
        }

        public async Task<int> GetCountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnUserDto" + cache);
            result_Int = _cacheutil.CacheString("GetCountAsync_SnUserDto" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnUsers.CountAsync();
                _cacheutil.CacheString("GetCountAsync_SnUserDto" + cache, result_Int, cache);
            }
            return result_Int;
        }
    }
}
