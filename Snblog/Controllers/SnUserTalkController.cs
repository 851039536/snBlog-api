using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnUserTalkController : Controller
    {
        private readonly ISnUserTalkService _service; //IOC依赖注入

        public SnUserTalkController(ISnUserTalkService service)
        {
            _service = service;
        }

        /// <summary>
        /// 说说查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGetUserTalk")]
        public async Task<IActionResult> AsyGetUserTalk()
        {
            return Ok(await _service.GetAll());
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
            return Ok(await _service.DelAsync(id));
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
