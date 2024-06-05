namespace Snblog.Enties.Models;

public partial class Permission
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 权限描述
    /// </summary>
    public string Description { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
