using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("userTalk")]
    public class UserTalkController : BaseController
    {
        private readonly IUserTalkService _service;

        public UserTalkController(IUserTalkService service)
        {
            _service = service;
        }

        
        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|用户:1</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0,string type = "null",string name = "winfrom",bool cache = false)
        {
            var data = await _service.GetContainsAsync(identity, type,name,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion
        
        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|用户:1</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|id:主键</param>
        /// <returns>list-entity</returns>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity = 0,string type = "null",int pageIndex = 1,int pageSize = 10,string ordering = "id",bool isDesc = true,bool cache = false)
        {
            var data = await _service.GetPagingAsync(identity,type,pageIndex,pageSize,ordering,isDesc,cache);
            return ApiResponse(cache: cache,data:data );
        }
        #endregion
        

        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsUserTalk")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<UserTalk>> AsyInsUserTalk(UserTalk talk)
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
        public async Task<IActionResult> AysUpUserTalk(UserTalk talk)
        {
            var data = await _service.AysUpUserTalk(talk);
            return Ok(data);
        }
    }
}
