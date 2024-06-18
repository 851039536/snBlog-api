using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Snblog;

/// <summary>
/// 应用程序入口点
/// </summary>
public class Program
{
    /// <summary>
    /// 应用程序主入口点
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        // 读取 appsettings 配置
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        // 配置Serilog日志记录器
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        try
        {
            Log.Information("启动虚拟主机");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "虚拟主机意外终止...");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    ///创建并配置Web主机构建器
    /// </summary>
    /// <param name="args">命令行参数</param>
    /// <returns>Web主机构建器</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() // 使用 Serilog 作为日志提供程序
            .ConfigureWebHostDefaults(webBuilder =>
            {
                _ = webBuilder.UseStartup<Startup>();
            });
}
