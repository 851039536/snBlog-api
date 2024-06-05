using Snblog.IService.Videos;
using Snblog.Jwt;

namespace Snblog.Controllers.Videos;

/// <summary>
/// 视频
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("video")]
public class VideoController : BaseController
{
    private readonly IVideoService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public VideoController(IVideoService service)
    {
        _service = service;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户:2 </param>
    /// <param name="type">查询条件</param>
    /// <param name="cache">缓存</param>
    /// <returns></returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity = 0, string type = "null", bool cache = false)
    {
        int data = await _service.GetSumAsync(identity, type, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 查询所有

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(bool cache = false)
    {
        var data = await _service.GetAllAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">无条件:0 || 分类:1 || 用户:2</param>
    /// <param name="type">查询条件</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">是否开启缓存</param>
    [HttpGet("contains")]
    public async Task<IActionResult> GetContainsAsync(
        int identity = 0,
        string type = "null",
        string name = "c",
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
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 条件查询

    /// <summary>
    ///条件查询
    /// </summary>
    /// <param name="identity">分类:1 || 用户:2</param>
    /// <param name="type">类别</param>
    /// <param name="cache">是否开启缓存</param>
    [HttpGet("type")]
    public async Task<IActionResult> GetTypeAsync(int identity = 1, string type = "null", bool cache = false)
    {
        var data = await _service.GetTypeAsync(identity, type, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
    /// <param name="type">类别参数, identity 0 可不填</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序[true/false]</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
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

    #region 读取[字段/阅读/点赞]总数量

    /// <summary>
    /// 统计标题字数
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    [HttpGet("strSum")]
    public async Task<IActionResult> GetSumAsync(bool cache)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    /// <summary>
    /// 添加
    /// </summary>
    [HttpPost("add")]
    [Authorize(Policy = JPermissions.Create)]
    public async Task<IActionResult> AddAsync(Video entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    [HttpDelete("del")]
    [Authorize(Policy = JPermissions.Delete)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    [HttpPut("update")]
    [Authorize(Policy = JPermissions.Edit)]
    public async Task<IActionResult> UpdateAsync(Video entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
}
