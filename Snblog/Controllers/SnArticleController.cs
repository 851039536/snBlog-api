using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IService;
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

        #region 构造函数SnArticleController
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SnArticleController(ISnArticleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region 查询总数 GetCountAsync
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 标签:2 || 用户3  </param>
        /// <param name="type">查询条件 </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        [HttpGet("GetCountAsync")]
        public async Task<IActionResult> GetCountAsync(int identity = 0, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetCountAsync(identity, type, cache));
        }
        #endregion

        #region 查询所有GetAllAsync
        /// <summary>
        /// 查询所有 
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        // [ApiExplorerSettings(IgnoreApi = true)] //隐藏接口 或者直接对这个方法 private，也可以直接使用obsolete属性
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync(bool cache = false)
        {
            return Ok(await _service.GetAllAsync(cache));
        }
        #endregion

        #region 模糊查询 Contains
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 标签:2 || 用户:3 || 标签,用户:4</param>
        /// <param name="type">查询参数-(多条件,分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        [HttpGet("GetContainsAsync")]
        public async Task<IActionResult> GetContainsAsync(int identity = 0, string type = "null", string name = "c", bool cache = false)
        {
            return Ok(await _service.GetContainsAsync(identity, type, name, cache));
        }
        #endregion

        #region 主键查询 GetByIdAsync
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region  条件查询 GetTypeAsync
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 标签:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetTypeAsync")]
        public async Task<IActionResult> GetTypeAsync(int identity = 1, string type = "null", bool cache = false)
        {
            return Ok(await _service.GetTypeAsync(identity, type, cache));
        }
        #endregion

        #region 读取字段长度 GetSumAsync
        /// <summary>
        /// 读取字段长度 GetSumAsync
        /// </summary>
        /// <param name="identity">0:所有: 分类:1 || 标签:2 || 用户:3</param>
        /// <param name="type">1-内容-2:阅读-3:点赞</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">是否开启缓存</param>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(int identity = 0, int type = 1, string name = "null", bool cache = false)
        {
            return Ok(await _service.GetSumAsync(identity, type, name, cache));
        }

        #endregion

        #region 分页查询GetFyAsync
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 标签:2 || 用户:3 || 标签,用户: 4</param>
        /// <param name="type">查询参数-(多条件,分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序[data:时间 read:阅读 give:点赞 id:主键]</param>
        [HttpGet("GetFyAsync")]
        public async Task<IActionResult> GetFyAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10, string ordering = "id", bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetFyAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache));
        }
        #endregion

        #region 添加数据AddAsync
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

        #region 更新数据UpdateAsync
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

        #region 删除数据DeleteAsync
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

        #region 更新部分列[comment give read] UpdatePortionAsync
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
