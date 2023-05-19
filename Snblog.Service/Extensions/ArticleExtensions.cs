namespace Snblog.Service.Extensions
{
    /// <summary>
    /// 封装Select返回ArticleDto实体
    /// 扩展EF Core以便在多个地方重复使用相同的代码
    /// 使用AsNoTracking方法来禁用跟踪
    /// </summary>
    public static class ArticleExtensions
    {
        public static IQueryable<ArticleDto> SelectArticle(this IQueryable<Article> articles)
        {
            return articles.Select(e => new ArticleDto {
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
            }).AsNoTracking();
        }
    }
}
