namespace Snblog.IService.IService.Navigations;

public interface INavigationTypeService
{
    
    /// <summary>
    ///  查询总数
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<int> GetSumAsync(bool cache);
    
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<NavigationType>> GetAllAsync(bool cache);

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<NavigationType>GetByIdAsync(int id,bool cache);

    /// <summary>
    /// 条件分页查询
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="isDesc"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    Task<List<NavigationType>> GetPagingAsync(string type, int pageIndex, int pageSize, bool isDesc,bool cache);

   

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddAsync(NavigationType entity);

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <returns></returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(NavigationType entity);
}