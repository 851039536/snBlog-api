namespace Snblog.IService.Users;

public interface IUserService
{
    
    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns></returns>
    Task<int> GetSumAsync(bool cache);
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns></returns>
    Task<UserDto> GetByIdAsync(int id,bool cache);

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="name">查询字段</param>
    /// <param name="cache">缓存</param>
    Task<List<UserDto>> GetContainsAsync(string name,bool cache);


    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc"></param>
    /// <param name="cache"></param>
    Task<List<UserDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache);


    /// <summary>
    /// 添加
    /// </summary>
    Task<int> AddAsync(User test);

    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DelAsync(int userId);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(User user);
}