using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnOneService : BaseService, ISnOneService
    {

        private readonly snblogContext _coreDbContext;//DB
        public SnOneService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext coreDbContext) : base(repositoryFactory, mydbcontext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<List<SnOne>> AsyGetOne()
        {
            var data = CreateService<SnOne>();
            return await data.GetAll().ToListAsync();
        }


        public async Task<SnOne> AsyGetOneId(int id)
        {
            return await CreateService<SnOne>().AysGetById(id);
        }

        public List<SnOne> GetPagingOne(int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnOne> data;
            data = CreateService<SnOne>().Wherepage(s => true, c => c.OneId, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
            //_coreDbContext.SnOne.Where(s => true).OrderByDescending(c => c.OneRead).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetOne(int id)
        {
            int da = await CreateService<SnOne>().AsyDelete(id);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        public async Task<SnOne> AsyInsOne(SnOne one)
        {
            return await CreateService<SnOne>().AysAdd(one);
        }

        public async Task<string> AysUpOne(SnOne one)
        {
            int da = await CreateService<SnOne>().AysUpdate(one);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        public async Task<int> CountAsync()
        {
            return await _coreDbContext.SnOne.CountAsync();
        }

        public async Task<int> CountTypeAsync(int type)
        {
            int count = await _coreDbContext.SnOne.Where(s => s.OneTypeId == type).CountAsync();

            return count;
        }
    }
}
