using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnUserTalkService
    {

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        int GetTalkCount();

        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int UserTalkTypeConut(int UserId);
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnUserTalk>> AsyGetUserTalk();

        /// <summary>
        /// 查询当前用户的说说
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        string GetUserTalkFirst(int UserId, bool isdesc);



        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnUserTalk> GetPagingUserTalk(int label, int pageIndex, int pageSize, out int count, bool isDesc);


        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDetUserTalk(int id);
      


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnUserTalk> AsyInsUserTalk(SnUserTalk talk);


     
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        Task<string> AysUpUserTalk(SnUserTalk talk);

    }
}
