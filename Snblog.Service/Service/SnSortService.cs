using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnSortService : BaseService, ISnSortService
    {
        public SnSortService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetSort(int id)
        {
            int da = await Task.Run(() => CreateService<SnSort>().AsyDelete(id));
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnSort>> AsyGetSort()
        {
             var data = CreateService<SnSort>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<List<SnSort>> AsyGetSortId(int id)
        {
            var data = CreateService<SnSort>().Where(s => s.SortId == id);
            return await data.ToListAsync();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnSort> AsyInsSort(SnSort test)
        {
            return await CreateService<SnSort>().AysAdd(test);
        }

        public async Task<string> AysUpSort(SnSort test)
        {
            int da = await CreateService<SnSort>().AysUpdate(test);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        public List<SnSort> GetPagingWhere( int pageIndex, int pageSize, out int count, bool isDesc)
        {
           var data = CreateService<SnSort>().Wherepage(s => true, c => c.SortId, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<SnSort> GetSort()
        {
            var data = this.CreateService<SnSort>();
            return data.GetAll().ToList();
        }

        public int GetSortCount()
        {
            int data = CreateService<SnSort>().Count();
            return data;
        }
    }
}
