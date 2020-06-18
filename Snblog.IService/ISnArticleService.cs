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
   public interface ISnArticleService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
         List<SnArticle> GetTest();

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
         Task<List<SnArticle>> AsyGetTest();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SnArticle> AsyGetTestName(int id);


        /// <summary>
        /// 按id删除
        /// </summary>
          Task<string> AsyDetTestId(int id);
        /// <summary>
        /// 按id删除
        /// </summary>
           string  DetTestId(int id);


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnArticle> AsyIntTest(SnArticle test);

          
        /// <summary>
        /// 同步添加数据
        /// </summary>
        /// <returns></returns>
         SnArticle IntTest(SnArticle test);

         Task<string> AysUpTest(SnArticle test);

          string UpTest(SnArticle test);
    }
}
