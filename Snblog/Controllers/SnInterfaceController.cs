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
        /// 根据用户分类查询
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="type">分类 </param>
        /// <param name="cache">缓存</param>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int userId, int type,bool cache)
        {
            return Ok(await _service.GetTypeAsync(userId,type,cache));
        }
        #endregion

        #region 查询所有
        /// <summary>
        /// 查询所有 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion

        #region  条件分页查询
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="userId">00-表示查询所有</param>
        /// <param name="type">分类 </param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetTypefyAsync")]
        public async Task<IActionResult> GetTypefyAsync(int userId,int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetTypefyAsync(userId,type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion
    }
}
