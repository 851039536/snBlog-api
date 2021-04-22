using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using System.Threading.Tasks;
using Snblog.Enties.Models;
using Snblog.IService.IService;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnOneTypeController : ControllerBase
    {

        private readonly ISnOneTypeService _service; //IOC依赖注入
        public SnOneTypeController(ISnOneTypeService service)
        {
            _service = service;
        }

        #region 查询所有（缓存）
        /// <summary>
        /// 查询所有（缓存）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        #endregion
        #region 主键查询（缓存）
        /// <summary>
        /// 主键查询（缓存）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        #endregion
        #region 类别查询（缓存）
        /// <summary>
        /// 类别查询（缓存）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int type)
        {
            return Ok(await _service.GetTypeAsync(type));
        }


        #endregion
        #region 查询总数（缓存）
        /// <summary>
        /// 查询总数（缓存）
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAsync")]

        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        #endregion
        #region 添加数据 （权限）
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AddAsync(SnOneType entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 删除数据 （权限）
         /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
         #region 更新数据 （权限）
        /// <summary>
        /// 更新数据（权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
           [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnOneType entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
    }
}
