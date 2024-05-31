using Snblog.Service.Service.DataBases;

namespace Snblog.Controllers.DataBases;

/// <summary>
/// 数据库API
/// </summary>
[ApiExplorerSettings(GroupName = "mysql")]
[ApiController]
[Route("dataBase")]
public class DataBaseController : ControllerBase
{
    private readonly DataBaseSql _data;

    public DataBaseController(DataBaseSql data)
    {
        _data = data;
    }

    /// <summary>
    /// 执行数据库备份操作
    /// </summary>
    /// <returns>操作成功返回"true"，否则返回错误信息</returns>
    [HttpPost("backups")]
    public ActionResult Backups()
    {
        bool data = _data.Backups();
        return Ok(data);
    }

    /// <summary>
    /// 执行数据库恢复操作
    /// </summary>
    /// <param name="ip">数据库服务器IP地址</param>
    /// <param name="user">数据库用户名</param>
    /// <param name="pwd">数据库密码</param>
    /// <param name="database">数据库名称</param>
    /// <returns>操作成功返回"true"，否则返回错误信息</returns>
    [HttpPost("restore")]
    public ActionResult Restore(string ip = "localhost",string user = "root",string pwd = "woshishui",
                                string database = "snblog")
    {
        bool data = _data.Restore(ip,user,pwd,database);
        return Ok(data);
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