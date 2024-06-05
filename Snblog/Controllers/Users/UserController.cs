using System.Net.Http;
using Snblog.IService.Users;
using Snblog.Jwt;
using Snblog.Util.GlobalVar;
using SnBlogCore.Jwt;

namespace Snblog.Controllers.Users;

/// <summary>
/// 用户API
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
    /// <param name="httpClient"></param>
    public UserController(IUserService service, SnblogContext coreDbContext, JwtHelper jwt, HttpClient httpClient)
    {
        _service = service;
        _coreDbContext = coreDbContext;
        _jwt = jwt;
        _httpClient = httpClient;
    }

    #region jwt权限测试

    /// <summary>
    /// 测试编辑权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Edit)]
    [HttpGet("edit")]
    public IActionResult Edit()
    {
        return Ok("可编辑权限");
    }

    /// <summary>
    /// 测试查看权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Viewer)]
    [HttpGet("viewer")]
    public IActionResult Viewer()
    {
        return Ok("可查看权限");
    }

    /// <summary>
    /// 测试删除权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Delete)]
    [HttpGet("delete")]
    public IActionResult Delete()
    {
        return Ok("可删除权限");
    }

    /// <summary>
    /// 测试创建权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Create)]
    [HttpGet("create")]
    public IActionResult Create()
    {
        return Ok("可创建权限");
    }

    /// <summary>
    /// 测试编辑和查看权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Viewer)]
    [Authorize(Policy = Permissions.Edit)]
    [HttpGet("viewerEdit")]
    public IActionResult ViewerEdit()
    {
        return Ok("可编辑和查看权限");
    }

    /// <summary>
    /// 测试管理员权限
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Viewer)]
    [Authorize(Policy = Permissions.Edit)]
    [Authorize(Policy = Permissions.Delete)]
    [Authorize(Policy = Permissions.Create)]
    [Authorize(Policy = Permissions.Admin)]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok("管理员权限");
    }

    /// <summary>
    /// 验证Token，测试角色权限是否能访问
    /// </summary>
    /// <param name="userName">账号</param>
    /// <param name="types">edit，viewer，viewerEdit，admin，delete，create</param>
    /// <returns></returns>
    [HttpGet("checkToken")]
    public async Task<IActionResult> CheckToken(string userName, string types)
    {
        var users = _coreDbContext
            .Users.Include(u => u.Roles)
            .ThenInclude(ur => ur.Permissions)
            .FirstOrDefault(u => u.Name == userName);

        if (users == null)
            return Forbid("用户或密码错误");

        string roleName = "";
        string roles = "";
        Console.WriteLine($"用户：{users.Name}");
        foreach (var role in users.Roles)
        {
            roles += "_" + role.Name;
            // 遍历角色的权限
            foreach (var permission in role.Permissions)
            {
                roleName += "_" + permission.Name;
            }
        }
        Console.WriteLine($"拥有角色: {roles}");
        Console.WriteLine($"此用户包含权限：{roleName}");
        //生成token
        string token = _jwt.CreateToken(userName, roleName);

        _httpClient.SetBearerToken(token);
        var response = await _httpClient.GetAsync($"http://localhost:5002/user/{types}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        return StatusCode((int)response.StatusCode);
    }

    #endregion

    #region 登录

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userName">用户</param>
    /// <param name="pwd">密码</param>
    /// <returns>Nickname,token,id,name</returns>
    [HttpGet("login")]
    [ProducesDefaultResponseType]
    public IActionResult Login(string userName, string pwd)
    {
        // 如果用户名和密码为空，则返回错误信息
        if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(pwd))
            return NotFound("用户密码不能为空");
        // 查询用户信息
        var ret = _coreDbContext
            .Users.Include(u => u.Roles)
            .ThenInclude(ur => ur.Permissions)
            .FirstOrDefault(u => u.Name == userName && u.Pwd == pwd);
        if (ret == null)
            return NotFound("用户或密码错误");

        string roleName = "";
        string roles = "";
        Console.WriteLine($"用户：{ret.Name}");
        foreach (var role in ret.Roles)
        {
            roles += "_" + role.Name;
            // 遍历角色的权限
            foreach (var permission in role.Permissions)
            {
                roleName += "_" + permission.Name;
            }
        }
        Console.WriteLine($"拥有角色: {roles}");
        Console.WriteLine($"此用户包含权限：{roleName}");

        string token = _jwt.CreateToken(userName, roleName);
        return ApiResponse(
            data: new
            {
                ret.Nickname,
                Token = token,
                ret.Id,
                ret.Name
            }
        );
    }

    #endregion

    #region 查询总数

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

    #endregion

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

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">记录条数</param>
    /// <param name="isDesc"></param>
    /// <param name="cache"></param>
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync(int pageIndex = 1, int pageSize = 10, bool isDesc = true, bool cache = false)
    {
        var data = await _service.GetPagingAsync(pageIndex, pageSize, isDesc, cache);
        return ApiResponse(data: data);
    }

    #endregion

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Authorize(Policy = Permissions.Admin)]
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
    [Authorize(Policy = Permissions.Admin)]
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
    [Authorize(Policy = Permissions.Admin)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(User user)
    {
        user.TimeModified = DateTime.Now;
        bool data = await _service.UpdateAsync(user);
        return ApiResponse(data: data);
    }
}
