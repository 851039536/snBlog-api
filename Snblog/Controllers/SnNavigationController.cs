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
    }
}
