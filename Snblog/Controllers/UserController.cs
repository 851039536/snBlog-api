using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Snblog.Jwt;
using Microsoft.AspNetCore.Http;
using Snblog.Util.GlobalVar;

namespace Snblog.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly snblogContext _coreDbContext;
        private readonly IUserService _service; //IOC依赖注入
        private readonly JwtConfig _jwtModel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="coreDbContext"></param>
        /// <param name="jwtModel"></param>
        public UserController(IUserService service,snblogContext coreDbContext,IOptions<JwtConfig> jwtModel)
        {
            _service = service;
            _coreDbContext = coreDbContext;
            this._jwtModel = jwtModel.Value;
        }

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <returns>Nickname,token,id,name</returns>
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Login(string user,string pwd)
        {
            // 如果用户名和密码为空，则返回错误信息
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd)) return Ok("用户密码不能为空");
            // 查询用户信息
            var res = _coreDbContext.Users.FirstOrDefault(u => u.Name == user && u.Pwd == pwd);
            if (res == null) return BadRequest("用户或密码错误");

            // 生成token
            string token = GenerateToken(res);
            // 返回用户信息和token
            return Ok(new { res.Nickname,Token = token,res.Id,res.Name });
        }

        private string GenerateToken(User user)
        {
            // 创建声明列表
            var claims = new List<Claim>();
            // 添加声明
            claims.AddRange(new[]
            {
              new Claim("UserName", user.Name),
              new Claim(ClaimTypes.Role,user.Name),
               new Claim(JwtRegisteredClaimNames.Sub,user.Name),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
              });
            // 获取当前时间
            DateTime now = DateTime.UtcNow;
            //生成token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtModel.Issuer, //签发者
                audience: _jwtModel.Audience, //生成token
                claims: claims, //jwt令牌数据体
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_jwtModel.Expiration)),//令牌过期时间
                                                                            //为数字签名定义SecurityKey                                    
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtModel.SecurityKey)),SecurityAlgorithms.HmacSha256)
            );
            // 将token转换为字符串
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }


        /// <summary>
        /// 登录2
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <returns>token</returns>
        [HttpPost("login2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Login2(string user,string pwd)
        {
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd)) return Ok("用户密码不能为空");
            // 查询用户信息
            var res = _coreDbContext.Users.FirstOrDefault(u => u.Name == user && u.Pwd == pwd);
            if (res == null) return BadRequest("用户或密码错误");

            // 生成token
            string token = GenerateToken(res);
            res.Ip = token;
            return Ok(res);
        }
        #endregion

        #region 主键查询
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        [HttpGet("bid")]
        public async Task<IActionResult> GetByIdAsync(int id = 0,bool cache = false)
        {
            var data = await _service.GetByIdAsync(id,cache);
            return ApiResponse(cache:cache,data:data);
        }
        #endregion

        #region 模糊查询
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        [HttpGet("contains")]
        public async Task<IActionResult> GetContainsAsync(string name = "c",bool cache = false)
        {
            var data = await _service.GetContainsAsync(name,cache);
            return ApiResponse(cache:cache,data:data);
        }
        #endregion

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        [HttpGet("sum")]
        public async Task<IActionResult> GetSumAsync(bool cache = false)
        {
            var data = await _service.GetSumAsync(cache);
            return ApiResponse(cache:cache,data:data);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">记录条数</param>
        [HttpGet("paging")]
        public IActionResult GetPagingAsync(int pageIndex = 1,int pageSize = 10)
        {
            var data = _service.GetPagingAsync(pageIndex,pageSize);
            return ApiResponse(data:data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize(Roles = Permissions.Name)]
        [HttpPost("add")]
        public async Task<IActionResult> Add(User entity)
        {
            entity.TimeCreate = DateTime.Now;
            entity.TimeModified = DateTime.Now;

            var data = await _service.AddAsync(entity);
            return ApiResponse(data:data);
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
            var data = await _service.DelAsync(id);
            return ApiResponse(data:data);
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
            user.TimeModified = DateTime.Now;
            var data = await _service.UpdateAsync(user);
            return ApiResponse(data:data);
        }

    }
}
