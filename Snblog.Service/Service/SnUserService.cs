using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnUserService : BaseService, ISnUserService
    {
        private readonly snblogContext _service;
        // 创建一个字段来存储mapper对象
        private readonly IMapper _mapper;
        public SnUserService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext service, IMapper mapper) : base(repositoryFactory, mydbcontext)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<string> AsyDetUserId(int userId)
        {
            int da = await  CreateService<SnUser>().DeleteAsync(userId);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnUserDto>> AsyGetUser()
        {
            return _mapper.Map<List<SnUserDto>>(await _service.SnUser.ToListAsync()); 
        }

        public async Task<SnUser> AsyGetUserId(int userId)
        {
            return await _service.SnUser.FindAsync(userId);
        }

        public async Task<SnUser> AsyInsUser(SnUser test)
        {
            return await CreateService<SnUser>().AddAsync(test);
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
            var data = CreateService<SnUser>().Wherepage(s => true, c => c.UserId, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
        }

        public int GetUserCount()
        {
            int data = CreateService<SnUser>().Count();
            return data;
        }
    }
}
