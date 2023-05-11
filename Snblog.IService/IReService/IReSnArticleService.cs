
namespace Snblog.IService.IReService
{
    public interface IReSnArticleService
    {
        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// 条件查询总条数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
         Task<int> CountAsync(int type);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<Article>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <returns></returns>
        Task<Article> GetByIdAsync(int id);

        /// <summary>
        /// 分类查询
        /// </summary>
        /// <param name="id">分类id(label_id)</param>
        /// <returns></returns>
        Task<List<Article>> GetLabelAllAsync(int id);

        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSumAsync(string type);

        /// <summary>
        /// 查询文章(无文章内容 缓存)
        /// </summary>
        /// <param name="pageIndex">当前页码[1]</param>
        /// <param name="pageSize">每页记录条数[10]</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        Task<List<Article>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 分页查询 (条件)
        /// </summary>
        /// <param name="type">分类 : 00-表示查询所有</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        Task<List<Article>> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc);

        /// <summary>
        /// 分页查询(条件排序)
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="order">排序条件</param>
        /// <returns></returns>
        Task<List<Article>> GetFyTypeorderAsync(int type, int pageIndex, int pageSize, string order, bool isDesc);

        /// <summary>
        /// 按标签id查询
        /// </summary>
        /// <param name="tag">标签id</param>
        /// <param name="isDesc">是否倒序[true/false]</param>
        /// <returns></returns>
        Task<List<Article>> GetTagtextAsync(int tag, bool isDesc);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="article">models</param>
        /// <returns></returns>
        Task<Article> AddAsync(Article article);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<string> UpdateAsync(Article entity);
        /// <summary>
        /// 更新部分列[comment give read]
        /// </summary>
        /// <param name="Article"></param>
        /// <param name="name">更新的字段</param>
        /// <returns></returns>
        Task<bool> UpdatePortionAsync(Article Article, string name);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<string> DeleteAsync(int id);
    }
}
