using System.Threading.Tasks;
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
    }
}
