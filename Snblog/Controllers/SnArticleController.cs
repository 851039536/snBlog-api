using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snblog.IService;
using Snblog.Models;
using Snblog.Repository;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class SnArticleController : ControllerBase
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnArticleService _service; //IOC依赖注入
        public SnArticleController(ISnArticleService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 查询文章总条数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetArticleCount")]
        public IActionResult GetArticleCount()
        {
            return Ok(_service.GetArticleCount());
        }

        /// <summary>
        /// 条件查询文章总数
        /// </summary>
        /// <param name="type">标签分类</param>
        /// <returns></returns>
        [HttpGet("ConutLabel")]
        public IActionResult ConutLabel(int type)
        {
            return Ok(_service.ConutLabel(type));
        }

        /// <summary>
        /// 按分类id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetTestWhere")]
        public IActionResult GetTestWhere(int SortId)
        {
            return Ok(_service.GetTestWhere(SortId));
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
            int count;
            return Ok(_service.GetPagingWhere(label, pageIndex, pageSize, out count, isDesc));
        }

        /// <summary>
        /// id条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
            return Ok(await _service.AsyGetTestName(id));
        }

        /// <summary>
        /// 标签条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("AsyGetTestString")]
        public async Task<IActionResult> AsyGetTestString(int LabelId)
        {
            var query = from c in _coreDbContext.SnArticle
                        where c.LabelId == LabelId
                        select new { c.ArticleId, c.TitleText, c.Title, c.Time, c.Read, c.Give };
            return Ok(await query.ToListAsync());
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsArticle")]
        public async Task<ActionResult<SnArticle>> AsyInsArticle(SnArticle test)
        {
            return Ok(await _service.AsyInsArticle(test));
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetArticleId")]
        public async Task<IActionResult> AsyDetArticleId(int id)
        {
            return Ok(await _service.AsyDetArticleId(id));
        }

        /// <summary>
        /// 异步更新数据
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
