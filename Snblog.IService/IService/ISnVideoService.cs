using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnVideoService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否缓存</param>
        /// <returns></returns>
        Task<List<SnVideoDto>> GetAllAsync(bool cache);


        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 用户:2</param>
        /// <param name="type">查询条件</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnVideoDto>> GetContainsAsync(int identity, string type, string name, bool cache);

        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2 </param>
        /// <param name="type">查询条件 </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(int identity, string type, bool cache);
        /// <summary>
        /// 按条件查询总数
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> GetTypeCount(int type, bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnVideoDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 用户:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnVideoDto>> GetTypeAsync(int identity, string type, bool cache);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="sortId">ID</param>
        /// <returns></returns>
        Task<List<SnVideo>> GetTypeAllAsync(int type, bool cache);

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        ///  /// <param name="cache">是否开启缓存</param>
        Task<List<SnVideo>> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间  按id排序]</param>
        /// <returns></returns>
        Task<List<SnVideoDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);

        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSumAsync(bool cache);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        Task<bool> AddAsync(SnVideo entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnVideo entity);

    }
}
