namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 查询总数 
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2  </param>
        /// <param name="type">条件(identity为0则填0) </param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<int> GetSumAsync(int identity, string type, bool cache);

        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类:1 || 标签:2</param>
        /// <param name="type">类别</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<NavigationDto>> GetTypeAsync(int identity, string type, bool cache);
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<NavigationDto> GetByIdAsync(int id, bool cache);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
        /// <param name="type">类别参数, identity 0 可不填</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <param name="cache">是否开启缓存</param>
        /// <param name="ordering">排序条件[data:时间 按id排序]</param>
        Task<List<NavigationDto>> GetPagingAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">匹配描述，标题，URL:0 || 分类:1 || 用户:2</param>
        /// <param name="type">查询条件:用户||分类</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<NavigationDto>> GetContainsAsync(int identity,string type , string name , bool cache );
        Task<bool> RandomImg(int minValue, int maxValue);
        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(int id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(Navigation entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Navigation entity);
    }
}
