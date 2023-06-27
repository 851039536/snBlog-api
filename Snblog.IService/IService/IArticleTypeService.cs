namespace Snblog.IService.IService
{
    /// <summary>
    /// 文章分类接口
    /// </summary>
    public interface IArticleTypeService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<ArticleTypeDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        Task<ArticleTypeDto> GetByIdAsync(int id, bool cache);


        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        Task<List<ArticleTypeDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache);

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
        Task<bool> AddAsync(ArticleType entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ArticleType test);

        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}