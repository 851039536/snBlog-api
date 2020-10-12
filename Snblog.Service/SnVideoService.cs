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
    public class SnVideoService : BaseService, ISnVideoService
    {
        public SnVideoService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public async Task<string> AsyDetVideo(int id)
        {
           int da= await Task.Run(() => CreateService<SnVideo>().AsyDelete(id));
           string data = da == 1 ? "删除成功" : "删除失败";
           return data;
        }

        public async Task<List<SnVideo>> AsyGetTest()
        {
           var data = CreateService<SnVideo>();
            return await data.GetAll().ToListAsync();
        }

        public Task<SnVideo> AsyGetTestName(int type)
        {
          throw new NotImplementedException();
        }

        public async Task<SnVideo> AsyInsVideo(SnVideo test)
        {
            return await  CreateService<SnVideo>().AysAdd(test);
        }

        public async Task<string> AysUpVideo(SnVideo test)
        {
            try
            {
           int da= await CreateService<SnVideo>().AysUpdate(test);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
                 }
            catch (Exception e)
            {
               return "异常:"+e.Message;
            }
        }

        public int ConutLabel(int type)
        {
            throw new NotImplementedException();
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetArticleCount()
        {
            throw new NotImplementedException();
        }

        public List<SnVideo> GetPagingWhere(int pageIndex, int pageSize, out int count, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public List<SnVideo> GetTest()
        {
           var data = CreateService<SnVideo>();
            return data.GetAll().ToList();
        }

        public List<SnVideo> GetTestWhere(int type)
        {
           var data=  CreateService<SnVideo>().Where(s => s.VTypeid == type);
            return  data.ToList();
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
