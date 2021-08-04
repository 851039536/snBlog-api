using Microsoft.AspNetCore.Mvc;
using Snblog.IService.IService;
using System;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    //  [Authorize]
    public class SnInterfaceController : ControllerBase
    {
        private readonly ISnInterfaceService _service; //IOC依赖注入
        #region 构造函数
        public SnInterfaceController(ISnInterfaceService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region   分类查询
        /// <summary>
        /// 分类查询
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="type">分类 </param>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int userId, int type)
        {
            return Ok(await _service.GetTypeAsync(userId,type));
        }
        #endregion
  

    }
}
