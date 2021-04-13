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

        /// <summary>
        /// 查询文章(无文章内容 缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 分页查询 (条件)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
      Task< List<SnArticle>> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc);
    }
}
