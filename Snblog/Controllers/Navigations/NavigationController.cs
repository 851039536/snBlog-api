using Snblog.IService.IService.Navigations;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers.Navigations;

/// <summary>
/// 导航API
/// </summary>
[Route("navigation")]
[ApiExplorerSettings(GroupName = "V1")] //版本控制
[ApiController]
public class NavigationController : BaseController
{
    private readonly INavigationService _service;

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public NavigationController(INavigationService service)
    {
        _service = service;
    }

    #endregion

    #region 查询总数

    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="identity">查询类型标识（所有:0, 分类:1, 用户:2）</param>
    /// <param name="type">查询条件</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>返回查询结果及是否使用缓存的状态</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity = 0,string type = "null",bool cache = false)
    {
        int data = await _service.GetSumAsync(identity,type,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>返回查询结果及是否使用缓存的状态</returns>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id,bool cache = false)
    {
        var data = await _service.GetByIdAsync(id,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">查询类型标识</param>
    /// <param name="type">查询条件</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>返回查询结果及是否使用缓存的状态</returns>
    [HttpGet("contains")]
    public async Task<IActionResult> GetContainsAsync(int identity = 0,string type = "null",string name = "c",bool cache = false)
    {
        var data = await _service.GetContainsAsync(identity,type,name,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 条件查询

    /// <summary>
    ///条件查询(可删除，同分页查询)
    /// </summary>
    /// <param name="identity">查询类型标识</param>
    /// <param name="type">查询条件</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>返回查询结果及是否使用缓存的状态</returns>
    [HttpGet("type")]
    public async Task<IActionResult> GetTypeAsync(int identity = 1,string type = "null",bool cache = false)
    {
        var data = await _service.GetTypeAsync(identity,type,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
    /// <param name="type">查询条件</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">是否使用缓存</param>
    /// <param name="ordering">排序条件</param>
    /// <returns>返回查询结果及是否使用缓存的状态</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetFyAsync(int identity = 0,string type = "null",int pageIndex = 1,int pageSize = 10,string ordering = "id",
                                                bool isDesc = true,bool cache = false)
    {
        var data = await _service.GetPagingAsync(identity,type,pageIndex,pageSize,ordering,isDesc,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 生成随机图片导航

    /// <summary>
    /// 生成随机图片导航
    /// </summary>
    /// <param name="minValue">随机数最小值</param>
    /// <param name="maxValue">随机数最大值</param>
    /// <returns>返回操作结果</returns>
    [HttpPost("randomImg")]
    public async Task<IActionResult> RandomImg(int minValue = 1,int maxValue = 11)
    {
        bool ret = await _service.RandomImg(minValue,maxValue);
        return ApiResponse(data: ret);
    }

    #endregion

    #region 添加

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity">导航数据实体</param>
    /// <returns>返回操作结果</returns>
    [HttpPost("add")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> AddAsync(Navigation entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 更新

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">导航数据实体</param>
    /// <returns>返回操作结果</returns>
    [HttpPut("update")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> UpdateAsync(Navigation entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 删除

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>返回操作结果</returns>
    [HttpDelete("del")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }

    #endregion
}