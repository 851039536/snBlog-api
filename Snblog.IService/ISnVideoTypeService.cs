using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnVideoTypeService
    {
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnVideoType>> AsyGetTest();


    }
}
