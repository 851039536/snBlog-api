using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnSortService
    {

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<SnSort> GetSort();

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnSort>> AsyGetSort();

        /// <summary>
        /// 主键id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnSort>> AsyGetSortId(int id);


          /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnSort> GetPagingWhere( int pageIndex, int pageSize, out int count, bool isDesc);

        /// <summary>
        /// 查询用户总数
        /// </summary>
        /// <returns></returns>
        int GetSortCount();


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnSort> AsyInsSort(SnSort test);

        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<string> AysUpSort(SnSort test);

        /// <summary>
        /// 异步按id删除
        /// </summary>
        Task<string> AsyDetSort(int id);
    }
}
