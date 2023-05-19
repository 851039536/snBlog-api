namespace Snblog.IService.IService
{
    public interface ISnPictureTypeService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
         Task<List<SnPictureType>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SnPictureType> GetByIdAsync(int id);

        Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();



        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnPictureType entity);

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
        Task<bool> UpdateAsync(SnPictureType entity);
    }
}
