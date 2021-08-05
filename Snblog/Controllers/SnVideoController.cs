using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;
using Snblog.Repository.Repository;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    
    [ApiController]
    public class SnVideoController : Controller
    {
        private readonly ISnVideoService _service; //IOC依赖注入
        public SnVideoController(ISnVideoService service)
        {
            _service = service;
        }



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

        /// <summary>
        /// 按条件查询总数
        /// </summary>
        /// <param name="typeId">分类条件</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTypeCountAsync")]
        public async Task<IActionResult> GetTypeCountAsync(int typeId, bool cache)
        {
            return Ok(await _service.GetTypeCount(typeId, cache));
        }

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

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="type">分类条件: 9999表示查询所有s</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetFyAsync(type, pageIndex, pageSize, isDesc, cache));
        }


        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">分类id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTypeAllAsync")]
        public async Task<IActionResult> GetTypeAllAsync(int type, bool cache)
        {
            return Ok(await _service.GetTypeAllAsync(type, cache));
        }

        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 统计标题字数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(bool cache)
        {
            return Ok(await _service.GetSumAsync(cache));
        }

        #endregion


        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnVideo>> AddAsync(SnVideo entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        /// <summary>
        /// 删除视频 （权限）
        /// </summary>
        /// <param name="id">视频id</param>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }



        /// <summary>
        /// 更新视频 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnVideo entity)
        {
            var data = await _service.UpdateAsync(entity);
            return Ok(data);
        }
    }
}
