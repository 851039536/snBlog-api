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



         int GetArticleCount();
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
        /// where条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
         List<SnArticle> GetTestWhere(int id);

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
          int ConutLabel(int type);
        /// <summary>
        /// 按id删除
        /// </summary>
          Task<string> AsyDetArticleId(int id);
        /// <summary>
        /// 按id删除
        /// </summary>
           string  DetTestId(int id);


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnArticle> AsyInsArticle(SnArticle test);

          
        /// <summary>
        /// 同步添加数据
        /// </summary>
        /// <returns></returns>
         SnArticle IntTest(SnArticle test);

         Task<string> AysUpArticle(SnArticle test);

          string UpTest(SnArticle test);
    }
}
