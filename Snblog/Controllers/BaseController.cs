namespace Snblog.Controllers;

/// <summary>
/// 基础控制器类，提供统一格式的API响应方法。
/// </summary>
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// 返回统一格式的API响应。
    /// </summary>
    /// <typeparam name="T">响应数据的类型。</typeparam>
    /// <param name="statusCode">HTTP状态码，表示响应的状态。默认值为200。</param>
    /// <param name="cache">指示响应是否可缓存。默认值为false。</param>
    /// <param name="total">数据项的总数（如果适用）。默认值为0。</param>
    /// <param name="message">返回给客户端的消息。默认值为"successful"。</param>
    /// <param name="data">返回给客户端的数据。默认值为类型T的默认值。</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>
    protected static IActionResult ApiResponse<T>(int statusCode = 200,bool cache = false,int total = 0,string message = "successful",T data = default)
    {
        // 创建ApiResponse实例，封装所有参数
        // var apiResponse = new ApiResponse<T>(statusCode, cache, total, message, data);
        var apiResponse = new ApiResponse<T>
        {
            StatusCode = statusCode,
            Cache = cache,
            Total = total,
            Message = message,
            Data = data
        };

        // 使用JsonResult包装ApiResponse实例，并返回
        return new JsonResult(apiResponse);
    }

    /// <summary>
    /// 返回成功的统一格式的API响应。
    /// </summary>
    /// <typeparam name="T">响应数据的类型。</typeparam>
    /// <param name="data">返回给客户端的数据。默认值为类型T的默认值。</param>
    /// <param name="cache">指示响应是否可缓存。默认值为false。</param>
    /// <param name="message">返回给客户端的消息。默认值为"successful"。</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>
    protected IActionResult ApiResponseSuccess<T>(T data = default,bool cache = false, string message = "successful")
    {
        return ApiResponse<T>(200, cache, 0, message, data);
    }

    /// <summary>
    /// 返回失败的统一格式的API响应。
    /// </summary>
    /// <typeparam name="T">响应数据的类型。</typeparam>
    /// <param name="statusCode">HTTP状态码，表示响应的状态。默认值为400。</param>
    /// <param name="message">返回给客户端的消息。默认值为"failed"。</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>
    protected IActionResult ApiResponseFailure<T>(int statusCode = 400, string message = "failed")
    {
        return ApiResponse<T>(statusCode, false, 0, message, default);
    }

    /// <summary>
    /// 返回未找到的统一格式的API响应。
    /// </summary>
    /// <typeparam name="T">响应数据的类型。</typeparam>
    /// <param name="message">返回给客户端的消息。默认值为"Resource not found"。</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>
    protected IActionResult ApiResponseNotFound<T>(string message = "Resource not found")
    {
        return ApiResponse<T>(404, false, 0, message, default);
    }

    /// <summary>
    /// 返回内部服务器错误的统一格式的API响应。
    /// </summary>
    /// <typeparam name="T">响应数据的类型。</typeparam>
    /// <param name="message">返回给客户端的消息。默认值为"Internal server error"。</param>
    /// <returns>一个封装了响应信息的JsonResult对象。</returns>
    protected IActionResult ApiResponseInternalServerError<T>(string message = "Internal server error")
    {
        return ApiResponse<T>(500, false, 0, message, default);
    }
}