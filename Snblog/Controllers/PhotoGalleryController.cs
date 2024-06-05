using Microsoft.AspNetCore.RateLimiting;
using Snblog.Jwt;

namespace Snblog.Controllers;

/// <summary>
/// 图库
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("photoGallery")]
public class PhotoGalleryController : BaseController
{
    private readonly IPhotoGalleryService _service;
    private readonly IValidator<PhotoGallery> _validator;

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public PhotoGalleryController(IServiceProvider service)
    {
        _service = service.GetRequiredService<IPhotoGalleryService>();
        _validator = service.GetRequiredService<IValidator<PhotoGallery>>();
    }

    #endregion

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
    /// <param name="type">条件</param>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    [EnableRateLimiting("fixed")]
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity = 0, string type = null, bool cache = false)
    {
        int data = await _service.GetSumAsync(identity, type, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("contains")]
    public async Task<IActionResult> GetContainsAsync(
        int identity = 0,
        string type = "null",
        string name = "",
        bool cache = false
    )
    {
        var data = await _service.GetContainsAsync(identity, type, name, cache);
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
    [HttpGet("bid")]
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
    /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <param name="ordering">排序规则 data:时间|give:点赞|id:主键</param>
    /// <returns>list-entity</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(
        int identity = 0,
        string type = "null",
        int pageIndex = 1,
        int pageSize = 10,
        string ordering = "id",
        bool isDesc = true,
        bool cache = false
    )
    {
        var data = await _service.GetPagingAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 新增

    /// <summary>
    ///  新增
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Policy = JPermissions.Create)]
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(PhotoGallery entity)
    {
        var result = await _validator.ValidateAsync(entity);
        if (!result.IsValid)
        {
            return ApiResponse(statusCode: 404, message: result.Errors[0].ErrorMessage, data: entity);
        }

        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 更新

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Policy = JPermissions.Edit)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(PhotoGallery entity)
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
    [Authorize(Policy = JPermissions.Delete)]
    [HttpDelete("del")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DelAsync(id);
        return ApiResponse(data: data);
    }

    #endregion

    #region 条件更新

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="type">更新字段:  Give </param>
    /// <returns>bool</returns>
    [HttpPut("upPortion")]
    public async Task<IActionResult> UpdatePortionAsync(PhotoGallery entity, string type)
    {
        bool data = await _service.UpdatePortionAsync(entity, type);
        return ApiResponse(data: data);
    }

    #endregion
}
