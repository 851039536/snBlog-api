using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;
using Snblog.Repository.Repository;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnVideoController : Controller
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnVideoService _service; //IOC依赖注入
        public SnVideoController(ISnVideoService service, snblogContext coreDbContext)
        {
            _service = service;
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
        /// id查询视频
        /// </summary>
        /// <param name="id">视频id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTestId")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
            return Ok(await _service.AsyGetTestId(id));
        }

        /// <summary>
        /// 分页查询视频 - 支持排序
        /// </summary>
        /// <param name="type">分类条件:99999 -表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyVideo")]
        public IActionResult GetfyVideo(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingWhere(type, pageIndex, pageSize, out _, isDesc));
        }


        /// <summary>
        /// 视频总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVideoCount")]
        public IActionResult GetVideoCount()
        {
            return Ok(_service.GetVideoCount());
        }


        /// <summary>
        /// 条件查视频总数
        /// </summary>
        /// <param name="typeId">视频分类id</param>
        /// <returns></returns>
        [HttpGet("GetVideoCountType")]
        public IActionResult GetVideoCountType(int typeId)
        {
            return Ok(_service.GetVideoCount(typeId));
        }


        /// <summary>
        /// 分类查询
        /// </summary>
        /// <param name="type">ID</param>
        /// <returns></returns>
        [HttpGet("GetTestWhere")]
        public IActionResult GetTestWhere(int type)
        {
            return Ok(_service.GetTestWhere(type));
        }

        /// <summary>
        /// 删除视频 （权限）
        /// </summary>
        /// <param name="id">视频id</param>
        /// <returns></returns>
        [HttpDelete("AsyDetVideo")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AsyDetVideo(int id)
        {
            return Ok(await _service.AsyDetVideo(id));
        }

        /// <summary>
        /// 添加视频 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsVideo")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnVideo>> AsyInsVideo(SnVideo test)
        {
            return Ok(await _service.AsyInsVideo(test));
        }

        /// <summary>
        /// 更新视频 （权限）
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpVideo")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AysUpVideo(SnVideo test)
        {
            var data = await _service.AysUpVideo(test);
            return Ok(data);
        }
    }
}
