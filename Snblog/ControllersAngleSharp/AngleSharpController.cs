using Snblog.Service.AngleSharp;
using Snblog.Util.GlobalVar;

namespace Snblog.ControllersAngleSharp
{
    /// <summary>
    /// AngleSharpController
    /// </summary>
    [ApiExplorerSettings(GroupName = "AngleSharp")]
    [ApiController]
    [Route("angleSharp")]
    public class AngleSharpController : ControllerBase
    {
        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="path">备份路径默认null</param>
        /// <returns></returns>
        [HttpPost("SqlBackups")]
        public ActionResult SqlBackups(string path = "null")
        {
            return Ok(HotNewsAngleSharp.SqlBackups(path));
        }

        /// <summary>
        /// 还原数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        [HttpPost("SqlRestore")]
        public ActionResult SqlRestore(string ip = "localhost", string user = "root", string pwd = "woshishui",
            string database = "snblog")
        {
            return Ok(HotNewsAngleSharp.SqlRestore(ip, user, pwd, database));
        }

        /// <summary>
        /// 测试TOKEN是否存在
        /// </summary>
        /// <returns></returns>
        [HttpGet("TOKEN")]
        [Authorize(Roles = Permissions.Name)]
        public ActionResult TOKEN()
        {
            return Ok();
        }
    }
}