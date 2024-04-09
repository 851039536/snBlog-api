namespace Snblog.Service.Extensions;

/// <summary>
/// 封装Select返回Dto实体
/// 扩展EF Core以便在多个地方重复使用相同的代码
/// 使用AsNoTracking方法来禁用跟踪
/// </summary>
public static class NavigationExtensions
{
    public static IQueryable<NavigationDto> SelectNavigation(this IQueryable<Navigation> ret)
    {
        return ret.AsNoTracking().Select(e => new NavigationDto {
            Id = e.Id,
            Name = e.Name,
            Describe = e.Describe,
            Img = e.Img,
            Url = e.Url,
            TimeCreate = (DateTime)e.TimeCreate,
            TimeModified = (DateTime)e.TimeModified,
            UserId = e.UserId,
            User = new User {
                Name = e.User.Name,
                Nickname = e.User.Nickname
            },
            TypeId = e.TypeId,
            Type = new NavigationType {
                Name = e.Type.Name
            },
        });
    }
}