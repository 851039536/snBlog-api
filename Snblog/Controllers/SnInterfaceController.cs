using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using System;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    //  [Authorize]
    public class SnInterfaceController : ControllerBase
    {
        private readonly ISnInterfaceService _service; //IOC依赖注入
        #region 构造函数 SnInterfaceController
        public SnInterfaceController(ISnInterfaceService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion
        #region  条件查询 GetTypeAsync
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类和用户:0 || 用户:1 || 分类:2</param>
        /// <param name="users">条件:用户</param>
        /// <param name="type">条件:类别</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int identity=0, string users="null", string type= "null", bool cache=false)
        {
            return Ok(await _service.GetTypeAsync(identity, users, type, cache));
        }
        #endregion
        #region  查询所有GetAllAsync
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache=false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion
        #region 分页查询 
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户名:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[按id排序]</param>
        /// <returns></returns>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int identity=0, string type="null", int pageIndex=1, int pageSize=10, string ordering="id", bool isDesc=true, bool cache=false)
        {
            return Ok(await _service.GetFyAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache));
        }
        #endregion



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(SnInterface entity)
        {
            return Ok(await _service.AddAsync(entity));
        }

        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(SnInterface entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion

        #region 删除数据DeleteAsync
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("DelAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }

}
