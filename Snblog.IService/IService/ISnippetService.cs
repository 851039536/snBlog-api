namespace Snblog.IService.IService
{
    /// <summary>
    /// 代码片段接口
    /// </summary>
    public interface ISnippetService
        {

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户名:3|内容:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <returns>list-entity</returns>
        Task<List<SnippetDto>> GetContainsAsync(int identity,string type,string name,bool cache,int pageIndex,int pageSize);
        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        Task<int> GetStrSumAsync(int identity,string name,bool cache);
        /// <summary>
        /// 主键查询 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<SnippetDto> GetByIdAsync(int id,bool cache);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|用户名:3|子标签:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<SnippetDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,bool isDesc,bool cache);


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> AddAsync(Snippet entity);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(Snippet entity);

        /// <summary>
        /// 条件更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="type">更新字段: Read | Give | Comment</param>
        /// <returns>bool</returns>
        Task<bool> UpdatePortionAsync(Snippet entity,string type);


        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
        /// <param name="type">条件</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        Task<int> GetSumAsync(int identity,string type,bool cache);
        }
    }
