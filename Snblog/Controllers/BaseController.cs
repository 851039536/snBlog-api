namespace Snblog.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// 返回统一格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode">状态</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="message">提示消息</param>
        /// <param name="data">返回的数据</param>
        protected IActionResult ApiResponse<T>(int statusCode = 200,bool cache=false,string message = "请求成功",T data = default)
        {
            return new JsonResult(new ApiResponse<T>(statusCode,cache,message,data));
        }

    }

}
