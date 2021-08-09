using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService.IService;
using Snblog.Models;
using System;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    /// 博客设置
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    //  [Authorize]
    public class SnSetBlogController : ControllerBase
    {
        private readonly ISnSetBlogService _service; //IOC依赖注入

        #region 构造函数
        public SnSetBlogController(ISnSetBlogService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion


        #region  按标签分页查询  
        /// <summary>
        ///分页查询 
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetfyAsync")]
        public async Task<IActionResult> GetfyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetfyAsync(type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion
        #region 主键查询 
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        //#region 添加数据
        ///// <summary>
        ///// 添加数据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[Authorize(Roles = Permissions.Name)]
        //[HttpPost("AddAsync")]
        //public async Task<ActionResult<SnArticle>> AddAsync(SnArticle entity)
        //{
        //    return Ok(await _service.AddAsync(entity));
        //}
        //#endregion
        //#region 更新数据
        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[Authorize(Roles = Permissions.Name)]
        //[HttpPut("UpdateAsync")]
        //public async Task<IActionResult> UpdateAsync(SnArticle entity) => Ok(await _service.UpdateAsync(entity));
        //#endregion
        //#region 删除数据
        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Authorize(Roles = Permissions.Name)]
        //[HttpDelete("DeleteAsync")]
        //public async Task<IActionResult> DeleteAsync(int id) => Ok(await _service.DeleteAsync(id));
        //#endregion
        #region 更新部分列[comment give read]
        /// <summary>
        /// 更新部分列
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段 type</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(SnSetBlogDto entity, string type)
        {
            return Ok(await _service.UpdatePortionAsync(entity, type));
        }
        #endregion

    }
}
