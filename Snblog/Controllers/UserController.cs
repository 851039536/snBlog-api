using System.Net.Http;
using Microsoft.AspNetCore.Http;
using NuGet.Common;
using Snblog.Jwt;
using Snblog.Util.GlobalVar;
using SnBlogCore.Jwt;

namespace Snblog.Controllers;

/// <summary>
/// 用户
/// </summary>
[ApiExplorerSettings(GroupName = "V1")]
[ApiController]
[Route("user")]
public class UserController : BaseController
{
    private readonly SnblogContext _coreDbContext;
    private readonly IUserService _service;
    private readonly JwtHelper _jwt;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service"></param>
    /// <param name="coreDbContext"></param>
    /// <param name="jwt"></param>
    public UserController(IUserService service, SnblogContext coreDbContext, JwtHelper jwt, HttpClient httpClient)
    {
        _service = service;
        _coreDbContext = coreDbContext;
        _jwt = jwt;
        _httpClient = httpClient;
    }

    [Authorize(Policy = "EditPolicy")]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("已授予对这一动态资源的访问权限");
    }

    /// <summary>
    /// 验证Token
    /// </summary>
    /// <returns></returns>
    [HttpGet("checkToken")]
    public async Task<IActionResult> CheckToken()
    {
        string token = _jwt.CreateToken("1", "admin");

        _httpClient.SetBearerToken(token);
        // 发送请求
        var response = await _httpClient.GetAsync("http://localhost:5002/user/test");
        // 处理响应
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        else
        {
            return StatusCode((int)response.StatusCode);
        }
    }

    #region 登录

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userName">用户</param>
    /// <param name="pwd">密码</param>
    /// <returns>Nickname,token,id,name</returns>
    [HttpGet("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Login(string userName, string pwd)
    {
        // 如果用户名和密码为空，则返回错误信息
        if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(pwd))
            return Ok("用户密码不能为空");
        // 查询用户信息
        var ret = _coreDbContext.Users.FirstOrDefault(u => u.Name == userName && u.Pwd == pwd);
        if (ret == null)
            return BadRequest("用户或密码错误");

        string token = _jwt.CreateToken(userName, "admin");
        return Ok(
            new
            {
                ret.Nickname,
                Token = token,
                ret.Id,
                ret.Name
            }
        );
    }

    /// <summary>
    /// 登录2
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="pwd">密码</param>
    /// <returns>token</returns>
    [HttpPost("login2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Login2(string user, string pwd)
    {
        if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd))
            return Ok("用户密码不能为空");
        // 查询用户信息
        var ret = _coreDbContext.Users.FirstOrDefault(u => u.Name == user && u.Pwd == pwd);
        if (ret == null)
            return BadRequest("用户或密码错误");

        string token = _jwt.CreateToken(user, "admin");
        ret.Ip = token;
        return Ok(ret);
    }

    #endregion

    /// <summary>
    /// admin后台
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="pwd">密码</param>
    /// <returns>token</returns>
    [HttpPost("loginAdmin")]
    public IActionResult LoginAdmin(string user, string pwd)
    {
        if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pwd))
            return ApiResponse(400, false, 0, "false", "");
        // 查询用户信息
        var ret = _coreDbContext.Users.FirstOrDefault(u => u.Name == user && u.Pwd == pwd);
        if (ret == null)
            return ApiResponse(400, false, 0, "false", "");

        string token = _jwt.CreateToken(user, "admin");
        ret.Ip = token;
        return ApiResponse(data: ret);
    }

    #region 主键查询

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    [HttpGet("bid")]
    public async Task<IActionResult> GetByIdAsync(int id = 0, bool cache = false)
    {
        var data = await _service.GetByIdAsync(id, cache);
        return ApiResponse(cache: cache, data: data);
    }

    #endregion

    #region 模糊查询

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="name">查询字段</param>
    /// <param name="cache">缓存</param>
    [HttpGet("contains")]
    public async Task<IActionResult> GetContainsAsync(string name = "c", bool cache = false)
    {
        var data = await _service.GetContainsAsync(name, cache);
        return ApiResponse(cache: cache, data: data);
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
        int data = await _service.GetSumAsync(cache);
        return ApiResponse(cache: cache, data: data);
    }

    //TODO 查询失败
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">记录条数</param>
    [HttpGet("paging")]
    public IActionResult GetPagingAsync(int pageIndex = 1, int pageSize = 10)
    {
        var data = _service.GetPagingAsync(pageIndex, pageSize);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Roles = Permissionss.Name)]
    [HttpPost("add")]
    public async Task<IActionResult> Add(User entity)
    {
        entity.TimeCreate = DateTime.Now;
        entity.TimeModified = DateTime.Now;
        int data = await _service.AddAsync(entity);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
    [Authorize(Roles = Permissionss.Name)]
    [HttpDelete("del")]
    public async Task<IActionResult> Del(int id)
    {
        bool data = await _service.DelAsync(id);
        return ApiResponse(data: data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Authorize(Roles = Permissionss.Name)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(User user)
    {
        user.TimeModified = DateTime.Now;
        bool data = await _service.UpdateAsync(user);
        return ApiResponse(data: data);
    }
}
