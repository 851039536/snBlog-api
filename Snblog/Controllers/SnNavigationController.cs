using System.Threading.Tasks;
using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnNavigationController : ControllerBase
    {
        private readonly ISnNavigationService _service; //IOC依赖注入
        public SnNavigationController(ISnNavigationService service)
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
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id,bool cache)
        {
            return Ok(await _service.GetByIdAsync(id,cache));
        }
        #endregion
        #region 查询总数
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync(bool cache)
        {
            return Ok(await _service.GetCountAsync(cache));
        }
        #endregion
        #region 条件查询总数
        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public async Task<IActionResult> CountTypeAsync(string type,bool cache)
        {
            return Ok(await _service.CountTypeAsync(type,cache));
        }

        #endregion
        #region 去重查询 (缓存)

        /// <summary>
        /// 去重查询 
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        [HttpGet("GetDistinct")]
        public async Task<IActionResult> GetDistinct(string type)
        {
            return Ok(await _service.GetDistinct(type));
        }

        #endregion
        #region 条件查询
        /// <summary>
        /// 条件查询 
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns>List</returns>
        [HttpGet("GetTypeOrderAsync")]
        public async Task<IActionResult> GetTypeOrderAsync(string type, bool order,bool cache)
        {
            return Ok(await _service.GetTypeOrderAsync(type, order,cache));
        }
        #endregion
        #region 分页查询 
        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="type">查询条件:all -表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetFyAllAsync")]
        public async Task<IActionResult> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc,bool cache)
        {
            return Ok(await _service.GetFyAllAsync(type, pageIndex, pageSize, isDesc,cache));
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加数据 
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<SnNavigation>> AddAsync(SnNavigation entity)
        {
            return Ok(await _service.AddAsync(entity));
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
        public async Task<IActionResult> UpdateAsync(SnNavigation entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
        #region 删除数据
         /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
       

    }
}
