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
        
    #region 分页查询 
    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int pageIndex = 1,int pageSize = 10,bool isDesc = true,bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex,pageSize,isDesc,cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
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
    /// <param name="type">分类</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [HttpGet("type")]
    public async Task<IActionResult> GetTypeAsync(int type, bool cache)
    {
        var data = await _service.GetTypeAsync(type, cache);
        return ApiResponse(data: data, cache: cache);
    }

    #endregion

    #region 查询总数

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    [HttpGet("sum")]
    public async Task<IActionResult> CountAsync(bool cache)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(data: data, cache: cache);
    }

    #endregion

    #region 添加

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [HttpPost("add")]
    [Authorize(Roles = Permissionss.Name)]
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
    /// <returns></returns>
    [HttpDelete("del")]
    [Authorize(Roles = Permissionss.Name)]
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
    /// <param name="entity">实体</param>
    /// <returns></returns>
    [HttpPut("update")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> UpdateAsync(DiaryType entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion
}