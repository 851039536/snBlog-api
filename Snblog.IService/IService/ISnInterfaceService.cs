using Snblog.Enties.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnInterfaceService
    {
        /// <summary>
        ///条件查询 
        /// </summary>
        /// <param name="identity">分类和用户:0 || 用户:1 || 分类:2</param>
        /// <param name="users">条件:用户</param>
        /// <param name="type">条件:类别</param>
        /// <param name="cache">是否开启缓存</param>
        Task<List<SnInterfaceDto>> GetTypeAsync(int identity, int users, int type, bool cache);

        Task<List<SnInterfaceDto>> GetAllAsync(bool cache);

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<List<SnInterfaceDto>> GetTypefyAsync(int userId,int type, int pageIndex, int pageSize, bool isDesc, bool cache);
    }
}