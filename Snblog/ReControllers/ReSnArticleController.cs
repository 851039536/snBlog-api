using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.IService.IReService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V2")] //版本控制
    [ApiController]
    public class ReSnArticleController : ControllerBase
    {
        private readonly IReSnArticleService _service; //IOC依赖注入

        public ReSnArticleController(IReSnArticleService service)
        {
            _service = service;
        }

        #region 查询总数 (缓存)
        /// <summary>
        /// 查询总数 (缓存)
        /// </summary>
        [HttpGet("AsyGestTest")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        #endregion
        #region 条件查询总数 (缓存)
        /// <summary>
        /// 条件查询总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public IActionResult CountTypeAsync(int type)
        {
            return Ok(_service.CountAsync(type));
        }
        #endregion
        #region 查询所有 (缓存)
        /// <summary>
        /// 查询所有 (缓存)
        /// </summary>
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
        #endregion
        #region  分类查询 (缓存)
       /// <summary>
        /// 分类查询 (缓存)
        /// </summary>
        /// <param name="id">分类id(label_id)</param>
        /// <returns></returns>
        [HttpGet("GetLabelAllAsync")]
        public async Task<IActionResult> GetLabelAllAsync(int id)
        {
            return Ok(await _service.GetLabelAllAsync(id));
        }
        #endregion
    }
}
