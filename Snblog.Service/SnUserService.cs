using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnUserService : BaseService, ISnUserService
    {
        public SnUserService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public async Task<string> AsyDetUserId(int UserId)
        {
            int da = await Task.Run(() => CreateService<SnUser>().AsyDelete(UserId));
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnUser>> AsyGetUser()
        {
            var data = CreateService<SnUser>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<List<SnUser>> AsyGetUserId(int UserId)
        {
            var data = CreateService<SnUser>().Where(s => s.UserId == UserId);
            return await data.ToListAsync();
        }

        public async Task<SnUser> AsyInsUser(SnUser test)
        {
            return await CreateService<SnUser>().AysAdd(test);
        }

        public async Task<string> AysUpUser(SnUser User)
        {
           int da = await CreateService<SnUser>().AysUpdate(User);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        public List<SnUser> GetPagingUser(int label, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnUser> data;
            data = CreateService<SnUser>().Wherepage(s => s.UserId != null, c => c.UserId, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
        }

        public int GetUserCount()
        {
            int data = CreateService<SnUser>().Count();
            return data;
        }
    }
}
