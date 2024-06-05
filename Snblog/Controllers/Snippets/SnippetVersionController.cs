using Snblog.IService.IService.Snippets;
using Snblog.Jwt;

namespace Snblog.Controllers.Snippets;

/// <summary>
/// 代码片段(历史版本)API,存储历史版本数据
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("snippetVersion")]
public class SnippetVersionController : BaseController
{
    private readonly ISnippetVersionService _service;

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">service</param>
    public SnippetVersionController(ISnippetVersionService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    #endregion

    #region 查询总数

    /// <summary>
    ///  查询总数
    /// </summary>
    /// <param name="identity">1:根据snId查询 0:默认</param>
    /// <param name="snippetId"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity, int snippetId, bool cache = false)
    {
        int ret = await _service.GetSumAsync(identity, snippetId, cache);
        return ApiResponse(data: ret, cache: cache);
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
        var ret = await _service.GetByIdAsync(id, cache);
        return ApiResponse(data: ret, cache: cache);
    }

    #endregion

    #region 根据snippet表的主键查询

    /// <summary>
    /// 根据snippet表的主键查询
    /// </summary>
    /// <param name="snippetId">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    [HttpGet("snippetId")]
    public async Task<IActionResult> GetAllBySnId(int snippetId, bool cache = false)
    {
        var ret = await _service.GetAllBySnId(snippetId, cache);
        return ApiResponse(data: ret, cache: cache);
    }

    #endregion

    #region 新增

    /// <summary>
    ///  新增(自动累加次数)
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Policy = JPermissions.Create)]
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync(SnippetVersion entity)
    {
        return ApiResponse(data: await _service.AddAsync(entity));
    }

    #endregion

    #region 更新数据

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Policy = JPermissions.Edit)]
    [HttpPut("edit")]
    public async Task<IActionResult> UpdateAsync(SnippetVersion entity)
    {
        bool ret = await _service.UpdateAsync(entity);
        return ApiResponse(data: ret);
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
        bool ret = await _service.DeleteAsync(id);
        return ApiResponse(data: ret);
    }

    #endregion

    #region 条件更新

    /// <summary>
    /// 条件更新(错误的)
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="type">更新字段: name | text</param>
    /// <returns>bool</returns>
    [HttpPut("upPortion")]
    public async Task<IActionResult> UpdatePortionAsync(SnippetVersion entity, string type)
    {
        bool ret = await _service.UpdatePortionAsync(entity, type);
        return ApiResponse(data: ret);
    }

    #endregion
}
