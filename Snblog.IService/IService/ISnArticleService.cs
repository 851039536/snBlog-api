using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Enties.Models;

namespace Snblog.IService.IService
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
        Task<List<SnArticle>> GetAllAsync();

        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSumAsync(string type);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SnArticle> AsyGetTestName(int id);

        /// <summary>
        /// ID条件查询
        /// </summary>
        /// <param name="sortId">ID</param>
        /// <returns></returns>
        List<SnArticle> GetTestWhere(int sortId);


        /// <summary>
        /// 按标签分页查询 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        Task< List<SnArticle>> GetPagingWhereAsync(int type, int pageIndex, int pageSize, bool isDesc);


           /// <summary>
        /// 按标签分页查询 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        Task< List<SnArticle>> GetPagingSortWhereAsync(int type, int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="name">排序条件</param>
        /// <returns></returns>
        Task<List<SnArticle>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc);

        /// <summary>
        /// 查询文章(无文章内容 缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc);

        /// <summary>
        /// 按标签id查询
        /// </summary>
        /// <param name="tag">标签id</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        Task<List<SnArticle>> GetTagtextAsync(int tag,bool isDesc);

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int ConutSort(int type);

         /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int ConutLabel(int type);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);



        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnArticle entity);


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnArticle entity);

      /// <summary>
      /// 更新部分列[comment give read]
      /// </summary>
      /// <param name="snArticle"></param>
      /// <param name="type">更新的字段</param>
      /// <returns></returns>
        Task<bool> UpdatePortionAsync(SnArticle snArticle, string type);


        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> CountAsync(bool cache);
    }
}
