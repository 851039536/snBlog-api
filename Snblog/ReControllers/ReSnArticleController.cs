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

        #region 构造函数
        public ReSnArticleController(IReSnArticleService service)
        {
            _service = service;
        }
        #endregion
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
        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 读取[字段/阅读/点赞]总数量-缓存
        /// </summary>
        /// <param name="type">text-内容-read:阅读-give:点赞</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type)
        {
            return Ok(await _service.GetSumAsync(type));
        }

        #endregion
        #region 查询文章(无文章内容 缓存)
        /// <summary>
        /// 查询文章(无文章内容 缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        [HttpGet("GetFyTitleAsync")]
        public async Task<IActionResult> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetFyTitleAsync(pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 分页查询 (条件 缓存)
        /// <summary>
        /// 分页查询 (条件 缓存)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetTypeFyTextAsync")]
        public async Task<IActionResult> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetTypeFyTextAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
    }
}
