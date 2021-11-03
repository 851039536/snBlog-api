using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using System.Threading.Tasks;
using Snblog.IService.IService;
using Blog.Core;
using Snblog.Enties.Models;

namespace Snblog.Controllers
{

    /// <summary>
    /// 图床
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnPictureController : ControllerBase
    {
        private readonly ISnPictureService _service;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SnPictureController(ISnPictureService service)
        {
            _service = service;
        }

        #region  图床查询（缓存）
        /// <summary>
        /// 图床查询（缓存）
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
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }



        #endregion
        #region  分页查询（ 缓存）
        /// <summary>
        /// 分页查询（ 缓存）
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
        #region 条件分页查询（缓存）
        /// <summary>
        /// 条件分页查询（缓存）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        [HttpGet("GetFyTypeAllAsync")]
        public async Task<IActionResult> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetFyTypeAllAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 图床总数（缓存）
        /// <summary>
        /// 图床总数（缓存）
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        #endregion
        #region 条件查询总数（缓存）
        /// <summary>
        /// 条件查询总数（缓存）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public async Task<IActionResult> CountAsync(int type)
        {
            return Ok(await _service.CountAsync(type));
        }
        #endregion
        #region 添加数据 （权限）
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(SnPicture entity)
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
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
        #region 更新数据 （权限）
        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnPicture entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion



    }
}
