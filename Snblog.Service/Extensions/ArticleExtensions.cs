namespace Snblog.Service.Extensions;

/// <summary>
/// 扩展IQueryable
/// 封装Select返回ArticleDto实体
/// 使用AsNoTracking方法来禁用跟踪
/// </summary>
public static class ArticleExtensions
{
    public static IQueryable<ArticleDto> SelectArticle(this IQueryable<Article> ret)
    {
        // 首先调用AsNoTracking来禁用跟踪，然后再进行Select转换  
        return ret.AsNoTracking().Select(e => new ArticleDto {
            Id = e.Id,
            Name = e.Name,
            Sketch = e.Sketch,
            Text =e.Text,
            Give = e.Give,
            Read = e.Read,
            Img = e.Img,
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
            UserId = e.UserId,
            User = new User {
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            TypeId = e.TypeId,
            Type = new ArticleType {
                Name = e.Type.Name
            },
            TagId = e.TagId,
            Tag = new ArticleTag {
                Name = e.Tag.Name
            },
        });
    }
}