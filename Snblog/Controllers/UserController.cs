using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService;
using Snblog.Jwt;
using Snblog.Repository.Repository;


//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers
{

    /// <summary>
    /// 用户
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]  Controller ControllerBase

    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly snblogContext _coreDbContext;
        private readonly IUserService _service; //IOC依赖注入
        private readonly JwtConfig jwtModel = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="coreDbContext"></param>
        /// <param name="_jwtModel"></param>
        public UserController(IUserService service, snblogContext coreDbContext, IOptions<JwtConfig> _jwtModel)
        {
            _service = service;
            _coreDbContext = coreDbContext;
            jwtModel = _jwtModel.Value;
        }

    

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <returns>string</returns>
        [HttpGet("login")]
        public IActionResult Login(string user, string pwd)
        {
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd)) return Ok("用户密码不能为空");
            var res = _coreDbContext.Users.Where(w => w.Name == user && w.Pwd == pwd).AsNoTracking().ToList();
            if (res.Count == 0) return Ok("用户或密码错误");
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim("UserName", res[0].Name),
                new Claim(ClaimTypes.Role,res[0].Name),
                new Claim(JwtRegisteredClaimNames.Sub,res[0].Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });
            DateTime now = DateTime.UtcNow;
            //生成token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtModel.Issuer, //签发者
                audience: jwtModel.Audience, //生成token
                claims: claims, //jwt令牌数据体
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(jwtModel.Expiration)),//令牌过期时间
                                                                            //为数字签名定义SecurityKey
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtModel.SecurityKey)), SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(res[0].Nickname + "," + token + "," + res[0].Id + "," + res[0].Name);
        }

        /// <summary>
        /// 登录2
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost("login2")]
        public IActionResult Login2(string user, string pwd)
        {
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd)) return Ok("用户密码不能为空");
            var res = _coreDbContext.Users.Where(w => w.Name == user && w.Pwd == pwd).AsNoTracking().ToList();
            if (res.Count == 0) return Ok("用户或密码错误");
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim("UserName", res[0].Name),
                new Claim(ClaimTypes.Role,res[0].Name),
                new Claim(JwtRegisteredClaimNames.Sub,res[0].Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });
            DateTime now = DateTime.UtcNow;
            var jwtSecurityToken = new JwtSecurityToken( //生成token
                issuer: jwtModel.Issuer, //签发者
                audience: jwtModel.Audience, //生成token
                claims: claims,  //jwt令牌数据体
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(jwtModel.Expiration)), //令牌过期时间
                                                                             //为数字签名定义SecurityKey
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtModel.SecurityKey)), SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            res[0].Ip = token;
            return Ok(res[0]);
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        [HttpGet("byid")]
        public async Task<IActionResult> GetByIdAsync(int id=0, bool cache=false)
        {
            return Ok(await _service.GetByIdAsync(id, cache));
        }


        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync( string name = "c", bool cache = false)
        {
            return Ok(await _service.GetContainsAsync(name, cache));
        }
        #endregion

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(bool cache=false)
        {
            return Ok(await _service.GetSumAsync(cache));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">记录条数</param>
        [HttpGet("paging")]
        public IActionResult GetPagingAsync(int pageIndex=1, int pageSize = 10)
        {
            return Ok(_service.GetPagingAsync(pageIndex, pageSize));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("add")]
        public async Task<ActionResult<User>> Add(User entity)
        {
            return Ok(await _service.AddAsync(entity));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpDelete("del")]
        public async Task<IActionResult> Del(int id)
        {
            return Ok(await _service.DelAsync(id));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UserDto user)
        {
            var data = await _service.UpdateAsync(user);
            return Ok(data);
        }

    }
}
