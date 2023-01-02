using System.Threading.Tasks;
using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService;
using Snblog.Repository.Repository;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    /// <summary>
    /// 视频
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")] 
    [ApiController]
    [Route("video")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _service; //IOC依赖注入
        public VideoController(IVideoService service)
        {
            _service = service;
        }

        #region 查询总数
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2 </param>
        /// <param name="type">查询条件</param>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(int identity = 0, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetSumAsync(identity, type, cache));
        }
        #endregion
        #region 查询所有
        /// <summary>
        /// 查询所有 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
         [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache = false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion
        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 用户:2</param>
        /// <param name="type">查询条件</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null", string name = "c", bool cache = false)
        {
            return Ok(await _service.GetContainsAsync(identity, type, name, cache));
        }
        #endregion
        #region 主键查询
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion
        #region  条件查询
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 用户:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("type")]
        public async Task<IActionResult> GetTypeAsync(int identity = 1, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetTypeAsync(identity, type, cache));
        }
        #endregion
        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10,bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetPagingAsync(identity, type, pageIndex, pageSize,  isDesc, cache));
        }
        #endregion
        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 统计标题字数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetStrSumAsync")]
        public async Task<IActionResult> GetSumAsync(bool cache)
        {
            return Ok(await _service.GetSumAsync(cache));
        }

        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<Video>> AddAsync(Video entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        [HttpDelete("DelAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(Video entity)
        {
            var data = await _service.UpdateAsync(entity);
            return Ok(data);
        }
    }
}
