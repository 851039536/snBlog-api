using Snblog.Util.GlobalVar;

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
        private readonly ISnVideoTypeService _service;

        public SnVideoTypeController(ISnVideoTypeService service)
        {
            _service = service;
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
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(SnVideoType entity)
        {
            return Ok(await _service.AddAsync(entity));
        }

        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete("del")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(SnVideoType entity)
        {
            return Ok(await _service.DeleteAsync(entity));
        }

        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnVideoType entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
    }
}