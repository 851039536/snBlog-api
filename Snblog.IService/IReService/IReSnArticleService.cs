using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService.IReService
{
    public interface IReSnArticleService
    {
        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// 条件查询总条数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int CountAsync(int type);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnArticle>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <returns></returns>
        Task<SnArticle> GetByIdAsync(int id);

        /// <summary>
        /// 分类查询
        /// </summary>
        /// <param name="id">分类id(label_id)</param>
        /// <returns></returns>
        Task<List<SnArticle>> GetLabelAllAsync(int id);

        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSumAsync(string type);
    }
}
