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
    public class SnOneController : Controller
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnOneService _service; //IOC依赖注入
        public SnOneController(ISnOneService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 一文查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOne")]
        public IActionResult GetOne()
        {
            return Ok(_service.GetOne());
        }
        /// <summary>
        /// 一文查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGetOne")]
        public async Task<IActionResult> AsyGetOne()
        {
            return Ok(await _service.AsyGetOne());
        }

        /// <summary>
        /// 按文章id查询
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("AsyGetOneId")]
        public async Task<IActionResult> AsyGetOneId(int id)
        {
            return Ok(await _service.AsyGetOneId(id));
        }

        /// <summary>
        /// 查询一文总条数
        /// </summary>
        /// <returns></returns>
        [HttpGet("OneCount")]
        public IActionResult OneCount()
        {
            return Ok(_service.OneCount());
        }

        /// <summary>
        /// 条件查询一文总数
        /// </summary>
        /// <param name="type">作者</param>
        /// <returns></returns>
        [HttpGet("OneCountType")]
        public IActionResult OneCountType(string type)
        {
            return Ok(_service.OneCountType(type));
        }


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetPagingOne")]
        public IActionResult GetPagingOne(int pageIndex, int pageSize, bool isDesc)
        {
            int count;
            return Ok(_service.GetPagingOne(pageIndex, pageSize, out count, isDesc));
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsOne")]
        public async Task<ActionResult<SnOne>> AsyInsOne(SnOne one)
        {
            return Ok(await _service.AsyInsOne(one));
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetOne")]
        public async Task<IActionResult> AsyDetOne(int id)
        {
            return Ok(await _service.AsyDetOne(id));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPut("AysUpOne")]
        public async Task<IActionResult> AysUpOne(SnOne one)
        {
            var data = await _service.AysUpOne(one);
            return Ok(data);
        }

    }
}
