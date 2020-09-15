using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// 查询所有
        /// </summary>
        /// <returns></returns>
         List<SnNavigation> GetSnNavigation();

        Task<List<SnNavigation>> AsyGetWhereTest(string type ,bool fag);
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
         Task<List<SnNavigation>> AsyGetTest();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SnNavigation> AsyGetTestName(int id);


        /// <summary>
        /// 按id删除
        /// </summary>
          Task<string> AsyDelNavigation(int id);
        /// <summary>
        /// 按id删除
        /// </summary>
           string  DetTestId(int id);


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnNavigation> AsyIntNavigation(SnNavigation test);

          
        /// <summary>
        /// 同步添加数据
        /// </summary>
        /// <returns></returns>
         SnArticle IntTest(SnNavigation test);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
         Task<string> AysUpNavigation(SnNavigation test);

          string UpTest(SnNavigation test);
    }
}
