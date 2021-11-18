using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;

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
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnArticleDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 标签:2</param>
        /// <param name="type">查询条件</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnArticleDto>> GetContainsAsync(int identity, string type, string name, bool cache);
        /// <summary>
        /// 读取字段长度 GetSumAsync
        /// </summary>
        /// <param name="identity">0:所有: 分类:1 || 标签:2 || 用户:3</param>
        /// <param name="type">1-内容-2:阅读-3:点赞</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">是否开启缓存</param>
        Task<int> GetSumAsync(int identity, int type, string name, bool cache);
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnArticleDto>> GetByIdAsync(int id, bool cache);

        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 标签:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnArticleDto>> GetTypeAsync(int identity, string type, bool cache);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 标签:2 || 用户:3</param>
        /// <param name="type">查询参数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 read:阅读 give:点赞 按id排序]</param>
        /// <returns></returns>
        Task<List<SnArticleDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);


        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加数据
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
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 标签:2 || 用户3  </param>
        /// <param name="type">查询条件 </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(int identity, string type, bool cache);
    }
}
