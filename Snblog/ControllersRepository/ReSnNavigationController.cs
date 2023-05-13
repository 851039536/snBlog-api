using Snblog.IService.IReService;

namespace Snblog.ControllersRepository
{
    /// <summary>
    /// ReSnNavigationController
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V2")] //版本控制
    [ApiController]
    public class ReSnNavigationController : ControllerBase
    {
        private readonly IReSnNavigationService _service; //IOC依赖注入

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ReSnNavigationController(IReSnNavigationService service)
        {
            _service = service;
        }
        #region 查询所有(缓存)
        /// <summary>
        /// 查询所有(缓存)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        #endregion
        #region 主键查询 (缓存)
        /// <summary>
        ///主键查询 (缓存)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        #endregion
        #region 查询总数(缓存)
        /// <summary>
        /// 查询总数(缓存)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSunAsync")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _service.GetCountAsync());
        }
        #endregion
        #region 条件查询总数(缓存)
        /// <summary>
        /// 条件查询总数(缓存)
        /// </summary>
        /// <param name="type">类别</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public async Task<IActionResult> CountTypeAsync(string type)
        {
            return Ok(await _service.CountTypeAsync(type));
        }
        #endregion
        #region 去重查询 (缓存)
        /// <summary>
        /// 去重查询 (缓存)
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        [HttpGet("GetDistinct")]
        public async Task<IActionResult> GetDistinct(string type)
        {
            return Ok(await _service.GetDistinct(type));
        }
        #endregion
        #region 条件查询 (缓存 排序)
        /// <summary>
        /// 条件查询 (缓存 排序)
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <returns>List</returns>
        [HttpGet("GetTypeOrderAsync")]
        public async Task<IActionResult> GetTypeOrderAsync(string type, bool order)
        {
            return Ok(await _service.GetTypeOrderAsync(type, order));
        }
        #endregion
        #region 分页查询 (缓存 排序 分页)
        /// <summary>
        /// 分页查询 (缓存 排序 分页)
        /// </summary>
        /// <param name="type">查询条件:all -表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetFyAllAsync")]
        public async Task<IActionResult> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetFyAllAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 添加数据 (权限)
        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnNavigation>> AddAsync(SnNavigation entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 更新数据 (权限)
        /// <summary>
        /// 更新数据 (权限)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> UpdateAsync(SnNavigation entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
        #region 删除数据 (权限)
        /// <summary>
        /// 删除数据 (权限)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete("DelAsync")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }
}
