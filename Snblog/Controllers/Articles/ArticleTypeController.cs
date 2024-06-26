﻿using Snblog.IService.IService.Articles;
using Snblog.Jwt;

namespace Snblog.Controllers.Articles;

/// <summary>
/// 文章分类API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("articleType")]
public class ArticleTypeController : BaseController
{
    private readonly IArticleTypeService _service;

    /// <summary>
    /// 初始化实例
    /// </summary>
    /// <param name="service">接口实例</param>
    public ArticleTypeController(IArticleTypeService service)
    {
        _service = service;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>文章分类的总数</returns>
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
    /// <param name="cache">是否使用缓存</param>
    /// <returns>文章分类列表</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(bool cache = false)
    {
        var data = await _service.GetAllAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>文章分类实体</returns>
    [HttpGet("byId")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>分页后的文章分类列表</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 添加
    /// <summary>
    ///  添加
    /// </summary>
    /// <param name="entity">文章分类实体</param>
    /// <returns>操作是否成功</returns>
    [HttpPost("add")]
    [Authorize(Policy = JPermissions.Create)]
    public async Task<IActionResult> AddAsync(ArticleType entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 更新
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">文章分类实体</param>
    /// <returns>操作是否成功</returns>
    [HttpPut("update")]
    //[Authorize(Roles = Permissions.Name)]
    public async Task<IActionResult> UpdateAsync(ArticleType entity)
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
    /// <returns></returns>
    [HttpDelete("del")]
    [Authorize(Policy = JPermissions.Delete)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
}
