using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;

namespace Snblog.IService.IService
    {
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface IArticleService
    {

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        Task<List<ArticleDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<ArticleDto>> GetContainsAsync(int identity, string type, string name, bool cache);
        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<int> GetStrSumAsync(int identity, int type, string name, bool cache);
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<ArticleDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        ///类别查询
        /// </summary>
        /// <param name="identity">分类:1|标签:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">缓存</param>
        Task<List<ArticleDto>> GetTypeAsync(int identity, string type, bool cache);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|read:阅读|give:点赞|id:主键</param>
        Task<List<ArticleDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> AddAsync(Article entity);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(Article entity);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="type">更新字段: Read | Give | Comment</param>
        /// <returns>bool</returns>
        Task<bool> UpdatePortionAsync(Article entity, string type);


        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
        /// <param name="type">条件</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        Task<int> GetSumAsync(int identity, string type, bool cache); 
    }
}
