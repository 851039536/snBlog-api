namespace Snblog.IService.IService;

public interface ISnippetTypeSubService
{
    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    Task<List<SnippetTypeSubDto>> GetAllAsync(bool cache);

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">缓存</param>
    /// <returns>entity</returns>
    Task<SnippetTypeSubDto> GetByIdAsync(int id,bool cache);

    /// <summary>
    /// 分页查询 
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="cache">缓存</param>
    /// <returns>list-entity</returns>
    Task<List<SnippetTypeSubDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache);

    /// <summary>
    /// 查询总数
    /// </summary>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    Task<int> GetSumAsync(bool cache);

    /// <summary>
    /// 根据主表类别id查询
    /// </summary>
    /// <param name="snippetTypeId"></param>
    /// <returns></returns>
    Task<List<SnippetTypeSubDto>> GetCondition(int snippetTypeId ,bool cache);
    /// <summary>
    ///  添加 
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    Task<bool> AddAsync(SnippetTypeSub entity);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>bool</returns>
    Task<bool> UpdateAsync(SnippetTypeSub entity);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    Task<bool> DeleteAsync(int id);
}