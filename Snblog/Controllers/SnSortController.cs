using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnSortController : ControllerBase
    {
        private readonly ISnSortService _service; //IOC依赖注入

        public SnSortController(ISnSortService service)
        {
            _service = service;
        }
        #region 查询分类（缓存）
        /// <summary>
        /// 查询分类（缓存）
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
        /// <param name="sortId">主键</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int sortId)
        {
            return Ok(await _service.GetByIdAsync(sortId));
        }
        #endregion
        #region 查询总数（缓存）
        /// <summary>
        /// 查询总数（缓存）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _service.GetCountAsync());
        }
        #endregion
        #region 添加数据 （权限）
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnSort>> AddAsync(SnSort test)
        {
            return Ok(await _service.AddAsync(test));
        }
        #endregion
        #region 分页查询 （缓存）
        /// <summary>
        /// 分页查询 （缓存）
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetFyAllAsync")]
        public async Task<IActionResult> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetFyAllAsync(pageIndex, pageSize, isDesc));
        }
        

        #endregion
        #region 更新数据 （权限）
         /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnSort entity)
        {
            return Ok(  await _service.UpdateAsync(entity));
        }
        #endregion
        #region 删除数据 （权限）
        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
}
