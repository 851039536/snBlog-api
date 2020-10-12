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
         public SnOneController(ISnOneService service , snblogContext coreDbContext)
         {
             _service=service;
             _coreDbContext = coreDbContext;
         }

        /// <summary>
        /// 文章查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
          return Ok(_service.GetTest());
         }
      
    }
}
