using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    public class SnSortController : ControllerBase
    {
        private readonly ISnSortService _service; //IOC依赖注入

        public SnSortController(ISnSortService service)
        {
            _service = service;
        }

        /// <summary>
        /// 查询分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSort")]
        public IActionResult GetSort()
        {
            return Ok(_service.GetSort());
        }

        /// <summary>
        /// 查询分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGetSort")]
        public async Task<IActionResult> AsyGetSort()
        {
            return Ok(await _service.AsyGetSort());
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="sortId">主键</param>
        /// <returns></returns>
        [HttpGet("AsyGetSortId")]
        public async Task<IActionResult> AsyGetSortId(int sortId)
        {
            return Ok(await _service.AsyGetSortId(sortId));
        }
        /// <summary>
        /// 分类总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSortCount")]
        public IActionResult GetSortCount()
        {
            return Ok(_service.GetSortCount());
        }

        /// <summary>
        /// 添加数据 （权限）
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsSort")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<ActionResult<SnSort>> AsyInsLabels(SnSort test)
        {
            return Ok(await _service.AsyInsSort(test));
        }

          /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetfyTest")]
        public IActionResult GetfyTest( int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingWhere( pageIndex, pageSize, out _, isDesc));
        }

        /// <summary>
        /// 更新数据 （权限）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("AysUpSort")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AysUpSort(SnSort id)
        {
            var data = await _service.AysUpSort(id);
            return Ok(data);
        }

        /// <summary>
        /// 删除数据 （权限）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetSort")]
        [Authorize(Roles = "kai")] //角色授权
        public async Task<IActionResult> AsyDetSort(int id)
        {
            return Ok(await _service.AsyDetSort(id));
        }

    }
}
