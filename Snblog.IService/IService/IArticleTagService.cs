namespace Snblog.IService.IService
{
    /// <summary>
    /// 文章标签接口
    /// </summary>
    public interface IArticleTagService
        {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<ArticleTagDto> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<ArticleTagDto>> GetPagingAsync(int pageInde, int pageSize, bool isDesc, bool cache);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        Task<int> GetSumAsync(bool cache);
        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> AddAsync(ArticleTag entity);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ArticleTag test);
        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
