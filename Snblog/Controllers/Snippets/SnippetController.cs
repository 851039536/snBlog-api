using Microsoft.AspNetCore.Http;
using Snblog.IService.IService.Snippets;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers.Snippets;

/// <summary>
/// 代码片段API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")] //版本控制
[ApiController]
[Route("snippet")]
public class SnippetController : BaseController
{
    private readonly ISnippetService _service;
    private readonly IValidator<Snippet> _validator;

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">service</param>
    /// <param name="validator">validator</param>
    public SnippetController(ISnippetService service, IValidator<Snippet> validator)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _validator = validator;
    }
    #endregion

    #region 查询总数
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="identity">查询类型 (所有0,分类1,标签2,用户3)</param>
    /// <param name="type">查询条件。</param>
    /// <param name="cache">是否使用缓存。</param>
    /// <returns>代码片段总数。</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity = 0, string type = "null", bool cache = false)
    {
        int ret = await _service.GetSumAsync(identity, type, cache);
        return ApiResponse(cache: cache, data: ret);
    }
    #endregion

    #region 主键查询
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">代码片段的主键。</param>
    /// <param name="cache">是否使用缓存。</param>
    /// <returns>查询到的代码片段。</returns>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var ret = await _service.GetByIdAsync(id, cache);
        return ApiResponse(cache: cache, data: ret);
    }
    #endregion

    #region 模糊查询
    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4|标题:5</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">缓存</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <returns>匹配的代码片段列表。</returns>
    [HttpGet("contains")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetContainsAsync(
        int identity = 0,
        string type = "null",
        string name = "c",
        bool cache = false,
        int pageIndex = 1,
        int pageSize = 10
    )
    {
        if (name == null)
            return NotFound();
        return ApiResponse(cache: cache, data: await _service.GetContainsAsync(identity, type, name, cache, pageIndex, pageSize));
    }
    #endregion

    #region 内容统计
    /// <summary>
    /// 内容统计
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户账号:3</param>
    /// <param name="name">查询参数。</param>
    /// <param name="cache">是否使用缓存。</param>
    /// <returns>统计结果。</returns>
    [HttpGet("strSum")]
    public async Task<IActionResult> GetStrSumAsync(int identity = 0, string name = "null", bool cache = false)
    {
        int ret = await _service.GetStrSumAsync(identity, name, cache);
        return ApiResponse(data: ret, cache: cache);
    }

    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0|分类:1|用户名:3|子标签:4</param>
    /// <param name="type">查询参数(多条件以','分割)</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <returns>分页的代码片段列表。</returns>
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
        var ret = await _service.GetPagingAsync(identity, type, pageIndex, pageSize, isDesc, cache);
        return ApiResponse(cache: cache, data: ret);
    }

    #endregion

    #region 新增
    /// <summary>
    ///  新增
    /// </summary>
    /// <param name="entity">要添加的代码片段实体。</param>
    /// <returns>操作结果。</returns>
    [Authorize(Policy = Permissions.Create)]
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(Snippet entity)
    {
        var ret = await _validator.ValidateAsync(entity);
        if (!ret.IsValid)
        {
            return ApiResponse(statusCode: 404, message: ret.Errors[0].ErrorMessage, data: entity);
        }
        return ApiResponse(data: await _service.AddAsync(entity));
    }
    #endregion

    #region 更新数据
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">包含更新信息的代码片段实体。</param>
    /// <returns>操作结果。</returns>
    [Authorize(Policy = Permissions.Edit)]
    [HttpPut("edit")]
    public async Task<IActionResult> UpdateAsync(Snippet entity)
    {
        var va = await _validator.ValidateAsync(entity);
        if (!va.IsValid)
        {
            return ApiResponse(statusCode: 404, message: va.Errors[0].ErrorMessage, data: entity);
        }
        var ent = _service.UpdateAsync(entity);
        return ApiResponse(data: ent);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">要删除的代码片段的主键。</param>
    /// <returns>操作结果。</returns>
    [Authorize(Policy = Permissions.Delete)]
    [HttpDelete("del")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool ret = await _service.DeleteAsync(id);
        return ApiResponse(data: ret);
    }
    #endregion

    #region 条件更新
    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="entity">包含更新信息的代码片段实体。</param>
    /// <param name="type">更新字段: name | text</param>
    /// <returns>操作结果。</returns>
    [HttpPut("upPortion")]
    public async Task<IActionResult> UpdatePortionAsync(Snippet entity, string type)
    {
        bool ret = await _service.UpdatePortionAsync(entity, type);
        return ApiResponse(data: ret);
    }
    #endregion
}
