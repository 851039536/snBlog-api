namespace Snblog.Enties.Models;

public partial class Role
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 角色描述
    /// </summary>
    public string Description { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
