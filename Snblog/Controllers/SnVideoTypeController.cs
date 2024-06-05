using Snblog.Jwt;

namespace Snblog.Controllers;

/// <summary>
/// 视频分类
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("videoType")]
public class SnVideoTypeController : BaseController
{
    private readonly ISnVideoTypeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public SnVideoTypeController(ISnVideoTypeService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAll();
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync(int id)
    {
        var data = await _service.GetAllAsync(id);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <returns></returns>
    [HttpGet("CountAsync")]
    public async Task<IActionResult> CountAsync()
    {
        int data = await _service.CountAsync();
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 添加数据 （权限）
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost("AddAsync")]
    [Authorize(Policy = JPermissions.Create)]
    public async Task<IActionResult> AddAsync(SnVideoType entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 删除数据 （权限）
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpDelete("del")]
    [Authorize(Policy = JPermissions.Delete)]
    public async Task<IActionResult> DeleteAsync(SnVideoType entity)
    {
        bool data = await _service.DeleteAsync(entity);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 更新数据 （权限）
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Policy = JPermissions.Edit)]
    public async Task<IActionResult> UpdateAsync(SnVideoType entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
}
