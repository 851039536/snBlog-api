using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SnBlogCore.Jwt;

/// <summary>
/// 生成 JWT 的 Token
/// </summary>
public class JwtHelper
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数，用于注入配置信息
    /// </summary>
    /// <param name="configuration"></param>
    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// 根据用户名称,角色生成token
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public string CreateToken(string userName, string roleName)
    {
        // 1. 定义需要使用到的Claims
        // Claims是JWT中的声明，用于存储用户信息和权限等
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName), //用户名
            new Claim(ClaimTypes.Role, roleName), //用户角色 //HttpContext.User.IsInRole("r_admin")
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT ID，防止重放攻击
            new Claim("userName", userName), //自定义Claim
            new Claim("roleName", roleName), //自定义Claim
            //new Claim("Name", "超级管理员") //自定义Claim，用于存储用户昵称或角色名
        };

        // 2. 从 appsettings.json 中读取SecretKey
        // SecretKey用于生成数字签名，保证JWT的安全性
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        // 3. 选择加密算法
        string algorithm = SecurityAlgorithms.HmacSha256;

        // 4. 生成Credentials
        var signingCredentials = new SigningCredentials(secretKey, algorithm);
        // 获取当前时间
        var now = DateTime.UtcNow;
        string expiration = _configuration["Jwt:Expiration"];
        // 5. 根据以上，生成token
        // 创建JwtSecurityToken对象，设置签发者、受众、声明、生效时间和过期时间等
        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"], //签发者
            _configuration["Jwt:Audience"], //生成token
            claims, //jwt令牌数据体
            DateTime.Now, //生效时间
            now.Add(TimeSpan.FromMinutes(int.Parse(expiration))), ////令牌过期时间
            signingCredentials //签名凭证
        );

        // 6. 将token变为string
        // 使用JwtSecurityTokenHandler将JwtSecurityToken对象转换为字符串
        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}
