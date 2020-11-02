using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnNavigationService
    {

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        int GetNavigationCount();

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetNavigationCount(string type);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<SnNavigation> GetSnNavigation();

        Task<List<SnNavigation>> AsyGetWhereTest(string type, bool fag);
      

        SnNavigation GetNavigationId(int id);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnNavigation> GetPagingWhere(string type, int pageIndex, int pageSize, out int count, bool isDesc);

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        List<SnNavigation> GetDistTest(string type);

      


        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDelNavigation(int id);
    


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnNavigation> AsyIntNavigation(SnNavigation test);


        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<string> AysUpNavigation(SnNavigation test);

      
    }
}
