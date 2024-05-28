using Snblog.IService.IService.Diarys;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers.Diarys;

/// <summary>
///日记API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("diary")]
public class DiaryController : BaseController
{
    private readonly IDiaryService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public DiaryController(IDiaryService service)
    {
        _service = service;
    }

    #region 查询总数

    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="identity">查询条件标识（0:所有, 1:分类, 2:用户）</param>
    /// <param name="type">查询类型（当identity为0时，type为null）</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>日记总数</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(int identity = 0,string type = "null",bool cache = false)
    {
        int data = await _service.GetSumAsync(identity,type,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="identity">查询条件标识（0:无条件, 1:分类, 2:标签）</param>
    /// <param name="type">分类或标签</param>
    /// <param name="name">查询字段</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>查询结果</returns>
    [HttpGet("contains")]
    public async Task<IActionResult> GetContainsAsync(int identity = 0,string type = "null",string name = "c",bool cache = false)
    {
        var data = await _service.GetContainsAsync(identity,type,name,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 主键查询

    /// <summary>
    /// 主键查询 
    /// </summary>
    /// <param name="id">日记主键</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>查询结果</returns>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id,bool cache = false)
    {
        var data = await _service.GetByIdAsync(id,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 统计[字段/阅读/点赞]总数量

    /// <summary>
    /// 统计[字段/阅读/点赞]总数量
    /// </summary>
    /// <param name="type">统计类型（text:内容字段数, read:阅读数量, give:点赞数量）</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>统计结果</returns>
    [HttpGet("strSum")]
    public async Task<IActionResult> GetSumAsync(string type,bool cache)
    {
        int data = await _service.GetSumAsync(type,cache);
        return ApiResponse(data: data,cache: cache);
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">查询条件标识（0:所有, 1:分类, 2:用户）</param>
    /// <param name="type">分类或用户</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">是否使用缓存</param>
    /// <param name="ordering">排序条件（data:时间, read:阅读, give:点赞, id:按id排序）</param>
    /// <returns>分页查询结果</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int identity = 0,string type = "null",int pageIndex = 1,int pageSize = 10,string ordering = "id",
                                                    bool isDesc = true,bool cache = false)
    {
        var data = await _service.GetPagingAsync(identity,type,pageIndex,pageSize,ordering,isDesc,cache);
        return ApiResponse(cache: cache,data: data);
    }

    #endregion

    #region 添加数据

    /// <summary>
    /// 添加数据 
    /// </summary>
    /// <param name="entity">日记实体</param>
    /// <returns>操作结果</returns>
    [HttpPost("add")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> AddAsync(Diary entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 删除数据

    /// <summary>
    /// 删除数据 
    /// </summary>
    /// <param name="id">日记主键</param>
    /// <returns>操作结果</returns>
    [HttpDelete("del")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> DelAsync(int id)
    {
        bool data = await _service.DelAsync(id);
        return ApiResponse(data: data);
    }

    #endregion

    #region 更新数据

    /// <summary>
    /// 更新数据 
    /// </summary>
    /// <param name="entity">日记实体</param>
    /// <returns>操作结果</returns>
    [HttpPut("update")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> UpdateAsync(Diary entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 更新部分列

    /// <summary>
    /// 更新部分列[give read]
    /// </summary>
    /// <param name="entity">日记实体</param>
    /// <param name="type">要更新的字段类型</param>
    /// <returns>操作结果</returns>
    [HttpPut("upPortion")]
    public async Task<IActionResult> UpdatePortionAsync(Diary entity,string type)
    {
        bool data = await _service.UpdatePortionAsync(entity,type);
        return ApiResponse(data: data);
    }

    #endregion
}