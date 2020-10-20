using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
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
        /// 主键id查询
        /// </summary>
        /// <param name="SortId">主键id</param>
        /// <returns></returns>
        [HttpGet("AsyGetSortId")]
        public async Task<IActionResult> AsyGetSortId(int SortId)
        {
            return Ok(await _service.AsyGetSortId(SortId));
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
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsSort")]
        public async Task<ActionResult<SnSort>> AsyInsLabels(SnSort test)
        {
            return Ok(await _service.AsyInsSort(test));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("AysUpSort")]
        public async Task<IActionResult> AysUpSort(SnSort id)
        {
            var data = await _service.AysUpSort(id);
            return Ok(data);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetSort")]
        public async Task<IActionResult> AsyDetSort(int id)
        {
            return Ok(await _service.AsyDetSort(id));
        }

    }
}
