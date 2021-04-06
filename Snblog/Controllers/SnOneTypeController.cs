using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnOneTypeController : Controller
    {

        private readonly ISnOneTypeService _service; //IOC依赖注入
        public SnOneTypeController(ISnOneTypeService service)
        {
            _service = service;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetFirst")]
        public async Task<IActionResult> GetFirst(int id)
        {
            return Ok(await _service.GetFirst(id));
        }

        /// <summary>
        /// 类别查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("GetTypeFirst")]
        public async Task<IActionResult> GetTypeFirst(int type)
        {
            return Ok(await _service.GetTypeFirst(type));
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAsync")]

        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }

        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AddAsync(SnOneType Entity)
        {
            return Ok(await _service.AddAsync(Entity));
        }

        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
