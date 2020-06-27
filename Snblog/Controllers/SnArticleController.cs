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
    public class SnArticleController : ControllerBase
    {
          private readonly ISnArticleService _service; //IOC依赖注入
         public SnArticleController(ISnArticleService service)
         {
             _service=service;
         }

         /// <summary>
         /// 查询总数
         /// </summary>
         /// <returns></returns>
         [HttpGet("GetArticleCount")]
        public IActionResult GetArticleCount()
        {
            return Ok( _service.GetArticleCount());
        }

        /// <summary>
        /// 同步查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTest")]
        public IActionResult GetTest()
        {
          return Ok(_service.GetTest());
         }

        /// <summary>
        /// id条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [HttpGet("AsyGetTestID")]
        public async Task<IActionResult> AsyGetTestId(int id)
        {
          return Ok(await _service.AsyGetTestName(id));
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsArticle")]
        public async Task<ActionResult<SnArticle>> AsyInsArticle(SnArticle test)
        {
            return Ok(await _service.AsyInsArticle(test));
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetArticleId")]
        public async Task<IActionResult> AsyDetArticleId(int id)
        {
          return Ok(await _service.AsyDetArticleId(id));
        }

        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut("AysUpArticle")]
        public async Task<IActionResult> AysUpArticle(SnArticle test)
        {
           var data=await Task.Run(()=> _service.AysUpArticle(test));
           return Ok(data);
        }
    }
}
