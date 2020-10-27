using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnVideoService : BaseService, ISnVideoService
    {
        public SnVideoService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public async Task<string> AsyDetVideo(int id)
        {
            int da = await Task.Run(() => CreateService<SnVideo>().AsyDelete(id));
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<List<SnVideo>> AsyGetTest()
        {
            var data = CreateService<SnVideo>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<List<SnVideo>> AsyGetTestId(int id)
        {
            var data = CreateService<SnVideo>().Where(s => s.VId == id);
            return await data.ToListAsync();
        }

        public async Task<SnVideo> AsyInsVideo(SnVideo test)
        {
            return await CreateService<SnVideo>().AysAdd(test);
        }

        public async Task<string> AysUpVideo(SnVideo test)
        {
            try
            {
                int da = await CreateService<SnVideo>().AysUpdate(test);
                string data = da == 1 ? "更新成功" : "更新失败";
                return data;
            }
            catch (Exception e)
            {
                return "异常:" + e.Message;
            }
        }

        public int ConutType(int type)
        {
            var data = CreateService<SnVideo>().Where(s => s.VTypeid == type);
            return data.Count();
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetVideoCount()
        {
            int data = CreateService<SnVideo>().Count();
            return data;
        }
        /// <summary>
        /// 查询视频总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetVideoCount(int type)
        {
            return CreateService<SnVideo>().Count(c => c.VTypeid == type);
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
        public List<SnVideo> GetPagingWhere(int type, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnVideo> data;
            if (type == 99999)
            {
                data = CreateService<SnVideo>().Wherepage(s => s.VTypeid != null, c => c.VId, pageIndex, pageSize, out count, isDesc);
            }
            else
            {
                data = CreateService<SnVideo>().Wherepage(s => s.VTypeid == type, c => c.VId, pageIndex, pageSize, out count, isDesc);
            }

            return data.ToList();
        }

        public List<SnVideo> GetTest()
        {
            var data = CreateService<SnVideo>();
            return data.GetAll().ToList();
        }

        public List<SnVideo> GetTestWhere(int type)
        {
            var data = CreateService<SnVideo>().Where(s => s.VTypeid == type);
            return data.ToList();
        }

        public SnArticle IntTest(SnVideo test)
        {
            throw new NotImplementedException();
        }

        public string UpTest(SnVideo test)
        {
            throw new NotImplementedException();
        }
    }
}
