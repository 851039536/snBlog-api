using System.Net.Http;
using System.Net.Http.Headers;

namespace Snblog.Jwt
{
    public static class HttpClientExtensions
    {
        public static void SetBearerToken(this HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
