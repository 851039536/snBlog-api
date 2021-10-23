using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService.IService;
using Snblog.Models;
using System;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    /// 文章内容
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    //[Authorize]
    public class SnArticleController : ControllerBase
    {
        private readonly ISnArticleService _service; //IOC依赖注入

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnArticleController(ISnArticleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion
        #region 查询总数
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync(bool cache)
        {
            return Ok(await _service.CountAsync(cache));
        }
        #endregion
        #region 条件查询总数
        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="type">分类id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTypeCountAsync")]
        public IActionResult GetTypeCountAsync(int type, bool cache)
        {
            return Ok(_service.GetTypeCountAsync(type, cache));
        }

        /// <summary>
        /// 查询分类总数 
        /// </summary>
        /// <param name="type">分类id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetConutSortAsync")]
        public async Task<IActionResult> GetConutSortAsync(int type, bool cache)
        {
            return Ok(await _service.GetConutSortAsync(type, cache));
        }
        #endregion
        #region 查询所有
        /// <summary>
        /// 查询所有 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        // [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion
        #region 模糊查询 Contains
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetContainsAsync")]
        public async Task<IActionResult> GetContainsAsync(string name, bool cache)
        {
            return Ok(await _service.GetContainsAsync(name, cache));
        }
        #endregion
        #region 条件模糊查询 Contains
        /// <summary>
        /// 条件模糊查询
        /// </summary>
        /// <param name="type">标签</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTypeContainsAsync")]
        public async Task<IActionResult> GetTypeContainsAsync(int type, string name, bool cache)
        {
            return Ok(await _service.GetTypeContainsAsync(type,name, cache));
        }
        #endregion
        #region 主键查询 
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion
        #region  分类ID查询 
        /// <summary>
        ///分类条件查询 
        /// </summary>
        /// <param name="sortId">分类id</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetTypeIdAsync")]
        public async Task<IActionResult> GetTypeIdAsync(int sortId, bool cache)
        {
            return Ok(await _service.GetTypeIdAsync(sortId, cache));
        }
        #endregion
        #region 读取[字段/阅读/点赞]总数量
        /// <summary>
        /// 统计[字段/阅读/点赞]总数量-缓存
        /// </summary>
        /// <param name="type">text-内容-read:阅读-give:点赞</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type, bool cache)
        {
            return Ok(await _service.GetSumAsync(type, cache));
        }

        #endregion
        #region 查询文章(无文章内容)
        /// <summary>
        /// 查询文章(无文章内容)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
      // [Authorize(Roles = Permissions.Name)]
        [HttpGet("GetFyTitleAsync")]
        public async Task<IActionResult> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetFyTitleAsync(pageIndex, pageSize, isDesc, cache));
        }
        #endregion
        #region  按标签分页查询  
        /// <summary>
        /// 按标签分页查询 
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetfyTestAsync")]
        public async Task<IActionResult> GetfyTestAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetfyTestAsync(type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion
        #region   按分类分页查询
        /// <summary>
        /// 按分类分页查询
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetfySortTestAsync")]
        public async Task<IActionResult> GetfySortTestAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            return Ok(await _service.GetfySortTestAsync(type, pageIndex, pageSize, isDesc, cache));
        }
        #endregion
        #region 分页查询(条件排序)
        /// <summary>
        /// 分页查询(条件排序)
        /// </summary>
        /// <param name="type">查询条件[00查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="name">排序条件[data:时间,read:阅读,give:点赞,comment:评论]默认按id排序</param>
        /// <returns></returns>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int type, int pageIndex, int pageSize, string name, bool isDesc, bool cache)
        {
            return Ok(await _service.GetFyAsync(type, pageIndex, pageSize, name, isDesc, cache));
        }
        #endregion
        #region 按标签条件查询
        /// <summary>
        /// 按标签条件查询
        /// </summary>
        /// <param name="labelId">标签id</param>
        /// <param name="isDesc"></param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetTagAsync")]
        public async Task<IActionResult> GetTagAsync(int labelId, bool isDesc, bool cache)
        {
            return Ok(await _service.GetTagAsync(labelId, isDesc, cache));
        }
        #endregion
        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(SnArticle entity)
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
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(SnArticle entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
        #region 更新部分列[comment give read]
        /// <summary>
        /// 更新部分列[comment give read]
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(SnArticle entity, string type)
        {
            return Ok(await _service.UpdatePortionAsync(entity, type));
        }
        #endregion

    }
}
