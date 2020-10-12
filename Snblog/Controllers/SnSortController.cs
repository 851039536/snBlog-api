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
    public class SnSortController : ControllerBase
    {
        private readonly ISnSortService _service; //IOC依赖注入

         public SnSortController(ISnSortService service)
         {
             _service=service;
         }

         /// <summary>
        /// 同步查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSort")]
        public IActionResult GetSort()
        {
          return Ok(_service.GetSort());
         }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsSort")]
        public async Task<ActionResult<SnSort>> AsyInsLabels(SnSort test)
        {
            return Ok(await _service.AsyInsSort(test));
        }

           /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpSort")]
        public async Task<IActionResult> AysUpSort(SnSort test)
        {
           var data= await  _service.AysUpSort(test);
           return Ok(data);
        }

         /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetSort")]
        public async Task<IActionResult> AsyDetSort(int id)
        {
          return Ok(await _service.AsyDetSort(id));
        }

    }
}
