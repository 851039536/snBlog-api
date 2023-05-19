namespace Snblog.Controllers
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool Cache { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        public ApiResponse(int statusCode =200, bool cache=false,int total = 0,string message = "null",T data = default(T))
        {
            StatusCode = statusCode;
            Cache = cache;
            Total = total;
            Message = message;
            Data = data; 
        }
    }

}
