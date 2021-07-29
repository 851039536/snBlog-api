using Microsoft.AspNetCore.Mvc;
using Snblog.Service.AngleSharp;
using System.Threading.Tasks;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.ControllersAngleSharp
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "AngleSharp")] //版本控制
    [ApiController]
    public class AngleSharpController : ControllerBase
    {
        private readonly HotNewsAngleSharp _angle; //IOC依赖注入
        #region 构造函数
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
        public async Task<IActionResult> GiteeItem() => Ok(await _angle.GiteeItem());
    }
}
