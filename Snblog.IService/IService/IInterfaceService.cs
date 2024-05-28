namespace Snblog.IService.IService;

/// <summary>
/// 业务类接口
/// </summary>
public interface IInterfaceService
{
    /// <summary>
    ///条件查询 
    /// </summary>
    /// <param name="identity">用户分类: 0 | 用户: 1 | 分类: 2</param>
    /// <param name="userName">用户名称</param>
    /// <param name="type">类别</param>
    /// <param name="cache">缓存</param>
    Task<List<InterfaceDto>> GetConditionAsync(int identity, string userName, string type, bool cache);

    //Task<List<InterfaceDto>> GetAllAsync(bool cache);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有: 0 | 分类: 1 | 用户名: 2 |  用户-分类: 3</param>
    /// <param name="type">类别参数, identity为0时可为空(null) 多条件以','分割</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">排序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    public Task<List<InterfaceDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize,  bool isDesc, bool cache);


    /// <summary>
    /// 添加
    /// </summary>
    /// <returns></returns>
    Task<bool> AddAsync(Interface entity);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Interface entity);

    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    Task<InterfaceDto> GetByIdAsync(int id, bool cache);
}