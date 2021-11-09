﻿using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
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
         Task<List<SnOneDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 标签:2</param>
        /// <param name="type">分类</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnOneDto>> GetContainsAsync(int identity, string type, string name, bool cache);
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
        /// <param name="type">条件(identity为0则填0) </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(int identity, string type, bool cache);

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
        Task<SnOneDto> GetByIdAsync(int id,bool cache);


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
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 read:阅读 give:点赞 按id排序]</param>
        /// <returns></returns>
        Task<List<SnOneDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);


        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnOne one);

        /// <summary>
        /// 更新
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
