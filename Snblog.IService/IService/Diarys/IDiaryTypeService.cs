namespace Snblog.IService.IService.Diarys;

/// <summary>
/// 日记分类接口
/// </summary>
public interface IDiaryTypeService
{
    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    Task<List<DiaryType>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache);
        
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<DiaryType> GetByIdAsync(int id, bool cache);

    /// <summary>
    /// 类别查询
    /// </summary>
    /// <param name="type">分类</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<DiaryType> GetTypeAsync(int type, bool cache);


    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<int> GetSumAsync(bool cache);

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> AddAsync(DiaryType entity);

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
    Task<bool>UpdateAsync(DiaryType entity);


}