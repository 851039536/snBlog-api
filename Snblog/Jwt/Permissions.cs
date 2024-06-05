using Snblog.Jwt;

namespace Snblog.Util.GlobalVar;

/// <summary>
/// 权限变量配置
/// </summary>
public static class Permissions
{
    /// <summary>
    /// 需动态加载权限，待更改
    /// </summary>
    public const string Viewer = "ViewerPolicy";
    public const string Edit = "EditPolicy";
    public const string Delete = "DeletePolicy";
    public const string Create = "CreatePolicy";
    public const string Admin = "AdminPolicy";

    /// <summary>
    /// 定义策略名称和对应权限的字典,便于管理和修改。
    /// </summary>
    public static readonly Dictionary<string, string> Policies = new Dictionary<string, string>
    {
        { "EditPolicy", "edit" },
        { "ViewerPolicy", "view" },
        { "DeletePolicy", "delete" },
        { "CreatePolicy", "create" },
        { "AdminPolicy", "admin" }
    };

    /// <summary>
    /// 配置授权策略
    /// </summary>
    /// <param name="options"></param>
    public static void ConfigureAuthorizationPolicies(AuthorizationOptions options)
    {
        foreach (var policy in Policies)
        {
            options.AddPolicy(
                policy.Key,
                builder =>
                {
                    builder.Requirements.Add(new DynamicAuthorizationRequirement(policy.Value));
                }
            );
        }
    }
}
