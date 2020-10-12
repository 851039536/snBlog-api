using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService
{
   public interface ISnLabelsService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
         List<SnLabels> GetLabels();


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnLabels> AsyInsLabels(SnLabels test);

        /// <summary>
        /// 异步更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
          Task<string> AysUpLabels(SnLabels test);

        /// <summary>
        /// 异步按id删除
        /// </summary>
          Task<string> AsyDetLabels(int id);
    }
}
