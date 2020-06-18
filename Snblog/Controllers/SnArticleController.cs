using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnArticleController : ControllerBase
    {
          private readonly ISnArticleService _service; //IOC依赖注入
         public SnArticleController(ISnArticleService service)
         {
             _service=service;
         }

        /// <summary>
        /// 同步查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
          return Ok(_service.GetTest());
         }


        //条件查询
         [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
          return Ok(await _service.AsyGetTestName(id));
        }
    }
}
