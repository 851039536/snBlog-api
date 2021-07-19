using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService;
using Snblog.IService.IService;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnOneController : ControllerBase
    {
        private readonly ISnOneService _service; //IOC依赖注入
        public SnOneController(ISnOneService service)
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
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
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
        #region 条件查总数（缓存）
        /// <summary>
        /// 条件查总数（缓存）
        /// </summary>
        /// <param name="type">分类</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public async Task<IActionResult> CountTypeAsync(int type)
        {
            return Ok(await _service.CountTypeAsync(type));
        }
        #endregion
        #region  读取[字段/阅读/点赞]总数量/缓存

        /// <summary>
        /// 读取[字段/阅读/点赞]总数量/缓存
        /// </summary>
        /// <param name="type">text:内容字段数-read:阅读数量-give:点赞数量</param>
        /// <returns></returns>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type)
        {
            return Ok(await _service.GetSumAsync(type));
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
        #region 条件分页查询（缓存）
        /// <summary>
        /// 条件分页查询（缓存）
        /// </summary>
        /// <param name="type">查询条件[999查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="name">排序条件[data:时间,read:阅读,give:点赞,comment:评论]默认按id排序</param>
        /// <returns></returns>
        [HttpGet("GetFyTypeAsync")]
        public async Task<IActionResult> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            return Ok(await _service.GetFyTypeAsync(type, pageIndex, pageSize, name, isDesc));
        }
        #endregion
        #region 添加数据 （权限）
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnOne>> AddAsync(SnOne entity)
        {
            return Ok(await _service.AddAsync(entity));
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
        #region 更新数据 （权限）
        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnOne entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion

         #region 更新部分列[comment give read]
        /// <summary>
        /// 更新点赞[ give ]
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(SnOne entity, string type) => Ok(await _service.UpdatePortionAsync(entity, type));
        #endregion

    }
}
