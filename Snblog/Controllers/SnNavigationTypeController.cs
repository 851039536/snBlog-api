using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{

    /// <summary>
    /// 导航表分类
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnNavigationTypeController : Controller
    {
        private readonly ISnNavigationTypeService _service; //IOC依赖注入
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnNavigationTypeController(ISnNavigationTypeService service)
        {
            _service = service;
        }

        #region 查询总数
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync(bool cache)
        {
            return Ok(await _service.CountAsync(cache));
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

        #region 主键查询
        /// <summary>
        ///  主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="type">类型[all查所有]</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetFyTypeAllAsync")]
        public async Task<IActionResult> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetFyTypeAllAsync(type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(SnNavigationType entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 删除数据 
        /// <summary>
        /// 删除数据 
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("DelAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
        #region 更新数据
        /// <summary>
        /// 更新数据 
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(SnNavigationType entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion


    }
}
