using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using System;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    /// 代码片段
    /// </summary>
   //[Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("snippet")]
    public class SnippetController : ControllerBase
    {
        private readonly ISnippetService _service; //IOC依赖注入

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">service</param>
        public SnippetController(ISnippetService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region 查询总数
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
        /// <param name="type">条件:vue</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(int identity = 0, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetSumAsync(identity, type, cache));
        }
        #endregion

        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null", string name = "c", bool cache = false)
        {
            if (name == null) return null;
            return Ok(await _service.GetContainsAsync(identity, type, name, cache));
        }
        #endregion

        #region 主键查询 
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region 内容统计
        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户账号:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        [HttpGet("strSum")]
        public async Task<IActionResult> GetStrSumAsync(int identity = 0,  string name = "null", bool cache = false)
        {
            return Ok(await _service.GetStrSumAsync(identity, name, cache));
        }

        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|子标签:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10,  bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetPagingAsync(identity, type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion

        #region 新增
        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Snippet entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateAsync(Snippet entity)
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
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("del")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion

        #region 条件更新
        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="type">更新字段: name | text</param>
        /// <returns>bool</returns>
        [HttpPut("upPortion")]
        public async Task<IActionResult> UpdatePortionAsync(Snippet entity, string type)
        {
            return Ok(await _service.UpdatePortionAsync(entity, type));
        }
        #endregion

    }
}
