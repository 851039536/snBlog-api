namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnUserTalkService
    {
        

 
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <returns></returns>
        Task<List<SnUserTalk>> GetAll();


        



        


        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DelAsync(int id);
      


        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AsyInsUserTalk(SnUserTalk entity);


     
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        Task<bool> AysUpUserTalk(SnUserTalk entity);

    }
}
