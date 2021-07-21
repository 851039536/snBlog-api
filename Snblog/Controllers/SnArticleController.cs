using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Service.AngleSharp;

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
        /// 查询总数 
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync(bool cache) => Ok(await _service.CountAsync(cache));
        #endregion
        #region 条件查询总数  (缓存)
        /// <summary>
        /// 条件查询总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("ConutLabel")]
        public IActionResult ConutLabel(int type) => Ok(_service.ConutLabel(type));

        /// <summary>
        /// 查询分类总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("ConutSort")]
        public IActionResult ConutSort(int type) => Ok(_service.ConutSort(type));
        #endregion
        #region 查询所有 (缓存)
        /// <summary>
        /// 查询所有 (缓存)
        /// </summary>
        // [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());
        #endregion
        #region 主键查询 (缓存)
        /// <summary>
        /// 主键查询 (缓存)
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id) => Ok(await _service.AsyGetTestName(id));
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
        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 读取[字段/阅读/点赞]总数量-缓存
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
        #endregion
        #region  条件分页查询 (缓存)
        /// <summary>
        /// 按标签分页查询  (缓存)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyTestAsync")]
        public async Task<IActionResult> GetfyTestAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetPagingWhereAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
         #region  条件分页查询 (缓存)
        /// <summary>
        /// 按分类分页查询  (缓存)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfySortTestAsync")]
        public async Task<IActionResult> GetfySortTestAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetPagingSortWhereAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 分页查询(条件排序 缓存)
        /// <summary>
        /// 分页查询(条件排序 缓存)
        /// </summary>
        /// <param name="type">查询条件[00查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
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
        #region 标签ID查询 (缓存)
        /// <summary>
        /// 标签ID查询 (缓存)
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
        /// <param name="entity"></param>
        /// <returns></returns>
        //[Authorize(Roles = "kai")] //角色授权
        [HttpPost("AddAsync")]
        public async Task<ActionResult<SnArticle>> AddAsync(SnArticle entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(SnArticle entity) => Ok(await _service.UpdateAsync(entity));
        #endregion
        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //  [Authorize(Roles = "kai")] //角色授权
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id) => Ok(await _service.DeleteAsync(id));
        #endregion
        #region 更新部分列[comment give read]
        /// <summary>
        /// 更新部分列[comment give read]
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(SnArticle entity, string type) => Ok(await _service.UpdatePortionAsync(entity, type));
        #endregion

    }
}
