using Microsoft.EntityFrameworkCore;

namespace Snblog.IService.IService.Articles;

/// <summary>
/// 文章接口
/// </summary>
public interface IArticleService
{
    /// <summary>
    /// 查询总数 
    /// </summary>
    /// <param name="identity">所有:0|分类:1|标签:2|用户3</param>
    /// <param name="type">条件</param>
    /// <param name="cache">缓存</param>
    /// <returns>int</returns>
    Task<int> GetSumAsync(int identity,string type,bool cache);



    /// <summary>
    /// 执行模糊查询操作，根据不同的标识和类型筛选文章。
    /// </summary>
    /// <param name="identity">查询的标识类型，具体值含义：所有:0|分类:1|标签:2|用户:3|标签,用户:4</param>
    /// <param name="type">查询的类型参数，用于进一步筛选文章。多条件以','分割。</param>
    /// <param name="name">查询的关键字段，用于模糊匹配文章名称。</param>
    /// <param name="cache">是否使用缓存。如果为true，则优先从缓存中获取结果。</param>
    /// <returns>包含匹配文章的列表。</returns>
    Task<List<ArticleDto>> GetContainsAsync(int identity,string type,string name,bool cache);

    /// <summary>
    /// 内容统计
    /// </summary>
    /// <param name="identity">统计的类型：所有:0|分类:1|标签:2|用户:3</param>
    /// <param name="type">统计的内容类型：内容:1|阅读:2|点赞:3</param>
    /// <param name="name">查询参数，如分类名、标签名或用户名</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>统计结果，整数类型</returns>
    Task<int> GetStrSumAsync(int identity,int type,string name,bool cache);

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id">主键</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>ArticleDto对象</returns>
    Task<ArticleDto> GetByIdAsync(int id,bool cache);

    /// <summary>
    ///类别查询
    /// </summary>
    /// <param name="identity">分类:1|标签:2</param>
    /// <param name="type">类别</param>
    /// <param name="cache">缓存</param>
    Task<List<ArticleDto>> GetTypeAsync(int identity,string type,bool cache);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="identity">查询的类型：所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
    /// <param name="type">查询参数，多条件以','分割</param>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">每页记录条数</param>
    /// <param name="ordering">排序规则：data:时间|read:阅读|give:点赞|id:主键</param>
    /// <param name="isDesc">排序方式：true为降序，false为升序</param>
    /// <param name="cache">是否使用缓存</param>
    /// <returns>分页查询结果，List ArticleDto类型</returns>
    Task<List<ArticleDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>bool</returns>
    Task<bool> DelAsync(int id);

    /// <summary>
    /// 新增
    /// </summary>
    /// <returns>bool</returns>
    Task<bool> AddAsync(Article entity);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>bool</returns>
    Task<bool> UpdateAsync(Article entity);

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="type">更新字段: Read | Give | Comment</param>
    /// <returns>bool</returns>
    Task<bool> UpdatePortionAsync(Article entity,string type);
}