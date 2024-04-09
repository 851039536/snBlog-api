namespace Snblog.Service.Extensions;

/// <summary>
/// 扩展EF Core以便在多个地方重复使用相同的代码
/// 封装Select返回ArticleDto实体
/// 使用AsNoTracking方法来禁用跟踪
/// </summary>
public static class DiaryExtensions
{
    public static IQueryable<DiaryDto> SelectDiary(this IQueryable<Diary> diaries)
    {
        return diaries.Select(e => new DiaryDto {
            Id = e.Id,
            Name = e.Name,
            Text =e.Text,
            Img = e.Img,
            Read = e.Read,
            Give = e.Give,
            CommentId = e.CommentId,
            TimeCreate = e.TimeCreate,
            TimeModified = e.TimeModified,
            UserId = e.UserId,
            User = new User {
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            TypeId = e.TypeId,
            Type = new DiaryType {
                Name = e.Type.Name
            },
              
        }).AsNoTracking();
    }
}