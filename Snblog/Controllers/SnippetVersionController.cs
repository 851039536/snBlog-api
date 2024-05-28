using Snblog.Util.GlobalVar;

namespace Snblog.Controllers;

/// <summary>
/// 代码片段(历史版本)
/// </summary>
[ApiExplorerSettings(GroupName = "V1")] //版本控制
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
    /// <param name="snId"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity,int snId, bool cache = false)
    {
        return Ok(await _service.GetSumAsync(identity,snId,cache));
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
        return Ok(await _service.GetByIdAsync(id, cache));
    }
    #endregion

    #region 条件查询 
    /// <summary>
    /// 根据snippet表的主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    [HttpGet("bysnid")]
    public async Task<IActionResult> GetAllBySnId(int id, bool cache = false)
    {
        return Ok(await _service.GetAllBySnId(id, cache));
    }
    #endregion

    #region 新增
    /// <summary>
    ///  新增(自动累加次数)
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    [Authorize(Roles = Permissionss.Name)]
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
    [Authorize(Roles = Permissionss.Name)]
    [HttpPut("edit")]
    public async Task<IActionResult> UpdateAsync(SnippetVersion entity)
    {
        return Ok(await _service.UpdateAsync(entity));
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    [Authorize(Roles = Permissionss.Name)]
    [HttpDelete("del")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await _service.DeleteAsync(id));
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
        return Ok(await _service.UpdatePortionAsync(entity, type));
    }
    #endregion

}