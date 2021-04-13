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
            return await CreateService<SnOne>().GetByIdAsync(id);
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
            int da = await CreateService<SnOne>().DeleteAsync(id);
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
            return await CreateService<SnOne>().AddAsync(one);
        }

        public async Task<string> AysUpOne(SnOne one)
        {
            int da = await CreateService<SnOne>().UpdateAsync(one);
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

        public async Task<List<SnOne>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            if (isDesc) //降序
            {
                if (type.Equals(999))//表示查所有
                {

                    switch (name)
                    {
                        case "read":
                            return await _coreDbContext.SnOne.Where(s => true)
                            .OrderByDescending(c => c.OneRead).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnOne.Where(s => true)
                           .OrderByDescending(c => c.OneData).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnOne.Where(s => true)
                           .OrderByDescending(c => c.OneGive).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnOne.Where(s => true)
                           .OrderByDescending(c => c.OneComment).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnOne.Where(s => true)
                            .OrderByDescending(c => c.OneId).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnOne.Where(s => s.OneTypeId == type)
                  .OrderByDescending(c => c.OneId).Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize).ToListAsync();
                }

            }
            else   //升序
            {
                if (type.Equals(999))//表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _coreDbContext.SnOne.Where(s => true)
                          .OrderBy(c => c.OneRead).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnOne.Where(s => true)
                          .OrderBy(c => c.OneData).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnOne.Where(s => true)
                          .OrderBy(c => c.OneGive).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnOne.Where(s => true)
                          .OrderBy(c => c.OneComment).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnOne.Where(s => true)
                            .OrderBy(c => c.OneId).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnOne.Where(s => s.OneTypeId == type)
                     .OrderBy(c => c.OneId).Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize).ToListAsync();
                }

            }
        }

        public async Task<int> GetSumAsync(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _coreDbContext.SnOne.Select(c => c.OneRead).ToListAsync();
                    foreach (int item in read)
                    {
                        num += item;
                    }
                    break;
                case "text":
                    var text = await _coreDbContext.SnOne.Select(c => c.OneText).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _coreDbContext.SnOne.Select(c => c.OneGive).ToListAsync();
                    foreach (int item in give)
                    {
                        num += item;
                    }
                    break;
                default:
                    break;
            }
            return num;
        }
    }
}
