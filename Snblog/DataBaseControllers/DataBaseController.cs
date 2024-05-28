using Snblog.Service.AngleSharp;
using Snblog.Util.GlobalVar;

namespace Snblog.DataBaseControllers;

/// <summary>
/// 数据库操作
/// </summary>
[ApiExplorerSettings(GroupName = "Sql")]
[ApiController]
[Route("dataBase")]
public class DataBaseController : ControllerBase
{
    /// <summary>
    /// 数据备份
    /// </summary>
    /// <param name="path">备份路径默认null</param>
    /// <returns></returns>
    [HttpPost("SqlBackups")]
    public ActionResult SqlBackups()
    {
        return Ok(DataBaseSql.SqlBackups());
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
        return Ok(DataBaseSql.SqlRestore(ip, user, pwd, database));
    }

    /// <summary>
    /// 测试TOKEN是否存在
    /// </summary>
    /// <returns></returns>
    [HttpGet("token")]
    [Authorize(Roles = "kai,1")]
    public ActionResult CheckToken()
    {
        return Ok();
    }
}