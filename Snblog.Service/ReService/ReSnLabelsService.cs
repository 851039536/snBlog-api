using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.IService.IReService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service.ReService
{
    public class ReSnLabelsService : BaseService, IReSnLabelsService
    {
        private readonly CacheUtil _cacheUtil;
        private int result_Int;
        private List<SnLabels> result_List = null;
        public ReSnLabelsService(ICacheUtil cacheUtil, IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
            _cacheUtil = (CacheUtil)cacheUtil;
        }

        public async Task<List<SnLabels>> GetAllAsync()
        {
            result_List = _cacheUtil.CacheString("ReGetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnLabels>().GetAllAsync();
                _cacheUtil.CacheString("ReGetAllAsync", result_List);
            }
            return result_List;
        }

        public async Task<SnLabels> GetByIdAsync(int id)
        {
            SnLabels labels = null;
            labels = _cacheUtil.CacheString("ReGetByIdAsync" + id, labels);
            if (labels == null)
            {
                labels = await CreateService<SnLabels>().GetByIdAsync(id);
                _cacheUtil.CacheString("ReGetByIdAsync" + id, labels);
            }
            return labels;
        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheUtil.CacheNumber("ReGetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnLabels>().CountAsync();
                _cacheUtil.CacheNumber("ReGetCountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<List<SnLabels>> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {

            result_List = _cacheUtil.CacheString("ReGetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                var data = await CreateService<SnLabels>().WherepageAsync(s => true, c => c.LabelId, pageIndex, pageSize, isDesc);
                result_List = data.ToList();
                _cacheUtil.CacheString("ReGetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            }
            return result_List;

        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnLabels> AddAsync(SnLabels Entity)
        {
            return await CreateService<SnLabels>().AddAsync(Entity);
        }

         /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<string> UpdateAsync(SnLabels Entity)
        {
            int da = await CreateService<SnLabels>().UpdateAsync(Entity);
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
            int da = await CreateService<SnLabels>().DeleteAsync(id);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }
    }
}
