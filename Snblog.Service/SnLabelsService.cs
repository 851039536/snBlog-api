using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnLabelsService : BaseService, ISnLabelsService
    {
        public SnLabelsService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetLabels(int id)
        {
            int da = await Task.Run(() => CreateService<SnLabels>().AsyDelete(id));
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnLabels>> AsyGetLabels()
        {
            var data = CreateService<SnLabels>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<List<SnLabels>> AsyGetLabelsId(int id)
        {
            var data = CreateService<SnLabels>().Where(s => s.LabelId == id);
            return await data.ToListAsync();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnLabels> AsyInsLabels(SnLabels test)
        {
            return await Task.Run(() => CreateService<SnLabels>().AysAdd(test));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<string> AysUpLabels(SnLabels test)
        {
            int da = await CreateService<SnLabels>().AysUpdate(test);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<SnLabels> GetLabels()
        {
            var data = this.CreateService<SnLabels>();
            return data.GetAll().ToList();
        }

        public int GetLabelsCount()
        {
            int data = CreateService<SnLabels>().Count();
            return data;
        }
    }
}
