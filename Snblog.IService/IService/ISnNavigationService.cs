using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;

namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnNavigationService
    {

        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2  </param>
        /// <param name="type">条件(identity为0则填0) </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(int identity, int type, bool cache);


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnNavigationDto>> GetAllAsync(bool cache);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns>List</returns>
        Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order, bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnNavigationDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        ///  <param name="cache">是否开启缓存</param>
        Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc,bool cache);


        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 用户:2</param>
        /// <param name="type">查询条件</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnNavigationDto>> GetContainsAsync(int identity,int type , string name , bool cache );


        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnNavigation entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnNavigation entity);


    }
}
