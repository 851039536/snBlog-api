﻿using Snblog.IService.IService.Snippets;
using Snblog.Jwt;

namespace Snblog.Controllers.Snippets;

/// <summary>
/// 片段分类的子类API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("snippetTypeSub")]
public class SnippetTypeSubController : BaseController
{
    private readonly ISnippetTypeSubService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public SnippetTypeSubController(ISnippetTypeSubService service)
    {
        _service = service;
    }

    #region 查询总数
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(bool cache = false)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region 查询所有
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(bool cache = false)
    {
        var data = await _service.GetAllAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 根据主表类别id查询
    /// <summary>
    /// 根据主表类别id查询
    /// </summary>
    /// <param name="snippetTypeId">主表类别id</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("condition")]
    public async Task<IActionResult> GetCondition(int snippetTypeId, bool cache = false)
    {
        var data = await _service.GetCondition(snippetTypeId, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region 主键查询
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    [HttpGet("byId")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetFyAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region 添加
    /// <summary>
    ///  添加
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [HttpPost("add")]
    [Authorize(Policy = JPermissions.Create)]
    public async Task<IActionResult> AddAsync(SnippetTypeSub entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 更新数据
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [HttpPut("update")]
    [Authorize(Policy = JPermissions.Edit)]
    public async Task<IActionResult> UpdateAsync(SnippetTypeSub entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    [HttpDelete("del")]
    [Authorize(Policy = JPermissions.Delete)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
}
