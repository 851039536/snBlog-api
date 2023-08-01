
namespace Snblog.IService.IService
{
    /// <summary>
    /// 用户说说接口
    /// </summary>
    public interface IUserTalkService
    {

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="identity">所有:0|用户:1</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="name">查询字段</param>
        /// <param name="cache">缓存</param>
        Task<List<UserTalkDto>> GetContainsAsync(int identity, string type, string name, bool cache);
        
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|用户:1</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|id:主键</param>
        /// <returns>list-entity</returns>
        Task<List<UserTalkDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache);
     
        
        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DelAsync(int id);

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        Task<bool> AddAsync(UserTalk entity);
     
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserTalk test);

    }
}
