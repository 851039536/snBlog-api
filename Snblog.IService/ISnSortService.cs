using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
