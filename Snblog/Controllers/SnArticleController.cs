using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ISnArticleService _service; //IOC依赖注入

        #region 构造函数
        public SnArticleController(ISnArticleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region 查询总数 (缓存)
        /// <summary>
        /// 查询总数 (缓存)
        /// </summary>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        #endregion
        #region 条件查询总数  (缓存)
        /// <summary>
        /// 条件查询总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("ConutLabel")]
        public IActionResult ConutLabel(int type)
        {
            return Ok(_service.ConutLabel(type));
        }
        #endregion
        #region 查询所有 (缓存)
        /// <summary>
        /// 查询所有 (缓存)
        /// </summary>
        // [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        #endregion
        #region 主键查询 (缓存)
        /// <summary>
        /// 主键查询 (缓存)
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
            return Ok(await _service.AsyGetTestName(id));
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

        //----------------------------------------------------------------
        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 读取[字段/阅读/点赞]总数量
        /// </summary>
        /// <param name="type">text-内容-read:阅读-give:点赞</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type)
        {
            return Ok(await _service.GetSumAsync(type));
        }

        #endregion
        #region 查询文章(无文章内容 缓存)
        /// <summary>
        /// 查询文章(无文章内容 缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        [HttpGet("GetFyTitleAsync")]
        public async Task<IActionResult> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetFyTitleAsync(pageIndex, pageSize, isDesc));
        }
        # endregion
        #region 条件分页查询
        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyTest")]
        public IActionResult GetfyTest(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingWhere(type, pageIndex, pageSize, out _, isDesc));
        }
        #endregion
        # region 条件分页查询
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
        #endregion
        #region 按标签查询 (缓存)
        /// <summary>
        /// 按标签查询(缓存)
        /// </summary>
        /// <param name="labelId">标签id</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        [HttpGet("GetTagtextAsync")]
        public async Task<IActionResult> GetTagtextAsync(int labelId, bool isDesc)
        {
            return Ok(await _service.GetTagtextAsync(labelId, isDesc));
        }
        #endregion
        #region 添加数据
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
        #endregion
        #region 删除数据
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
        #endregion
        #region 更新数据
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
        #endregion
        # region 更新部分列[comment give read]
        /// <summary>
        /// 更新部分列[comment give read]
        /// </summary>
        /// <param name="result">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(SnArticle result, string type)
        {
            var data = await _service.UpdatePortionAsync(result, type);
            return Ok(result);
        }
        #endregion


    }
}
