namespace Snblog.IService.IService
{
    public interface IDiaryTypeService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<DiaryType>> GetAllAsync(bool cache);

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
        Task<int> CountAsync(bool cache);

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
}
