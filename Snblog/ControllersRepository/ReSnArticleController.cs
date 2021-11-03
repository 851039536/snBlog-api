using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.IService.IReService;

namespace Snblog.ControllersRepository
{
    /// <summary>
    /// ReSnArticleController
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V2")] //版本控制
    [ApiController]
    public class ReSnArticleController : ControllerBase
    {
        private readonly IReSnArticleService _service;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ReSnArticleController(IReSnArticleService service)
        {
            _service = service;
        }
        #endregion

        #region 查询总数 (缓存)
        /// <summary>
        /// 查询总数 (缓存)
        /// </summary>
        [HttpGet("AsyGestTest")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }
        #endregion
        #region 条件查询总数 (缓存)
        /// <summary>
        /// 条件查询总数 (缓存)
        /// </summary>
        /// <param name="type">分类id</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public IActionResult CountTypeAsync(int type)
        {
            return Ok(_service.CountAsync(type));
        }
        #endregion
        #region 查询所有 (缓存)
        /// <summary>
        /// 查询所有 (缓存)
        /// </summary>
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
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        #endregion
        #region  分类查询 (缓存)
        /// <summary>
        /// 分类查询 (缓存)
        /// </summary>
        /// <param name="id">分类id(label_id)</param>
        /// <returns></returns>
        [HttpGet("GetLabelAllAsync")]
        public async Task<IActionResult> GetLabelAllAsync(int id)
        {
            return Ok(await _service.GetLabelAllAsync(id));
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
        #region 分页查询 (条件 缓存)
        /// <summary>
        /// 分页查询 (条件 缓存)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetTypeFyTextAsync")]
        public async Task<IActionResult> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(await _service.GetTypeFyTextAsync(type, pageIndex, pageSize, isDesc));
        }
        #endregion
        #region 分页查询(条件排序)
        /// <summary>
        /// 分页查询(条件排序 缓存)
        /// </summary>
        /// <param name="type">查询条件[00查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="order">排序条件[data:时间,read:阅读,give:点赞,comment:评论]默认按id排序</param>
        /// <returns></returns>
        [HttpGet("GetFyTypeorderAsync")]
        public async Task<IActionResult> GetFyTypeorderAsync(int type, int pageIndex, int pageSize, string order, bool isDesc)
        {
            return Ok(await _service.GetFyTypeorderAsync(type, pageIndex, pageSize, order, isDesc));
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
        public async Task<IActionResult> UpdateAsync(SnArticle entity)
        {
            var data = await _service.UpdateAsync(entity);
            return Ok(data);
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
        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       //  [Authorize(Roles = "kai")] //角色授权
        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion

    }
}
