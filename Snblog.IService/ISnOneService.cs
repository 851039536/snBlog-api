using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnOneService
    {

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnOne>> AsyGetOne();

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 条件查询总数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<int> CountTypeAsync(int type);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SnOne> AsyGetOneId(int id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnOne> GetPagingOne(int pageIndex, int pageSize, out int count, bool isDesc);


        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="pageIndex">每页记录条数</param>
        /// <param name="pageSize">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="name">排序条件</param>
        /// <returns></returns>
        Task<List<SnOne>> GetFyTypeAsync(int type, int pageIndex, int pageSize,string name, bool isDesc);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDetOne(int id);

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnOne> AsyInsOne(SnOne one);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        Task<string> AysUpOne(SnOne one);




    }
}
