using Snblog.Util.GlobalVar;

namespace Snblog.Controllers;

/// <summary>
/// 导航表分类
/// </summary>
[Route("navigationType")]
[ApiExplorerSettings(GroupName = "V1")] //版本控制
[ApiController]
public class NavigationTypeController : BaseController
{
    private readonly INavigationTypeService _service; //IOC依赖注入
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public NavigationTypeController(INavigationTypeService service)
    {
        _service = service;
    }

    #region 查询总数
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(bool cache)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 查询所有
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(bool cache = false)
    {
        var data = await _service.GetAllAsync(cache);
        return ApiResponse(data: data);
    }
    #endregion

    #region 主键查询
    /// <summary>
    ///  主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache)
    {
        var data = await _service.GetByIdAsync(id,cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="type">类型[all查所有]</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">是否开启缓存</param>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cache)
    {
        var data = await _service.GetPagingAsync(type,pageIndex,pageSize,isDesc,cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 添加
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    [HttpPost("add")]
    [Authorize(Roles = Permissions.Name)]
    public async Task<IActionResult> AddAsync(NavigationType entity)
    {
        var data = await _service.AddAsync(entity);
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
    [Authorize(Roles = Permissions.Name)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
    #region 更新
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体类</param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Roles = Permissions.Name)]
    public async Task<IActionResult> UpdateAsync(NavigationType entity)
    {
        var data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion


}