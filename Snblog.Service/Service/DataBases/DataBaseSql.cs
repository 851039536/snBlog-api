using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace Snblog.Service.Service.DataBases;

public class DataBaseSql
{
    private  readonly IConfiguration _configuration;

    public DataBaseSql(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// 执行数据库备份操作
    /// </summary>
    /// <returns>操作成功返回"true"，否则返回错误信息</returns>
    public  bool Backups()
    { 
        // 从配置中获取数据库连接字符串
        string sqlUrl = _configuration["ConnectionStrings:MysqlConnection"];
        Log.Warning("备份数据库连接字符串:"+sqlUrl);
        
        
        // 获取当前日期，格式化并替换分隔符
        string time = DateTime.Now.ToString("yyyyMMddHHmmss").Replace("/","-");
        // 获取当前工作目录
        string path = Directory.GetCurrentDirectory();
        // 构造备份文件路径
        string file = Path.Combine(path,"mysql",$"{time}_blog.sql");
        // 使用using语句确保资源被正确释放
        using MySqlConnection conn = new(sqlUrl);
        using MySqlCommand cmd = new();
        using MySqlBackup mb = new(cmd);
        // 设置命令的连接
        cmd.Connection = conn;
        try
        {
            // 打开数据库连接
            conn.Open();
            // 导出数据库备份到文件
            mb.ExportToFile(file);
            conn.Close();
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 执行数据库恢复操作
    /// </summary>
    /// <param name="ip">数据库服务器IP地址</param>
    /// <param name="user">数据库用户名</param>
    /// <param name="pwd">数据库密码</param>
    /// <param name="database">数据库名称</param>
    /// <returns>操作成功返回"true"，否则返回错误信息</returns>
    public  bool Restore(string ip,string user,string pwd,string database)
    {
        // 构造数据库连接字符串
        string sqlUrl = $"server={ip};User={user};pwd={pwd};database={database};";
        // 备份文件路径
        string file = Path.Combine(Directory.GetCurrentDirectory(),"mysql","blog.sql");

        // 使用using语句确保资源被正确释放
        using MySqlConnection conn = new(sqlUrl);
        using MySqlCommand cmd = new();
        using MySqlBackup mb = new(cmd);
        // 设置命令的连接
        cmd.Connection = conn;
        try
        {
            conn.Open();
            // 从文件导入数据库备份
            mb.ImportFromFile(file);
            conn.Close();
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }
    
    /// <summary>
    /// 执行数据库恢复操作，根据配置文件恢复
    /// </summary>
    /// <returns>操作成功返回"true"，否则返回错误信息</returns>
    public  bool Restore()
    {
        // 构造数据库连接字符串
        string sqlUrl = _configuration["ConnectionStrings:MysqlConnection"];
        Log.Warning("备份数据库连接字符串:"+sqlUrl);
        // 备份文件路径
        string file = Path.Combine(Directory.GetCurrentDirectory(),"mysql","blog.sql");
        // 使用using语句确保资源被正确释放
        using MySqlConnection conn = new(sqlUrl);
        using MySqlCommand cmd = new();
        using MySqlBackup mb = new(cmd);
        // 设置命令的连接
        cmd.Connection = conn;
        try
        {
            conn.Open();
            // 从文件导入数据库备份
            mb.ImportFromFile(file);
            conn.Close();
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }
}