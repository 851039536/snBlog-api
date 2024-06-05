using Snblog.IService.IService.Snippets;
using Snblog.Jwt;

namespace Snblog.Controllers.Snippets;

/// <summary>
/// 片段标签API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("snippetTag")]
public class SnippetTagController : BaseController
{
    private readonly ISnippetTagService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public SnippetTagController(ISnippetTagService service)
    {
        _service = service;
    }

    #region 查询总数
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否使用缓存数据。</param>
    /// <returns>返回片段标签的总数。</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(bool cache = false)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 查询所有
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">是否使用缓存数据。</param>
    /// <returns>返回所有片段标签的列表。</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(bool cache = false)
    {
        var data = await _service.GetAllAsync(cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 主键查询
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">要查询的片段标签的主键。</param>
    /// <param name="cache">是否使用缓存数据。</param>
    /// <returns>返回匹配的片段标签实体。</returns>
    [HttpGet("byId")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 按名称查询

    /// <summary>
    /// 按名称查询
    /// </summary>
    /// <param name="name">要查询的片段标签的名称。</param>
    /// <param name="cache">是否使用缓存数据。</param>
    /// <returns>返回匹配的片段标签实体。</returns>
    [HttpGet("byTitle")]
    public async Task<IActionResult> GetByTitle(string name, bool cache = false)
    {
        var data = await _service.GetByTitle(name, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码。</param>
    /// <param name="pageSize">每页显示的记录数。</param>
    /// <param name="isDesc">是否按倒序排列。</param>
    /// <param name="cache">是否使用缓存数据。</param>
    /// <returns>返回分页的片段标签列表。</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetFyAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache);
        return ApiResponse(data: data, cache: cache);
    }
    #endregion

    #region 添加
    /// <summary>
    ///  添加
    /// </summary>
    /// <param name="entity">要添加的片段标签实体。</param>
    /// <returns>操作是否成功。</returns>
    [HttpPost("add")]
    [Authorize(Policy = JPermissions.Create)]
    public async Task<IActionResult> AddAsync(SnippetTag entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 更新数据
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">包含更新信息的片段标签实体。</param>
    /// <returns>操作是否成功。</returns>
    [HttpPut("update")]
    [Authorize(Policy = JPermissions.Edit)]
    public async Task<IActionResult> UpdateAsync(SnippetTag entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">要删除的片段标签的主键。</param>
    /// <returns>操作是否成功。</returns>
    [HttpDelete("del")]
    [Authorize(Policy = JPermissions.Delete)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
}
