namespace Snblog.Service.Extensions;

/// <summary>
/// 封装Select返回ArticleDto实体
/// 扩展EF Core以便在多个地方重复使用相同的代码
/// 使用AsNoTracking方法来禁用跟踪
/// </summary>
public static class UserTalkExtensions
{
    public static IQueryable<UserTalkDto> SelectUserTalk(this IQueryable<UserTalk> ret)
    {
        return ret.AsNoTracking().Select(e => new UserTalkDto {
            Id = e.Id,
            Text =e.Text,
            Read = e.Read,
            Give = e.Give,
            TimeCreate = e.TimeCreate,
            UserId = e.UserId,
            User = new User {
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            CommentId = e.CommentId
               
        });
    }
}