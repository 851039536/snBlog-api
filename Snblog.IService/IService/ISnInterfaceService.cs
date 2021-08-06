using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Models;

namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnInterfaceService
    {
        Task<List<SnInterfaceDto>> GetTypeAsync(int userId, int type, bool cache);

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