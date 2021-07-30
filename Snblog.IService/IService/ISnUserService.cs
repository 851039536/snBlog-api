using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnUserService
    {
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnUserDto>> AsyGetUser();

        /// <summary>
        /// 主键id查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<SnUser> AsyGetUserId(int userId);

        /// <summary>
        /// 查询用户总数
        /// </summary>
        /// <returns></returns>
        int GetUserCount();

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
        Task<SnUser> AsyInsUser(SnUser test);

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
