using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Snblog.Util.Paginated;

public class PaginatedList<T>
{
    // 分页列表中的数据项集合
    public List<T> Items { get; }

    // 数据源的总记录数
    public int TotalCount { get; }

    // 总页数
    public int TotalPages { get; }

    // 当前页码
    public int CurrentPage { get; }

    // 每页显示的记录数
    public int PageSize { get; }

    // 构造函数，初始化分页列表的各项属性
    public PaginatedList(IEnumerable<T> items,int totalCount,int currentPage,int pageSize)
    {
        Items = items.ToList(); // 将数据项转换为列表
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize); // 计算总页数
    }

    // 判断是否存在下一页
    public bool HasNextPage => CurrentPage < TotalPages;

    // 判断是否存在上一页
    public bool HasPreviousPage => CurrentPage > 1;
}

public static class PaginatedListExtensions
{
    /// <summary>
    /// 创建一个分页列表。
    /// </summary>
    /// <typeparam name="T">实体类型。</typeparam>
    /// <param name="source">IQueryable数据源。</param>
    /// <param name="pageIndex">当前页码（从1开始）。</param>
    /// <param name="pageSize">每页显示的记录数。</param>
    /// <returns>一个包含分页信息的PaginatedList对象。</returns>
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,int pageIndex,int pageSize)
    {
        if (pageIndex < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex),"页码不能小于1。");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize),"每页数量不能小于1。");
        }

        // 获取总数
        int totalCount = await source.CountAsync();

        // 跳过前面的记录并取指定数量的记录
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items,totalCount,pageIndex,pageSize);
    }
}