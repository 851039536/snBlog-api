using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnNavigationController : ControllerBase
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnNavigationService _service; //IOC依赖注入
        public SnNavigationController(ISnNavigationService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSnNavigation")]
        public IActionResult GetSnNavigation()
        {
            return Ok(_service.GetSnNavigation());
        }

        /// <summary>
        ///根据id查询
        /// </summary>
        /// <param name="id">Navigation id</param>
        /// <returns></returns>
        [HttpGet("GetNavigationId")]
        public IActionResult GetNavigationId(int id)
        {
            return Ok(_service.GetNavigationId(id));
        }
        /// <summary>
        /// Navigation表总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNavigationCount")]
        public IActionResult GetNavigationCount()
        {
            return Ok(_service.GetNavigationCount());
        }

        /// <summary>
        /// 条件查询Navigation总数
        /// </summary>
        /// <param name="type">分类</param>
        /// <returns></returns>
        [HttpGet("GetNavigationCountType")]
        public IActionResult GetNavigationCountType(string type)
        {
            return Ok(_service.GetNavigationCount(type));
        }

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        [HttpGet("GetDistTest")]
        public IActionResult GetDistTest(string type)
        {

            return Ok(_service.GetDistTest(type));
        }
        /// <summary>
        /// 条件-排序-查询
        /// </summary>
        /// <param name="type">分类条件</param>
        /// <param name="fag">排序</param>
        /// <returns>List</returns>
        [HttpGet("AsyGetWhereTest")]
        public async Task<IActionResult> AsyGetWhereTest(string type, bool fag)
        {
            return Ok(await _service.AsyGetWhereTest(type, fag));
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type">查询条件:all -表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyNavigation")]
        public IActionResult GetfyNavigation(string type, int pageIndex, int pageSize, bool isDesc)
        {
            int count;
            return Ok(_service.GetPagingWhere(type, pageIndex, pageSize, out count, isDesc));
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyIntNavigation")]
        public async Task<ActionResult<SnNavigation>> AsyIntNavigation(SnNavigation test)
        {
            return Ok(await _service.AsyIntNavigation(test));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpNavigation")]
        public async Task<IActionResult> AysUpNavigation(SnNavigation test)
        {
            var data = await _service.AysUpNavigation(test);
            return Ok(data);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDelNavigation")]
        public async Task<IActionResult> AsyDelNavigation(int id)
        {
            return Ok(await _service.AsyDelNavigation(id));
        }

    }
}
