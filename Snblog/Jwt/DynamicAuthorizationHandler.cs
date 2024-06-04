using System.Security.Claims;

namespace Snblog.Jwt
{
    /// <summary>
    /// 授权处理程序，该处理程序将检查用户是否具有所需的权限。
    /// </summary>
    public class DynamicAuthorizationHandler : AuthorizationHandler<DynamicAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DynamicAuthorizationRequirement requirement
        )
        {
            // 假设你有一个方法来检查用户是否具有特定权限
            if (UserHasPermission(context.User, requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 检查用户是否具有特定的权限
        /// </summary>
        /// <param name="user">包含了用户的声明信息</param>
        /// <param name="permission"> 字符串类型的权限名称，例如 "EditPost" 或 "DeleteUser,或设置通用"</param>
        /// <returns></returns>
        private bool UserHasPermission(ClaimsPrincipal user, string permission)
        {
            // 输出用户的声明信息
            foreach (var claim in user.Claims)
            {
                // 输出声明的类型和值
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // 检查roleName，是否有管理员权限
            if (user.Claims.Any(c => c.Type == "roleName" && c.Value == "admin"))
            {
                // 如果用户的声明中包含指定的权限，返回 true
                return true;
            }
            return false; // 示例返回 true
        }
    }
}
