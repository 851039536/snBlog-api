using Snblog.Models;
using System.Collections.Generic;
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
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnLabels>> AsyGetLabels();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnLabels>> AsyGetLabelsId(int id);

        /// <summary>
        /// 查询标签总数
        /// </summary>
        /// <returns></returns>
        int GetLabelsCount();

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
