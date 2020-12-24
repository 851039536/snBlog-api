using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snblog.IService;
using Snblog.Models;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnOneController : Controller
    {
        private readonly snblogContext _coreDbContext;
        private readonly ISnOneService _service; //IOC依赖注入
        public SnOneController(ISnOneService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.AsyGetOne());
        }

        /// <summary>
        /// 按id查询
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        [HttpGet("GetOneIdAsync")]
        public async Task<IActionResult> GetOneIdAsync(int id)
        {
            return Ok(await _service.AsyGetOneId(id));
        }


        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAsync")]
        public async Task<IActionResult> CountAsync()
        {
            return Ok(await _service.CountAsync());
        }

        /// <summary>
        /// 条件查总数
        /// </summary>
        /// <param name="type">分类</param>
        /// <returns></returns>
        [HttpGet("CountTypeAsync")]
        public async Task<IActionResult> CountTypeAsync(int type)
        {
            return Ok(await _service.CountTypeAsync(type));
        }
        /// <summary>
        /// 读取[字段/阅读/点赞]总数量
        /// </summary>
        /// <param name="type">text:内容字段数-read:阅读数量-give:点赞数量</param>
        /// <returns></returns>
        [HttpGet("GetSumAsync")]
        public async Task<IActionResult> GetSumAsync(string type)
        {
            return Ok(await _service.GetSumAsync(type));
        }

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetPagingOne")]
        public IActionResult GetPagingOne(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingOne(pageIndex, pageSize, out _, isDesc));
        }

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type">查询条件[999查所有]-[排序条件查询所有才会生效,默认按id排序]</param>
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

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsOne")]
        public async Task<ActionResult<SnOne>> AsyInsOne(SnOne one)
        {
            return Ok(await _service.AsyInsOne(one));
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetOne")]
        public async Task<IActionResult> AsyDetOne(int id)
        {
            return Ok(await _service.AsyDetOne(id));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPut("AysUpOne")]
        public async Task<IActionResult> AysUpOne(SnOne one)
        {
            var data = await _service.AysUpOne(one);
            return Ok(data);
        }

    }
}
