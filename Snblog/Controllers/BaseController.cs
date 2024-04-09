namespace Snblog.Controllers;

/// <summary>
/// 重载路由统一返回格式
/// </summary>
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// 返回统一格式
    /// </summary>
    /// <typeparam name="T">响应数据的类型</typeparam>
    /// <param name="statusCode">HTTP状态码，表示响应的状态</param>
    /// <param name="cache">指示响应是否可缓存</param>
    /// <param name="total">数据项的总数（如果适用）</param>
    /// <param name="message">返回给客户端的消息</param>
    /// <param name="data">返回给客户端的数据</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>  
    protected static IActionResult ApiResponse<T>(int statusCode = 200,bool cache = false,int total = 0,string message = "请求成功",T data = default)
    {
        return new JsonResult(new ApiResponse<T>(statusCode,cache,total,message,data));
    }

}