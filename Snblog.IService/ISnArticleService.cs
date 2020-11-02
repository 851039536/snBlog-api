using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnArticleService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<SnArticle> GetTest();

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnArticle>> GetAllAsync();

        int GetArticleCount();


        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SnArticle> AsyGetTestName(int id);

        /// <summary>
        /// where条件查询
        /// </summary>
        /// <param name="sortId"></param>
        /// <returns></returns>
        List<SnArticle> GetTestWhere(int sortId);


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnArticle> GetPagingWhere(int label, int pageIndex, int pageSize, out int count, bool isDesc);

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int ConutLabel(int type);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDetArticleId(int id);



        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnArticle> AsyInsArticle(SnArticle test);



        Task<string> AysUpArticle(SnArticle test);



        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
    }
}
