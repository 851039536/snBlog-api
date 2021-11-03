using Snblog.Enties.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService.IService
{
    public interface ISnOneService
    {

       /// <summary>
       /// 查询所有
       /// </summary>
       /// <param name="cache"></param>
       /// <returns></returns>
         Task<List<SnOne>> GetAllAsync(bool cache);

        /// <summary>
        ///  查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> CountAsync(bool cache);

        /// <summary>
        /// 条件查询总数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> CountTypeAsync(int type, bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnOne> GetByIdAsync(int id,bool cache);


        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetSumAsync(string type, bool cache);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnOne>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc, bool cache);


        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="pageIndex">每页记录条数</param>
        /// <param name="pageSize">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="name">排序条件</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnOne>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc, bool cache);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnOne one);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnOne one);

        /// <summary>
        /// 更新点赞[ give ]
        /// </summary>
        /// <param name="snOne"></param>
        /// <param name="type">更新的字段</param>
        /// <returns></returns>
        Task<bool> UpdatePortionAsync(SnOne snOne, string type);


    }
}
