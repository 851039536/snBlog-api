using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    /// 文章标签
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")] 
    [ApiController]
    [Route("articleTag")]
    public class ArticleTagController : ControllerBase
    {
        private readonly IArticleTagService _service; //IOC依赖注入
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
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(bool cache = false)
        {
            return Ok(await _service.GetSumAsync(cache));
        }
        #endregion
        #region 查询所有
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync(bool cache = false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }

        #endregion
        #region 主键查询
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        [HttpGet("byId")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion
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
        public async Task<IActionResult> GetPagingAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache));
        }
        #endregion
        #region 添加
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [HttpPost("add")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<ArticleTag>> AddAsync(ArticleTag entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [HttpPut("update")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(ArticleTag entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
        #region 删除数据 
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("del")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
}
