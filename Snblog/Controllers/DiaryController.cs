using System.Threading.Tasks;
using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Util.components;
//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    /// <summary>
    ///日记
    /// </summary>
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("diary")]
    public class DiaryController : ControllerBase
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
            return Ok(await _service.GetSumAsync(identity, type, cache));
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
            return Ok(await _service.GetContainsAsync(identity, type, name, cache));
        }
        #endregion

        #region 主键查询 GetByIdAsync
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region 统计[字段/阅读/点赞]总数量 GetSumAsync

        /// <summary>
        /// 统计[字段/阅读/点赞]总数量
        /// </summary>
        /// <param name="type">text:内容字段数-read:阅读数量-give:点赞数量</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        [HttpGet("GetStrSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type, bool cache)
        {
            return Ok(await _service.GetSumAsync(type, cache));
        }
        #endregion

        #region 分页查询GetFyAsync
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
        /// <returns></returns>
        [HttpGet("GetPagingAsync")]
        public async Task<IActionResult> GetFyAsync(int identity = 0, string type = "null", int pageIndex = 1, int pageSize = 10, string ordering = "id", bool isDesc = true, bool cache = false)
        {
            return Ok(await _service.GetFyAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache));
        }
        #endregion

        #region 添加数据 
        /// <summary>
        /// 添加数据 
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<ActionResult<Diary>> AddAsync(Diary entity)
        {
            return Ok(await _service.AddAsync(entity));
        }
        #endregion
        #region 删除数据 
        /// <summary>
        /// 删除数据 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DelAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
        #region 更新数据
        /// <summary>
        /// 更新数据 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdateAsync")]
        [Authorize(Roles = Permissions.Name)]
        public async Task<IActionResult> UpdateAsync(Diary entity)
        {
            return Ok(await _service.UpdateAsync(entity));
        }
        #endregion
        #region 更新部分列[give read]
        /// <summary>
        /// 更新部分列[give read]
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="type">更新字段</param>
        /// <returns></returns>
        [HttpPut("UpdatePortionAsync")]
        public async Task<IActionResult> UpdatePortionAsync(Diary entity, string type)
        {
            return Ok(await _service.UpdatePortionAsync(entity, type));
        }
        #endregion

    }
}
