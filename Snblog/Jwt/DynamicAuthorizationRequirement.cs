namespace Snblog.Jwt
{
    /// <summary>
    /// 定义动态授权策略
    /// </summary>
    public class DynamicAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public DynamicAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
