﻿using Snblog.Enties.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnInterfaceService
    {
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类和用户:0 || 用户:1 || 分类:2</param>
        /// <param name="users">条件:用户</param>
        /// <param name="type">条件:类别</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnInterfaceDto>> GetTypeAsync(int identity, int users, int type, bool cache);

        Task<List<SnInterfaceDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[按id排序]</param>
        /// <returns></returns>
        public Task<List<SnInterfaceDto>> GetFyAsync(int identity, int type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);
    }
}