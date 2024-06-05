using Snblog.Util.GlobalVar;

namespace Snblog.Controllers;

/// <summary>
/// 路由导航
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("Interface")]
public class InterfaceController : BaseController
{
    private readonly IInterfaceService _service;

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public InterfaceController(IInterfaceService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    #endregion

    #region 主键查询
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">启缓存</param>
    /// <returns></returns>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region  条件查询
    /// <summary>
    ///条件查询
    /// </summary>
    /// <param name="identity">用户-分类: 0 | 用户: 1 | 分类: 2</param>
    /// <param name="userName">用户名称</param>
    /// <param name="type">类别</param>
    /// <param name="cache">缓存</param>
    [HttpGet("condition")]
    public async Task<IActionResult> GetConditionAsync(
        int identity = 0,
        string userName = "null",
        string type = "null",
        bool cache = false
    )
    {
        var data = await _service.GetConditionAsync(identity, userName, type, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有: 0 | 分类: 1 | 用户名: 2 |  用户-分类: 3</param>
    /// <param name="type">类别参数, identity为0时可为空(null) 多条件以','分割</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(
        int identity = 0,
        string type = "null",
        int pageIndex = 1,
        int pageSize = 10,
        bool isDesc = true,
        bool cache = false
    )
    {
        var data = await _service.GetPagingAsync(identity, type, pageIndex, pageSize, isDesc, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Policy = Permissions.Create)]
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(Interface entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    #region 更新
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Edit)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Interface entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Delete)]
    [HttpDelete("del")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
}
