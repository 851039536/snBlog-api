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
            private readonly ISnNavigationService _service; //IOC依赖注入
         public SnNavigationController(ISnNavigationService service)
         {
             _service=service;
         }

        /// <summary>
        /// 同步查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSnNavigation")]
        public IActionResult GetSnNavigation()
        {
          return Ok(_service.GetSnNavigation());
         }
          /// <summary>
         /// 查询Navigation表总数
         /// </summary>
         /// <returns></returns>
         [HttpGet("GetNavigationCount")]
        public IActionResult GetNavigationCount()
        {
            return Ok( _service.GetNavigationCount());
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyIntNavigation")]
        public async Task<ActionResult<SnNavigation>> AsyIntNavigation(SnNavigation test)
        {
            return Ok(await _service.AsyIntNavigation(test));
        }

        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpNavigation")]
        public async Task<IActionResult> AysUpNavigation(SnNavigation test)
        {
           var data=await Task.Run(()=> _service.AysUpNavigation(test));
           return Ok(data);
        }

                /// <summary>
                /// 异步删除数据
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
