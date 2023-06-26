namespace Snblog.Service.Extensions
{
    /// <summary>
    /// 封装Select
    /// 扩展EF Core以便在多个地方重复使用相同的代码
    /// 使用AsNoTracking方法来禁用跟踪
    /// </summary>
    public static class SnippetExtensions
    {
        public static IQueryable<SnippetDto> SelectSnippet(this IQueryable<Snippet> snippets)
        {
            return snippets.Select(e => new SnippetDto {
                Id = e.Id,
                Name = e.Name,
                Text = e.Text,
                UserId = e.UserId,
                User = new User {
                    Id = e.User.Id,
                    Name = e.User.Name,
                    Nickname = e.User.Nickname
                },
                TypeId = e.TypeId,
                Type = new SnippetType {
                    Id = e.Type.Id,
                    Name = e.Type.Name
                },
                TagId = e.TagId,
                Tag = new SnippetTag {
                    Id = e.Tag.Id,
                    Name = e.Tag.Name
                },
                LabelId = e.LabelId,
                Label = new SnippetLabel {
                    Id = e.Label.Id,
                    Name = e.Label.Name
                }
            }).AsNoTracking();
        }
    }
}
