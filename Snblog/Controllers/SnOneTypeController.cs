using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Snblog.IService.IService;
using Snblog.Models;
using Blog.Core;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    /// SnOneTypeController
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnOneTypeController : ControllerBase
    {

        private readonly ISnOneTypeService _service; //IOC依赖注入

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnOneTypeController(ISnOneTypeService service)
        {
            _service = service;
        }

        #region 查询所有
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion
        #region 主键查询
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id,bool cache)
        {
            return Ok(await _service.GetByIdAsync(id,cache));
        }
        #endregion
        #region 类别查询
        /// <summary>
        /// 类别查询
        /// </summary>
        /// <param name="type">分类</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int type,bool cache)
        {
            return Ok(await _service.GetTypeAsync(type,cache));
        }
        #endregion
        #region 查询总数
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("CountAsync")]

        public async Task<IActionResult> CountAsync(bool cache)
        {
            return Ok(await _service.CountAsync(cache));
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加数据 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(SnOneType entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 删除数据
         /// <summary>
        /// 删除数据 
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
         #region 更新数据 
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnOneType entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
    }
}
