using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService.IReService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.ReService
{
    public class ReSnArticleService : BaseService, IReSnArticleService
    {
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnArticle> result_List = null;

        public ReSnArticleService(ICacheUtil cacheUtil, IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
            _cacheutil = (CacheUtil)cacheUtil;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("CountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnArticle>().CountAsync();
                _cacheutil.CacheNumber1("CountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<int> CountAsync(int type)
        {
            //读取缓存值
            result_Int = _cacheutil.CacheNumber1("CountAsync" + type, result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnArticle>().CountAsync(c => c.LabelId == type);//获取数据值
                _cacheutil.CacheNumber1("CountAsync" + type, result_Int);//设置缓存值
            }
            return result_Int;
        }

        public async Task<List<SnArticle>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("GetAllSnArticleAsync", result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnArticle>().GetAllAsync();
                _cacheutil.CacheString1("GetAllSnArticleAsync", result_List);
            }
            return result_List;
        }

        public async Task<SnArticle> GetByIdAsync(int id)
        {
            SnArticle result = null;
            result = _cacheutil.CacheString1("GetByIdAsync" + id, result);
            if (result == null)
            {
                result = await CreateService<SnArticle>().GetByIdAsync(id);
                _cacheutil.CacheString1("GetByIdAsync" + id, result);
            }
            return result;
        }

        public async Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("ReGetFyTitleAsync" + pageIndex + pageSize + isDesc, result_List); //设置缓存
            if (result_List == null)
            {
                result_List = await GetFyTitle(pageIndex, pageSize, isDesc); //读取数据
                _cacheutil.CacheString1("ReGetFyTitleAsync" + pageIndex + pageSize + isDesc, result_List); //设置缓存
            }
            return result_List;
        }

        /// <summary>
        /// 读取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        private async Task<List<SnArticle>> GetFyTitle(int pageIndex, int pageSize, bool isDesc)
        {
            var data = await Task.Run(() => CreateService<SnArticle>().Wherepage(s => true, c => c.ArticleId, pageIndex, pageSize, out _, isDesc).Select(s => new
            {
                s.ArticleId,
                s.Title,
                s.Comment,
                s.Give,
                s.Read,
                s.Time,
                s.TitleText,
                s.UserId
            }).ToList());
            //解决方案二：foreach遍历
            var list = new List<SnArticle>();
            foreach (var t in data)
            {
                var s = new SnArticle();
                s.ArticleId = t.ArticleId;
                s.Title = t.Title;
                s.Comment = t.Comment;
                s.Give = t.Give;
                s.Read = t.Read;
                s.Time = t.Time;
                s.TitleText = t.TitleText;
                s.UserId = t.UserId;
                list.Add(s);
            }
            return list;
        }

        public async Task<List<SnArticle>> GetLabelAllAsync(int id)
        {
            result_List = _cacheutil.CacheString1("GetLabelAllAsync" + id, result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnArticle>().Where(s => s.LabelId == id).ToListAsync();
                _cacheutil.CacheString1("GetLabelAllAsync" + id, result_List);
            }
            return result_List;
        }

        public async Task<int> GetSumAsync(string type)
        {
            result_Int = _cacheutil.CacheNumber1("ReGetSumAsync" + type, result_Int);
            if (result_Int == 0)
            {
                result_Int = await GetSum(type);
                _cacheutil.CacheNumber1("ReGetSumAsync" + type, result_Int);
            }
            return result_Int;
        }

        /// <summary>
        /// 读取总字数
        /// </summary>
        /// <param name="type">阅读/点赞/评论</param>
        /// <returns></returns>
        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":

                    var read = await CreateService<SnArticle>().Where(s => true).Select(c => c.Read).ToListAsync();
                    foreach (var i in read)
                    {
                            var item = i;
                            num += item;
                    }
                    break;
                case "text":
                    var text = await CreateService<SnArticle>().Where(s => true).Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await CreateService<SnArticle>().Where(s => true).Select(c => c.Give).ToListAsync();
                    foreach (var i in give)
                    {
                            var item = i;
                            num += item;
                    }
                    break;
            }

            return num;
        }

        public async Task<List<SnArticle>> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("ReGetTypeFyTextAsync" + type + pageIndex + isDesc, result_List);
            if (result_List == null)
            {
                result_List = await GetTypeFy(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString1("ReGetTypeFyTextAsync" + type + pageIndex + isDesc, result_List);
            }
            return result_List;

        }

        private async Task<List<SnArticle>> GetTypeFy(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == 00)
            {
                var data = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.ArticleId, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
            else
            {
                var data = await CreateService<SnArticle>().WherepageAsync(s => s.LabelId == type, c => c.ArticleId, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
        }

        public async Task<List<SnArticle>> GetFyTypeorderAsync(int type, int pageIndex, int pageSize, string order, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("ReGetFyTypeorderAsync" + type + pageIndex + pageSize + order + isDesc, result_List);
            if (result_List == null)
            {
                result_List = await GetFyTypeorder(type, pageIndex, pageSize, order, isDesc);
                _cacheutil.CacheString1("ReGetFyTypeorderAsync" + type + pageIndex + pageSize + order + isDesc, result_List);
            }
            return result_List;

        }

        private async Task<List<SnArticle>> GetFyTypeorder(int type, int pageIndex, int pageSize, string order, bool isDesc)
        {
            if (type == 00)//表示查所有
            {
                switch (order)
                {
                    case "read":
                        var data = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.Read, pageIndex, pageSize, isDesc);
                        return data.ToList();
                    case "data":
                        var data1 = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.Time, pageIndex, pageSize, isDesc);
                        return data1.ToList();
                    case "give":
                        var data2 = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.Give, pageIndex, pageSize, isDesc);
                        return data2.ToList();
                    case "comment":
                        var data4 = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.Comment, pageIndex, pageSize, isDesc);
                        return data4.ToList();
                    default:
                        var data5 = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.ArticleId, pageIndex, pageSize, isDesc);
                        return data5.ToList();
                }
            }
            else
            {
                var data = await CreateService<SnArticle>().WherepageAsync(s => s.SortId == type, c => c.ArticleId, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
        }

        public async Task<List<SnArticle>> GetTagtextAsync(int tag, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("ReGetTagtextAsync" + tag + isDesc, result_List); //设置缓存
            if (result_List == null)
            {
                result_List = await GetTagtext(tag, isDesc); //读取数据
                _cacheutil.CacheString1("ReGetTagtextAsync" + tag + isDesc, result_List); //设置缓存
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetTagtext(int tag, bool isDesc)
        {
            var data = await CreateService<SnArticle>().Where(s => s.LabelId == tag, c => c.ArticleId, isDesc).Select(s => new
            {
                s.ArticleId,
                s.Title,
                s.TitleText,
                s.Time,
                s.Give,
                s.Read
            }).ToListAsync();
            var list = new List<SnArticle>();
            foreach (var t in data)
            {
                var s = new SnArticle();
                s.ArticleId = t.ArticleId;
                s.Title = t.Title;
                s.TitleText = t.TitleText;
                s.Time = t.Time;
                s.Give = t.Give;
                s.Read = t.Read;
                list.Add(s);
            }
            return list;

        }

        public async Task<SnArticle> AddAsync(SnArticle entity)
        {
            return await CreateService<SnArticle>().AddAsync(entity);
        }

        public async Task<string> UpdateAsync(SnArticle entity)
        {
            int result = await CreateService<SnArticle>().UpdateAsync(entity);
            string Func(int data) => data == 1 ? "更新成功" : "更新失败";
            return Func(result);
        }

        public async Task<string> DeleteAsync(int id)
        {
            int resultId = await Task.Run(() => CreateService<SnArticle>().DeleteAsync(id));
            string result = resultId == 1 ? "删除成功" : "删除失败";
            return result;
        }

        public async Task<bool> UpdatePortionAsync(SnArticle snArticle, string name)
        {
            var date = await CreateService<SnArticle>().UpdateAsync1(snArticle, true, name);

            return date;
        }
    }
}
