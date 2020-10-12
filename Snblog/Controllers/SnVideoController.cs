using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    public class SnVideoController : Controller
    {
        private readonly snblogContext _coreDbContext;
          private readonly ISnVideoService _service; //IOC依赖注入
         public SnVideoController(ISnVideoService service , snblogContext coreDbContext)
         {
             _service=service;
             _coreDbContext = coreDbContext;
         }

        /// <summary>
        /// 视频查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
          return Ok(_service.GetTest());
         }
        /// <summary>
        /// 视频查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGestTest")]
        public async Task<IActionResult> AsyGestTest()
        {
            return Ok(await _service.AsyGetTest());
        }

        /// <summary>
        /// 分类查询
        /// </summary>
        /// <param name="type">int类型</param>
        /// <returns></returns>
         [HttpGet("GetTestWhere")]
        public IActionResult GetTestWhere(int type)
        {
            return Ok( _service.GetTestWhere(type));
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("AsyDetVideo")]
        public async Task<IActionResult> AsyDetVideo(int id)
        {
          return Ok(await _service.AsyDetVideo(id));
        }

        /// <summary>
        /// 添加视频
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsVideo")]
        public async Task<ActionResult<SnVideo>> AsyInsVideo(SnVideo test)
        {
            return Ok(await _service.AsyInsVideo(test));
        }

         /// <summary>
        /// 更新视频
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpVideo")]
        public async Task<IActionResult> AysUpVideo(SnVideo test)
        {
           var data=await  _service.AysUpVideo(test);
           return Ok(data);
        }
    }
}
