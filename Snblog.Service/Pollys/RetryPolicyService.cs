using Polly;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace Snblog.Service.pollys
{
    /// <summary>
    /// 错误重试服务类
    /// </summary>
    public class RetryPolicyService
    {
        /// <summary>
        /// 重复请求重试
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public  AsyncRetryPolicy<TResult> CreateRetryPolicy<TResult>()
        {
            return Policy<TResult>
                   .Handle<Exception>()
                   .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1), (exception, timeSpan, retryCount, _) =>
                   {
                       Console.WriteLine($"重试 {retryCount} 次，等待 {timeSpan.Seconds} 秒，原因: {exception.GetType().Name}");
                   });
        }
        
        /// <summary>
        /// 应用重试和超时策略
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public AsyncPolicyWrap<TResult> CreateRetryAndTimeoutPolicy<TResult>()
        {
            var retryPolicy = Policy<TResult>
                              .Handle<Exception>()
                              .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1), (exception, timeSpan, retryCount, _) =>
                              {
                                  Console.WriteLine($"重试 {retryCount} 次，等待 {timeSpan.Seconds} 秒，原因: {exception.GetType().Name}");
                              });

            // 创建超时策略，确保操作不会超过5秒
            var timeoutPolicy = Policy.TimeoutAsync<TResult>(5, TimeoutStrategy.Pessimistic);

            // 使用 WrapAsync 将重试策略和超时策略组合在一起
            return retryPolicy.WrapAsync(timeoutPolicy);
        }

    }
}