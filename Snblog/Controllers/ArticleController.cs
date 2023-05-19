using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController] //控制路由
    [Route("article")]
    public class ArticleController : BaseController
    {
        //服务
        private readonly IArticleService _service;
        private readonly IValidator<Article> _validator;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ArticleController(IServiceProvider service)
        {
            _service = service.GetRequiredService<IArticleService>();
            _validator = service.GetRequiredService<IValidator<Article>>();
        }
        #endregion

        #region 查询总数
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
        /// <param name="type">条件</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(int identity = 0,string type = null,bool cache = false)
        {
            int data = await _service.GetSumAsync(identity,type,cache);
            return ApiResponse(cache:cache, data:data);
        }
        #endregion

        #region 查询所有
        /// <summary>
        /// 查询所有 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns>list-entity</returns>
        //[ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync(bool cache = false)
        {
            var data = await _service.GetAllAsync(cache);
            return ApiResponse(data: data);
        }
        #endregion

        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0,string type = "null",string name = "winfrom",bool cache = false)
        {
            var data = await _service.GetContainsAsync(identity, type,name,cache);
            return ApiResponse(cache: cache,data: data);
        }

        /// <summary>
        /// ml
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("ml")]
        public IActionResult GetMLTest(string name)
        {
            //Load sample data
            var sampleData = new TextMLModel.ModelInput() {
                Title = @name,
            };
            //Load model and predict output
            var result =  TextMLModel.Predict(sampleData);

            return ApiResponse( data: result.PredictedLabel);
        }
        #endregion

        #region 主键查询 
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id,bool cache = false)
        {
            var data = await _service.GetByIdAsync(id, cache);
            return ApiResponse(cache:cache, data: data);
        }
        #endregion

        #region  类别查询
        /// <summary>
        ///类别查询
        /// </summary>
        /// <param name="identity">分类:1|标签:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">缓存</param>
        [HttpGet("type")]
        public async Task<IActionResult> GetTypeAsync(int identity = 1,string type = "null",bool cache = false)
        {
            var data = await _service.GetTypeAsync(identity, type,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion

        #region 内容统计
        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        [HttpGet("strSum")]
        public async Task<IActionResult> GetStrSumAsync(int identity = 0,int type = 1,string name = "null",bool cache = false)
        {
            var data = await _service.GetStrSumAsync(identity,type,name,cache);
            return ApiResponse(cache: cache,data: data);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|read:阅读|give:点赞|id:主键</param>
        /// <returns>list-entity</returns>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity = 0,string type = "null",int pageIndex = 1,int pageSize = 10,string ordering = "id",bool isDesc = true,bool cache = false)
        {
            var data = await _service.GetPagingAsync(identity,type,pageIndex,pageSize,ordering,isDesc,cache);
            return ApiResponse(cache: cache,data:data );
        }
        #endregion

        #region 新增
        /// <summary>
        ///  新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
       [Authorize(Roles = Permissions.Name)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Article entity)
        {
            var result = _validator.Validate(entity);
            if (!result.IsValid) {
                return ApiResponse(statusCode: 404,message: result.Errors[0].ErrorMessage,data: entity);
            }

            var data = await _service.AddAsync(entity);
            return ApiResponse(data:data);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(Article entity)
        {
            var data = await _service.UpdateAsync(entity);
            return ApiResponse(data:data );
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("del")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.DelAsync(id);
            return ApiResponse(data: data);
        }
        #endregion

        #region 条件更新
        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="type">更新字段: Read | Give | Comment</param>
        /// <returns>bool</returns>
        [HttpPut("upPortion")]
        public async Task<IActionResult> UpdatePortionAsync(Article entity,string type)
        {
            var data = await _service.UpdatePortionAsync(entity,type);
            return ApiResponse(data: data);
        }
        #endregion

    }
}
