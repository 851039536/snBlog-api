namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnUserTalkController : Controller
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnUserTalkService _service; //IOC依赖注入

        public SnUserTalkController(ISnUserTalkService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 说说查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGetUserTalk")]
        public async Task<IActionResult> AsyGetUserTalk()
        {
            return Ok(await _service.AsyGetUserTalk());
        }

        /// <summary>
        /// 主键id查询
        /// </summary>
        /// <param name="talkId">主键id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTalk")]
        public async Task<IActionResult> AsyGetTalk(int talkId)
        {
            return Ok(await _service.AsyGetTalk(talkId));
        }

        /// <summary>
        /// 查询当前用户的说说
        /// </summary>
        /// <param name="userId">查询条件</param>
        /// <param name="isdesc">排序方式</param>
        /// <returns></returns>
        [HttpGet("GetUserTalkFirst")]
        public IActionResult GetUserTalkFirst(int userId, bool isdesc)
        {
            return Ok(_service.GetUserTalkFirst(userId, isdesc));
        }

        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTalkCount")]
        public IActionResult GetTalkCount()
        {
            return Ok(_service.GetTalkCount());
        }

        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpGet("UserTalkTypeConut")]
        public IActionResult UserTalkTypeConut(int userId)
        {
            return Ok(_service.UserTalkTypeConut(userId));
        }

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetPagingUserTalk")]
        public IActionResult GetPagingUserTalk(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingUserTalk(1, pageIndex, pageSize, out _, isDesc));
        }

        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsUserTalk")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<SnUserTalk>> AsyInsUserTalk(SnUserTalk talk)
        {
            return Ok(await _service.AsyInsUserTalk(talk));
        }
        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetUserTalk")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AsyDetUserTalk(int id)
        {
            return Ok(await _service.AsyDetUserTalk(id));
        }

        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        [HttpPut("AysUpArticle")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AysUpUserTalk(SnUserTalk talk)
        {
            var data = await _service.AysUpUserTalk(talk);
            return Ok(data);
        }
    }
}
