using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snblog.IService;
using Snblog.Models;



//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class SnUserController : Controller
    {

        private readonly snblogContext _coreDbContext;
        private readonly ISnUserService _service; //IOC依赖注入
        private readonly DbSet<SnUser> user;

        public SnUserController(ISnUserService service, snblogContext coreDbContext)
        {
            _service = service;
            _coreDbContext = coreDbContext;
            user = coreDbContext.SnUser;
        }


        [HttpGet("Login")]
        public IActionResult Login(string users, string pwd)
        {
            if (string.IsNullOrEmpty(users) && string.IsNullOrEmpty(pwd))
            {
                return Ok("用户密码不能为空");
            }
            var data = from u in user
                       where u.UserName == users && u.UserPwd == pwd
                       select u.UserName;
            if (data.Count() == 0)
            {
                return Ok("登录失败");
            }
            else
            {
                return Ok("登录成功");
            }

        }

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("AsyGestTest")]
        public async Task<IActionResult> AsyGetUser()
        {
            return Ok(await _service.AsyGetUser());
        }

        /// <summary>
        /// 主键id查询
        /// </summary>
        /// <param name="userId">主键id</param>
        /// <returns></returns>
        [HttpGet("AsyGetUserId")]
        public async Task<IActionResult> AsyGetUserId(int userId)
        {
            return Ok(await _service.AsyGetUserId(userId));
        }

        /// <summary>
        /// 用户总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserCount")]
        public IActionResult GetUserCount()
        {
            return Ok(_service.GetUserCount());
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        [HttpGet("GetPagingUser")]
        public IActionResult GetPagingUser(int pageIndex, int pageSize, bool isDesc)
        {
            return Ok(_service.GetPagingUser(1, pageIndex, pageSize, out _, isDesc));
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AsyInsArticle")]
        public async Task<ActionResult<SnUser>> AsyInsArticle(SnUser user)
        {
            return Ok(await _service.AsyInsUser(user));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("AsyDetUserId")]
        public async Task<IActionResult> AsyDetUserId(int userId)
        {
            return Ok(await _service.AsyDetUserId(userId));
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("AysUpUser")]
        public async Task<IActionResult> AysUpUser(SnUser user)
        {
            var data = await _service.AysUpUser(user);
            return Ok(data);
        }

    }
}
