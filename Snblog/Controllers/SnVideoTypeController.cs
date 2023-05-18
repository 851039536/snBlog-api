namespace Snblog.Controllers
{
    /// <summary>
    /// 视频分类
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")]
    [ApiController]
    [Route("videoType")]
    public class SnVideoTypeController : BaseController
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnVideoTypeService _service; //IOC依赖注入

        public SnVideoTypeController(ISnVideoTypeService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return ApiResponse(data: data);
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            var data = await _service.GetAllAsync(id);
            return ApiResponse(data: data);
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            var data = await _service.CountAsync();
            return ApiResponse(data: data);
        }

        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(SnVideoType Entity)
        {
            return Ok(await _service.AddAsync(Entity));
        }

        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        [HttpDelete("DelectAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DelectAsync(SnVideoType Entity)
        {
            return Ok(await _service.DeleteAsync(Entity));
        }

        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnVideoType Entity)
        {
            return Ok(await _service.UpdateAsync(Entity));
        }
    }
}