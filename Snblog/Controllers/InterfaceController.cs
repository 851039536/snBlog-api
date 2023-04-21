using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using System;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [ApiExplorerSettings(GroupName = "V1")] 
    [ApiController]
    [Route("Interface")]
    public class InterfaceController : ControllerBase
    {    
        //IOC依赖注入
        private readonly IInterfaceService _service; 
        #region 构造函数
        public InterfaceController(IInterfaceService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region 主键查询 
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">启缓存</param>
        /// <returns></returns>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id, bool cache = false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }
        #endregion

        #region  条件查询
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">用户-分类: 0 | 用户: 1 | 分类: 2</param>
        /// <param name="userName">用户名称</param>
        /// <param name="type">类别</param>
        /// <param name="cache">缓存</param>
        [HttpGet("condition")]
        public async Task<IActionResult> GetConditionAsync(int identity=0, string userName = "null", string type= "null", bool cache=false)
        {
            return Ok(await _service.GetConditionAsync(identity,userName, type, cache));
        }
        #endregion

        #region  查询所有
        //[HttpGet("GetAllAsync")]
        //public async Task<IActionResult> GetAllAsync(bool cache=false)
        //{
        //    return Ok(await _service.GetAllAsync(cache));
        //}
        #endregion

        #region 分页查询 
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有: 0 | 分类: 1 | 用户名: 2 |  用户-分类: 3</param>
        /// <param name="type">类别参数, identity为0时可为空(null) 多条件以','分割</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序条件[按id排序]</param>
        /// <returns>list-entity</returns>
        [HttpGet("paging")]
        public async Task<IActionResult> GetPagingAsync(int identity=0, string type="null", int pageIndex=1, int pageSize=10, string ordering="id", bool isDesc=true, bool cache=false)
        {
            return Ok(await _service.GetPagingAsync(identity, type, pageIndex, pageSize, ordering, isDesc, cache));
        }
        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Interface entity)
        {
            return Ok(await _service.AddAsync(entity));
        }

        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateAsync(Interface entity)
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
        [HttpDelete("del")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
        #endregion
    }

}
