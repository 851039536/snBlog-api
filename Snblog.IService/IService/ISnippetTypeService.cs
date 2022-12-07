﻿using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
    {
    public interface ISnippetTypeService
        {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<SnippetTypeDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnippetTypeDto>> AsyGetSort();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<SnippetTypeDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<SnippetTypeDto>> GetPagingAsync(int pageInde, int pageSize, bool isDesc, bool cache);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        Task<int> GetSumAsync(bool cache);
        /// <summary>
        ///  添加 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        Task<bool> AddAsync(SnippetType entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(SnippetType test);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);
    }
}
