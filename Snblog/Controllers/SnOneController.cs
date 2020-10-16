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
        [HttpGet("ConutLabel")]
        public IActionResult OneCountType(string type)
        {
            return Ok(_service.OneCountType(type));
        }

    }
}
