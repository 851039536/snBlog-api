
namespace Snblog.IService.IService;

/// <summary>
/// 业务类接口
/// </summary>
public interface ISnSetBlogService
{

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<SnSetblog>> GetAllAsync(bool cache);

    /// <summary>
    /// 模糊查询
    /// </summary>
    /// <param name="name">查询字段</param>
    /// <param name="cache">是否缓存</param>
    /// <returns></returns>
    Task<List<ArticleDto>> GetContainsAsync(string name, bool cache);
    /// <summary>
    /// 读取[字段/阅读/点赞]数量
    /// </summary>
    /// <param name="type">条件</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<int> GetSumAsync(string type, bool cache);
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<SnSetblogDto> GetByIdAsync(int id,bool cache);

    /// <summary>
    /// 按分类条件查询
    /// </summary>
    /// <param name="sortId"></param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<Article>> GetTypeIdAsync(int sortId,bool cache);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户:2</param>
    /// <param name="type">类别参数, identity 0 可不填</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序[true/false]</param>
    /// <param name="cache">是否开启缓存</param>
    /// <param name="ordering">排序条件[按id排序]</param>
    /// <returns></returns>
    Task<List<SnSetblogDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache);

    /// <summary>
    /// 按标签分页查询 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// /// <param name="cache">是否开启缓存</param>
    Task< List<Article>> GetfySortTestAsync(int type, int pageIndex, int pageSize, bool isDesc,bool cache);
    /// <summary>
    /// 条件分页查询
    /// </summary>
    /// <param name="type">查询条件</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="isDesc">是否倒序</param>
    /// <param name="name">排序条件</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<Article>> GetFyAsync(int type, int pageIndex, int pageSize, string name, bool isDesc,bool cache);

    /// <summary>
    /// 查询文章(无文章内容 缓存)
    /// </summary>
    /// <param name="pageIndex">当前页码[1]</param>
    /// <param name="pageSize">每页记录条数[10]</param>
    /// <param name="isDesc">是否倒序[true/false]</param>
    /// /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<Article>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc,bool cache);

    /// <summary>
    /// 按标签id查询
    /// </summary>
    /// <param name="tag">标签id</param>
    /// <param name="isDesc">是否倒序[true/false]</param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<List<Article>> GetTagAsync(int tag,bool isDesc,bool cache);

    /// <summary>
    /// 查询分类总数
    /// </summary>
    /// <param name="type"></param>
    /// /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    Task<int> GetConutSortAsync(int type,bool cache);

    /// <summary>
    /// 条件查询总数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="cache">是否开启缓存</param>
    /// <returns></returns>
    int GetTypeCountAsync(int type,bool cache);
    /// <summary>
    /// 按id删除
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// 异步添加数据
    /// </summary>
    /// <returns></returns>
    Task<bool> AddAsync(SnSetblogDto entity);

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(SnSetblogDto entity);

    /// <summary>
    /// 更新部分列[comment give read]
    /// </summary>
    /// <param name="Article"></param>
    /// <param name="type">更新的字段</param>
    /// <returns></returns>
    Task<bool> UpdatePortionAsync(SnSetblogDto entity, string type);

    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="identity">所有:0 || 分类:1 || 用户2  </param>
    /// <param name="type">条件(identity为0则填0) </param>
    /// <param name="cache"></param>
    Task<int> GetCountAsync(int identity, string type, bool cache);
}