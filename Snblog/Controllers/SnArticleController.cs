using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    //  [Authorize]
    public class SnArticleController : ControllerBase
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnArticleService _service; //IOC依赖注入
        private readonly ILogger<SnArticleController> _logger; // <-添加此行
        #region 构造函数
        public SnArticleController(ISnArticleService service, snblogContext coreDbContext, ILogger<SnArticleController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _coreDbContext = coreDbContext ?? throw new ArgumentNullException(nameof(coreDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion



        #region 查询总数 (缓存)
        /// <summary>
        /// 查询总数 (缓存)
        /// </summary>
        [HttpGet("GetArticleCount")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult GetArticleCount()
        {
            return Ok(_service.GetArticleCount());
        }
        #endregion
        #region 分类ID查询总数  (缓存)
        /// <summary>
        /// 分类ID查询总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("ConutLabel")]
        public IActionResult ConutLabel(int type)
        {
            return Ok(_service.ConutLabel(type));
        }
        #endregion
        #region  分类ID查询 (缓存)
        /// <summary>
        ///分类ID查询 (缓存)
        /// </summary>
        /// <param name="sortId">分类id</param>
        [HttpGet("GetTestWhere")]
        public IActionResult GetTestWhere(int sortId)
        {
            return Ok(_service.GetTestWhere(sortId));
        }
        #endregion


        /// <summary>
        /// 查询所有(Linq)
        /// </summary>
        // [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        /// <summary>
        /// 查询总数(Linq)
        /// </summary>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        /// <summary>
        /// 读取[字段/阅读/点赞]总数量
        /// </summary>
        /// <param name="type">text:内容字段数-read:阅读数量-give:点赞数量</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type)
        {
            return Ok(await _service.GetSumAsync(type));
        }


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
            return Ok(_service.GetTest());
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyTest")]
        public IActionResult GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingWhere(label, pageIndex, pageSize, out _, isDesc));
        }
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type">查询条件[999查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="name">排序条件[data:时间,read:阅读,give:点赞,comment:评论]默认按id排序</param>
        /// <returns></returns>
        [HttpGet("GetFyTypeAsync")]
        public async Task<IActionResult> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            return Ok(await _service.GetFyTypeAsync(type, pageIndex, pageSize, name, isDesc));
        }


        /// <summary>
        /// 按文章id查询 (缓存)
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
            return Ok(await _service.AsyGetTestName(id));
        }
        /// <summary>
        /// 标签条件查询
        /// </summary>
        /// <param name="labelId">标签id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTestString")]
        public async Task<IActionResult> AsyGetTestString(int labelId)
        {
            var query = from c in _coreDbContext.SnArticle
                        where c.LabelId == labelId
                        select new { c.ArticleId, c.TitleText, c.Title, c.Time, c.Read, c.Give };
            return Ok(await query.ToListAsync());
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "kai")] //角色授权
        [HttpPost("AsyInsArticle")]
        public async Task<ActionResult<SnArticle>> AsyInsArticle(SnArticle test)
        {
            return Ok(await _service.AsyInsArticle(test));
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "kai")] //角色授权
        [HttpDelete("AsyDetArticleId")]
        public async Task<IActionResult> AsyDetArticleId(int id)
        {
            return Ok(await _service.AsyDetArticleId(id));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpArticle")]
        public async Task<IActionResult> AysUpArticle(SnArticle test)
        {
            var data = await _service.AysUpArticle(test);
            return Ok(data);
        }




    }
}
