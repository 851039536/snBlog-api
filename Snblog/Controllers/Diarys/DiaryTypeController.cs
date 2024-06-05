using Snblog.IService.IService.Diarys;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers.Diarys;

/// <summary>
/// 日记分类API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("diaryType")]
public class DiaryTypeController : BaseController
{
    private readonly IDiaryTypeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public DiaryTypeController(IDiaryTypeService service)
    {
        _service = service;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否使用缓存查询结果</param>
    /// <returns>日记分类的总数</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> CountAsync(bool cache)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(data: data, cache: cache);
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">日记分类的主键ID</param>
    /// <param name="cache">是否使用缓存查询结果</param>
    /// <returns>日记分类信息</returns>
    [HttpGet("byId")]
    public async Task<IActionResult> GetByIdAsync(int id, bool cache)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(data: data, cache: cache);
    }

    #endregion

    #region 类别查询

    /// <summary>
    /// 类别查询
    /// </summary>
    /// <param name="type">日记分类的类型</param>
    /// <param name="cache">是否使用缓存查询结果</param>
    /// <returns>日记分类信息</returns>
    /// <returns></returns>
    [HttpGet("type")]
    public async Task<IActionResult> GetTypeAsync(int type, bool cache)
    {
        var data = await _service.GetTypeAsync(type, cache);
        return ApiResponse(data: data, cache: cache);
    }

    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码，默认为1</param>
    /// <param name="pageSize">每页显示的记录数，默认为10</param>
    /// <param name="isDesc">是否按降序排列，默认为true</param>
    /// <param name="cache">是否使用缓存查询结果</param>
    /// <returns>分页的日记分类信息列表</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache);
        return ApiResponse(cache: cache, data: data);
    }
    #endregion

    #region 添加

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity">要添加的日记分类实体</param>
    /// <returns>操作是否成功</returns>
    [HttpPost("add")]
    [Authorize(Policy = Permissions.Create)]
    public async Task<IActionResult> AddAsync(DiaryType entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">要删除的日记分类的主键ID</param>
    /// <returns>操作是否成功</returns>
    [HttpDelete("del")]
    [Authorize(Policy = Permissions.Delete)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }

    #endregion

    #region 更新

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">包含更新信息的日记分类实体</param>
    /// <returns>操作是否成功</returns>
    [HttpPut("update")]
    [Authorize(Policy = Permissions.Edit)]
    public async Task<IActionResult> UpdateAsync(DiaryType entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion
}
