﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;
//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class SnVideoTypeController : Controller
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnVideoTypeService _service; //IOC依赖注入
        public SnVideoTypeController(ISnVideoTypeService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 分类视频查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGestTest")]
        public async Task<IActionResult> AsyGestTest()
        {
            return Ok(await _service.AsyGetTest());
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            return Ok(await _service.GetAllAsync(id));
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
        /// 添加数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
         [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(SnVideoType Entity)
        {
            return Ok(await _service.AddAsync(Entity));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
         [HttpDelete("DelectAsync")]
        public async Task<IActionResult> DelectAsync(SnVideoType Entity)
        {
            return Ok(await _service.DeleteAsync(Entity));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(SnVideoType Entity)
        {
            return Ok(await _service.UpdateAsync(Entity));
        }
    }
}
