﻿using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnOneService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<SnOne> GetOne();
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnOne>> AsyGetOne();
        int OneCount();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SnOne> AsyGetOneId(int id);
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
        List<SnOne> GetPagingOne(int pageIndex, int pageSize, out int count, bool isDesc);

        /// <summary>
        ///  条件查询一文总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int OneCountType(string type);

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
