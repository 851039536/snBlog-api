using Snblog.IService.IService.Articles;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers.Articles;

/// <summary>
/// 文章标签API
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("articleTag")]
public class ArticleTagController : BaseController
{
    private readonly IArticleTagService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    public ArticleTagController(IArticleTagService service)
    {
        _service = service;
    }
    
    
    #region 查询总数
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否使用缓存。默认为false。</param>
    /// <returns>文章标签的总数。</returns>
    [HttpGet("sum")]
    public async Task<IActionResult> GetSumAsync(bool cache = false)
    {
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 主键查询
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">文章标签的主键ID。</param>
    /// <param name="cache">是否使用缓存。默认为false。</param>
    /// <returns>文章标签的详细信息。</returns>
    [HttpGet("byId")]
    public async Task<IActionResult> GetByIdAsync(int id,bool cache = false)
    {
        var data = await _service.GetByIdAsync(id,cache);
        return ApiResponse(cache: cache,data: data);
    }
    #endregion

    #region 分页查询 
    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex">当前页码，默认为1。</param>
    /// <param name="pageSize">每页显示的记录数，默认为10。</param>
    /// <param name="isDesc">是否按降序排列，默认为true。</param>
    /// <param name="cache">是否使用缓存，默认为false。</param>
    /// <returns>分页后的文章标签列表。</returns>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int pageIndex = 1,int pageSize = 10,bool isDesc = true,bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex,pageSize,isDesc,cache);
        return ApiResponse(cache: cache,data: data);
    }
    /// <summary>
    /// 分页测试，使用分页通用类。
    /// </summary>
    /// <param name="page">当前页码，默认为1。</param>
    /// <param name="pageSize">每页显示的记录数，默认为10。</param>
    /// <returns>分页后的测试数据。</returns>
    [HttpGet("pagingTest")]
    public async Task<IActionResult> TestPaging(int page = 1, int pageSize = 10)  
    {  
        var data = await _service.GetPagingTest(page,pageSize);
        return ApiResponse(data: data);
    }
    #endregion

    #region 添加
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity">要添加的文章标签实体。</param>
    /// <returns>操作是否成功。</returns>
    [HttpPost("add")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> AddAsync(ArticleTag entity)
    {
        bool data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }
    #endregion

    #region 更新
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">包含更新信息的文章标签实体。</param>
    /// <returns>操作是否成功。</returns>
    [HttpPut("update")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> UpdateAsync(ArticleTag entity)
    {
        bool data = await _service.UpdateAsync(entity);
        return ApiResponse(data: data);
    }

    #endregion

    #region 删除 
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">要删除的文章标签的主键ID。</param>
    /// <returns>操作是否成功。</returns>
    [HttpDelete("del")]
    [Authorize(Roles = Permissionss.Name)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        bool data = await _service.DeleteAsync(id);
        return ApiResponse(data: data);
    }
    #endregion
}