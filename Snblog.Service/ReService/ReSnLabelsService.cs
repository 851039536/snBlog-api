using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IRepository;
using Snblog.IService.IReService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.ReService
{
    public class ReSnLabelsService : BaseService, IReSnLabelsService
    {
        private readonly CacheUtil _cacheUtil;
        private int result_Int;
        private List<SnLabel> result_List = null;
        public ReSnLabelsService(ICacheUtil cacheUtil, IRepositoryFactory repositoryFactory, IConcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
            _cacheUtil = (CacheUtil)cacheUtil;
        }

        public async Task<List<SnLabel>> GetAllAsync()
        {
            result_List = _cacheUtil.CacheString1("ReGetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnLabel>().GetAllAsync();
                _cacheUtil.CacheString1("ReGetAllAsync", result_List);
            }
            return result_List;
        }

        public async Task<SnLabel> GetByIdAsync(int id)
        {
            SnLabel labels = null;
            labels = _cacheUtil.CacheString1("ReGetByIdAsync" + id, labels);
            if (labels == null)
            {
                labels = await CreateService<SnLabel>().GetByIdAsync(id);
                _cacheUtil.CacheString1("ReGetByIdAsync" + id, labels);
            }
            return labels;
        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheUtil.CacheNumber1("ReGetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnLabel>().CountAsync();
                _cacheUtil.CacheNumber1("ReGetCountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<List<SnLabel>> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {

            result_List = _cacheUtil.CacheString1("ReGetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                var data = await CreateService<SnLabel>().WherepageAsync(s => true, c => c.Id, pageIndex, pageSize, isDesc);
                result_List = data.ToList();
                _cacheUtil.CacheString1("ReGetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            }
            return result_List;

        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<SnLabel> AddAsync(SnLabel entity)
        {
            return await CreateService<SnLabel>().AddAsync(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> UpdateAsync(SnLabel entity)
        {
            int da = await CreateService<SnLabel>().UpdateAsync(entity);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(int id)
        {
            int da = await CreateService<SnLabel>().DeleteAsync(id);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }
    }
}
