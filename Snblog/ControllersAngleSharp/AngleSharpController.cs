using Microsoft.AspNetCore.Mvc;
using Snblog.Service.AngleSharp;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.ControllersAngleSharp
{

    /// <summary>
    /// AngleSharp
    /// </summary>
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "AngleSharp")] //版本控制
    [ApiController]
    public class AngleSharpController : ControllerBase
    {
        private readonly HotNewsAngleSharp _angle; //IOC依赖注入
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="angle"></param>
        public AngleSharpController(HotNewsAngleSharp angle)
        {
            _angle = angle;
        }
        #endregion
        /// <summary>
        /// 自定义爬取内容
        /// </summary>
        /// <param name="url">网站：https://www.cnblogs.com/</param>
        /// <param name="selector">selector：#post_list</param>
        /// <param name="selectorall">selectorall：div.post-item-text > a</param>
        /// <returns></returns>
        [HttpGet("GeneralCrawl")]
        public async Task<IActionResult> GeneralCrawl(string url, string selector, string selectorall) => Ok(await _angle.GeneralCrawl(url, selector, selectorall));
        /// <summary>
        /// 读取博客园最新内容（如选项为空读取默认值）此项为参考示例
        /// </summary>
        /// <param name="url">博客网站：https://www.cnblogs.com/</param>
        /// <param name="selector">selector：#post_list</param>
        /// <param name="selectorall">selectorall：div.post-item-text > a</param>
        /// <returns></returns>
        [HttpGet("Cnblogs")]
        public async Task<IActionResult> Cnblogs(string url, string selector, string selectorall)
        {
            return Ok(await _angle.Cnblogs(url, selector, selectorall));
        }

        /// <summary>
        /// 读取项目名称
        /// </summary>
        /// <returns></returns>
        [HttpGet("GiteeItem")]
        public async Task<IActionResult> GiteeItem()
        {
            return Ok(await _angle.GiteeItem());
        }
        [HttpGet("Daka")]
        public  async Task<IActionResult> Daka()
        {
            return Ok(await _angle.Daka());
        }
        /// <summary>
        ///  数据备份
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库</param>
        /// <returns></returns>
        [HttpGet("SqlBackups")]
        public  ActionResult SqlBackups(string ip= "localhost", string user= "root", string pwd= "woshishui", string database= "snblog")
        {
            return Ok( _angle.SqlBackups(ip,user,pwd,database));
        }

        /// <summary>
        /// 还原数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        [HttpGet("SqlRestore")]
        public ActionResult SqlRestore(string ip = "localhost", string user = "root", string pwd = "woshishui", string database = "snblog")
        {
            return Ok(_angle.SqlRestore(ip, user, pwd, database));
        }
    }
}
