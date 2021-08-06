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

    }
}