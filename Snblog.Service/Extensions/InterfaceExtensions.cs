namespace Snblog.Service.Extensions
{
    /// <summary>
    /// 封装Select返回ArticleDto实体
    /// 扩展EF Core以便在多个地方重复使用相同的代码
    /// 使用AsNoTracking方法来禁用跟踪
    /// </summary>
    public static class InterfaceExtensions
    {
        public static IQueryable<InterfaceDto> SelectInterface(this IQueryable<Interface> interfaces)
        {
            return interfaces.Select(e => new InterfaceDto {
                Id = e.Id,
                Name = e.Name,
                Path = e.Path,
                Identity = e.Identity,
                UserId = e.UserId,
                User = new User {
                    Name = e.User.Name,
                    Nickname = e.User.Nickname
                },
                TypeId = e.TypeId,
                Type = new InterfaceType {
                    Name = e.Type.Name
                },
            }).AsNoTracking();
        }
    }
}
