using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Snblog.Jwt;

/// <summary>
/// AuthConfigure
/// </summary>
public static class AuthConfigure
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //??如果该值为null，则该运算符将返回一个空字符串
        if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"] ?? string.Empty))
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.Audience = configuration["Authentication:JwtBearer:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证IssuerSigningKey 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            configuration["Authentication:JwtBearer:SecurityKey"] ?? string.Empty
                        )),

                    // //是否验证Issuer
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                    // 是否验证 Audience 
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                    // //是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}