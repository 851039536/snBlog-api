namespace Snblog.IService.IReService
{
    public interface IReSnNavigationService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnNavigation>> GetAllAsync();
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SnNavigation> GetByIdAsync(int id);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<int> CountTypeAsync(string type);
        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        Task<List<SnNavigation>> GetDistinct(string type);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <returns>List</returns>
        Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnNavigation> AddAsync(SnNavigation entity);
        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnNavigation entity);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
