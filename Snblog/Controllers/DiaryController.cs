using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    /// <summary>
    ///日记
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("diary")]
    public class DiaryController : BaseController
    {
        private readonly IDiaryService _service; 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public DiaryController(IDiaryService service)
        {
            _service = service;
        }

        #region 查询总数
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
        /// <param name="type">条件(identity为0则null) </param>
        /// <param name="cache"></param>
        /// <returns>int</returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(int identity = 0, string type = "null", bool cache = false)
        {
            var data = await _service.GetSumAsync(identity,type,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion

        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">无条件:0 || 分类:1 || 标签:2</param>
        /// <param name="type">分类</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null", string name = "c", bool cache = false)
        {
            var data = await _service.GetContainsAsync(identity,type,name,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion

        #region 主键查询
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            var data = await _service.GetByIdAsync(id,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion

        #region 统计[字段/阅读/点赞]总数量

        /// <summary>
        /// 统计[字段/阅读/点赞]总数量
        /// </summary>
        /// <param name="type">text:内容字段数-read:阅读数量-give:点赞数量</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("strSum")]
        public async Task<IActionResult> GetSumAsync(string type, bool cache)
        {
            var data = await _service.GetSumAsync(type,cache);
            return ApiResponse(data: data,cache:cache);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 read:阅读 give:点赞 按id排序]</param>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10, string ordering = "id", bool isDesc = true, bool cache = false)
        {
            var data = await _service.GetPagingAsync(identity, type,pageIndex,pageSize,ordering,isDesc,cache);
            return ApiResponse(cache:cache,data:data);
        }
        #endregion

        #region 添加数据 
        /// <summary>
        /// 添加数据 
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> AddAsync(Diary entity)
        {
            var data = await _service.AddAsync(entity);
            return ApiResponse(data: data);
        }
        #endregion

        #region 删除数据 
        /// <summary>
        /// 删除数据 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("del")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DelAsync(int id)
        {
            var data = await _service.DelAsync(id);
            return ApiResponse(data: data);
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新数据 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(Diary entity)
        {
            var data = await _service.UpdateAsync(entity);
            return ApiResponse(data: data);
        }
        #endregion

        #region 更新部分列
        /// <summary>
        /// 更新部分列[give read]
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("upPortion")]
        public async Task<IActionResult> UpdatePortionAsync(Diary entity, string type)
        {
            var data = await _service.UpdatePortionAsync(entity, type);
            return ApiResponse(data: data);
        }
        #endregion

    }
}
