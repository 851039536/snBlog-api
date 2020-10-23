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
    public class SnLabelsController : ControllerBase
    {

        private readonly ISnLabelsService _service; //IOC依赖注入

        public SnLabelsController(ISnLabelsService service)
        {
            _service = service;
        }

        /// <summary>
        /// 查询标签
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLabels")]
        public IActionResult GetLabels()
        {
            return Ok(_service.GetLabels());
        }
        /// <summary>
        /// 查询标签
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGetLabels")]
        public async Task<IActionResult> AsyGetLabels()
        {
            return Ok(await _service.AsyGetLabels());
        }
        /// <summary>
        /// id查询标签
        /// </summary>
        /// <param name="LabelsId">主键</param>
        /// <returns></returns>
        [HttpGet("AsyGetLabelsId")]
        public async Task<IActionResult> AsyGetLabelsId(int LabelsId)
        {
            return Ok(await _service.AsyGetLabelsId(LabelsId));
        }

        /// <summary>
        /// 标签总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLabelsCount")]
        public IActionResult GetLabelsCount()
        {
            return Ok(_service.GetLabelsCount());
        }


        /// <summary>
        /// 添加标签
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsLabels")]
        public async Task<ActionResult<SnLabels>> AsyInsLabels(SnLabels test)
        {
            return Ok(await _service.AsyInsLabels(test));
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns></returns>
        [HttpPut("AysUpLabels")]
        public async Task<IActionResult> AysUpLabels(SnLabels id)
        {
            var data = await _service.AysUpLabels(id);
            return Ok(data);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签id</param>
        /// <returns></returns>
        [HttpDelete("AsyDetLabels")]
        public async Task<IActionResult> AsyDetLabels(int id)
        {
            return Ok(await _service.AsyDetLabels(id));
        }
    }
}
