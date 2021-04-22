using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService.IReService;
using Snblog.Models;

namespace Snblog.ControllersRepository
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V2")] //版本控制
    [ApiController]
    public class ReSnLabelsController : ControllerBase
    {
        private readonly IReSnLabelsService _service; //IOC依赖注入

        public ReSnLabelsController(IReSnLabelsService service)
        {
            _service = service;
        }
        # region 查询所有 (缓存)
        /// <summary>
        /// 查询所有 (缓存)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        #endregion
        #region 主键查询 (缓存)
        /// <summary>
        /// 主键查询 (缓存)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        # endregion
        # region 分页查询 (缓存)
        /// <summary>
        /// 分页查询 (缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyAllAsync")]
        public async Task<IActionResult> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetfyAllAsync(pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 查询总条数 (缓存)
        /// <summary>
        /// 查询总条数 (缓存)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _service.GetCountAsync());
        }
        #endregion
        # region  添加数据 (权限)
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnLabels>> AddAsync(SnLabels entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 更新数据 (权限)
        /// <summary>
        /// 更新数据 (权限)
        /// </summary>
        /// <param name="entity">标签id</param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnLabels entity)
        {
            var data = await _service.UpdateAsync(entity);
            return Ok(data);
        }
        # endregion
        # region 删除数据 (权限)
        /// <summary>
        /// 删除数据 (权限)
        /// </summary>
        /// <param name="id">标签id</param>
        [Authorize(Roles = "kai")] //角色授权
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
}
