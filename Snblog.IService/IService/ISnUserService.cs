using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnUserService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        Task<List<SnUserDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        Task<SnUserDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
       Task<int> GetCountAsync(bool cache);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnUser> GetPagingUser(int label, int pageIndex, int pageSize, out int count, bool isDesc);


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AsyInsUser(SnUser test);

        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDetUserId(int userId);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> AysUpUser(SnUserDto user);
    }
}
