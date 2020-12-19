using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnVideoService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        List<SnVideo> GetTest();

        int GetVideoCount();
        /// <summary>
        /// 查询视频总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetVideoCount(int type);
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnVideo>> AsyGetTest();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnVideo>> AsyGetTestId(int id);

        /// <summary>
        /// where条件查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<SnVideo> GetTestWhere(int type);


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        List<SnVideo> GetPagingWhere(int type, int pageIndex, int pageSize, out int count, bool isDesc);

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int ConutType(int type);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> AsyDetVideo(int id);
        /// <summary>
        /// 按id删除
        /// </summary>
        string DetTestId(int id);


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<SnVideo> AsyInsVideo(SnVideo test);



        Task<string> AysUpVideo(SnVideo test);



    }
}
