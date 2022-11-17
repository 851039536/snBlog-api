using System.Threading.Tasks;
using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IService;
//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    /// <summary>
    /// 标签
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")]
    [ApiController]
    public class SnLabelsController : ControllerBase
    {
        private readonly ISnLabelsService _service; //IOC依赖注入

        #region 构造函数 SnLabelsController
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnLabelsController(ISnLabelsService service)
        {
            _service = service;
        }
        #endregion

        #region 查询所有 GetAllAsync
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache =false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion

        #region 主键查询 GetByIdAsync
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region 查询总数 GetCountAsync
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetSunAsync")]
        public async Task<IActionResult> GetCountAsync(bool cache = false)
        {
            return Ok(await _service.GetCountAsync(cache));
        }
        #endregion

        #region 分页查询 GetFyAsync
        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetFyAsync(pageIndex, pageSize, isDesc, cache));
        }
        #endregion

        #region 添加 AddAsync
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<SnLabel>> AddAsync(SnLabel entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion

        #region 更新 UpdateAsync
        /// <summary>
        /// 更新标签 
        /// </summary>
        /// <param name="entity">标签id</param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnLabel entity)
        {
            var data = await _service.UpdateAsync(entity);
            return Ok(data);
        }
        #endregion

        #region 删除数据 DeleteAsync
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">标签id</param>
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("DelAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
}
