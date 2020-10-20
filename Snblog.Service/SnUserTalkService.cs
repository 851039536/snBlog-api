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
    public class SnUserTalkService : BaseService, ISnUserTalkService
    {
        public SnUserTalkService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetUserTalk(int id)
        {
            int da = await  CreateService<SnUserTalk>().AsyDelete(id);
            string data = da == 1 ? "删除成功" : "删除失败";
            return data; ;
        }

        public async Task<List<SnUserTalk>> AsyGetUserTalk()
        {
            var data = CreateService<SnUserTalk>();
            return await data.GetAll().ToListAsync();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        public async Task<SnUserTalk> AsyInsUserTalk(SnUserTalk talk)
        {
             return await CreateService<SnUserTalk>().AysAdd(talk);
        }


        public async Task<string> AysUpUserTalk(SnUserTalk talk)
        {
            int da = await CreateService<SnUserTalk>().AysUpdate(talk);
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        public int UserTalkTypeConut(int UserId)
        {
            return CreateService<SnUserTalk>().Count(c => c.UserId == UserId);
        }


        public List<SnUserTalk> GetPagingUserTalk(int label, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnUserTalk> data;
            data = CreateService<SnUserTalk>().Wherepage(s => s.Id != null, c => c.TalkTime, pageIndex, pageSize, out count, isDesc);
            return data.ToList();
        }

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        public int GetTalkCount()
        {
            int data = CreateService<SnUserTalk>().Count();
            return data;
        }




        public string GetUserTalkFirst(int UserId, bool isdesc)
        {
            var data = CreateService<SnUserTalk>().FirstOrDefault(u => u.UserId == UserId, c => c.TalkTime, isdesc);
            return data.TalkText;

        }

        public async Task<List<SnUserTalk>> AsyGetTalk(int TalkId)
        {
           var data = CreateService<SnUserTalk>().Where(s => s.Id == TalkId);
            return await data.ToListAsync();
        }
    }
}
