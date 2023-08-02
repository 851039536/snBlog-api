using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    [ApiExplorerSettings(GroupName = "V1")]
    [ApiController]
    [Route("userTalk")]
    public class UserTalkController : BaseController
    {
        private readonly IUserTalkService _service;
        private readonly IValidator <UserTalk> _validator;
        public UserTalkController(IUserTalkService service, IValidator<UserTalk> validator)
        {
            _service = service;
            _validator = validator;
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
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null",
            string name = "winfrom", bool cache = false)
        {
            var data = await _service.GetContainsAsync(identity, type, name, cache);
            return ApiResponse(cache: cache, data: data);
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
        public async Task<IActionResult> GetPagingAsync(int identity = 0, string type = "null", int pageIndex = 1,
            int pageSize = 10, string ordering = "id", bool isDesc = true, bool cache = false)
        {
            var data = await _service.GetPagingAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache);
            return ApiResponse(cache: cache, data: data);
        }

        #endregion


        #region 添加

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [HttpPost("add")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(UserTalk entity)
        {
            var ret = await _validator.ValidateAsync(entity);
            if (!ret.IsValid)
            {
                return ApiResponse(statusCode: 404, message: ret.Errors[0].ErrorMessage, data: entity);
            }
            return ApiResponse(data: await _service.AddAsync(entity));
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("del")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DelAsync(int id)
        {
            var data = await _service.DelAsync(id);
            return ApiResponse(data: data);
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [HttpPut("update")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(UserTalk entity)
        {
            var data = await _service.UpdateAsync(entity);
            return ApiResponse(data: data);
        }

        #endregion
    }
}