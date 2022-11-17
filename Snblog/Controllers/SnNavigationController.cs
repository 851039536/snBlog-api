using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    #region 导航内容 SnNavigationController


    /// <summary>
    /// 导航内容
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnNavigationController : ControllerBase
    {
        private readonly ISnNavigationService _service; //IOC依赖注入

        #region SnNavigationController
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnNavigationController(ISnNavigationService service)
        {
            _service = service;
        }
        #endregion

        #region 查询总数 GetCountAsync
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2  </param>
        /// <param name="type">条件(identity为0则填0) </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        [HttpGet("GetSunAsync")]
        public async Task<IActionResult> GetCountAsync(int identity = 0, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetCountAsync(identity, type, cache));
        }
        #endregion

        #region 查询所有GetAllAsync
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache = false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion

        #region 模糊查询 Contains
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 用户:2</param>
        /// <param name="type">查询条件:用户||分类</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetContainsAsync")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null", string name = "c", bool cache = false)
        {
            return Ok(await _service.GetContainsAsync(identity, type, name, cache));
        }
        #endregion

        #region 主键查询GetByIdAsync
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region 条件查询 GetTypeAsync
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 用户:2</param>
        /// <param name="type">查询条件</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int identity = 1, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetTypeAsync(identity, type, cache));
        }
        #endregion

        #region 分页查询GetFyAsync
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 按id排序]</param>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10, string ordering = "id", bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetFyAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache));
        }
        #endregion

        #region 添加数据AddAsync
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

        #region 更新数据 UpdateAsync
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

        #region 删除数据DeleteAsync
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("DelAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
    #endregion
}
