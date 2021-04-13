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
        public SnUserService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public async Task<string> AsyDetUserId(int userId)
        {
            int da = await  CreateService<SnUser>().DeleteAsync(userId);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnUser>> AsyGetUser()
        {
            var data = CreateService<SnUser>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<List<SnUser>> AsyGetUserId(int userId)
        {
            var data = CreateService<SnUser>().Where(s => s.UserId == userId);
            return await data.ToListAsync();
        }

        public async Task<SnUser> AsyInsUser(SnUser test)
        {
            return await CreateService<SnUser>().AddAsync(test);
        }

        public async Task<string> AysUpUser(SnUser user)
        {
           int da = await CreateService<SnUser>().UpdateAsync(user);
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
