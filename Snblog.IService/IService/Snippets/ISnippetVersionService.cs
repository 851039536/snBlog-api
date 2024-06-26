﻿namespace Snblog.IService.IService.Snippets;

/// <summary>
/// 版本片段接口
/// </summary>
public interface ISnippetVersionService
{
    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="snippetId"></param>
    /// <param name="cache">缓存</param>
    /// <param name="identity"></param>
    /// <returns>int</returns>
    Task<int> GetSumAsync(int identity,int snippetId,bool cache);

    /// <summary>
    /// 主键查询 
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    Task<SnippetVersionDto> GetByIdAsync(int id,bool cache);

    /// <summary>
    /// 根据snippet表的主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    Task<List<SnippetVersionDto>> GetAllBySnId(int id,bool cache);


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns>bool</returns>
    Task<bool> AddAsync(SnippetVersion entity);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>bool</returns>
    Task<bool> UpdateAsync(SnippetVersion entity);

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="type">更新字段: Read | Give | Comment</param>
    /// <returns>bool</returns>
    Task<bool> UpdatePortionAsync(SnippetVersion entity,string type);
}