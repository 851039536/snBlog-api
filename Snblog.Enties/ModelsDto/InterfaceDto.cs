using Snblog.Enties.Models;

#nullable disable

namespace Snblog.Enties.ModelsDto;

public partial class InterfaceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public int TypeId { get; set; }
    public int UserId { get; set; }
    public bool Identity { get; set; }

    public virtual InterfaceType Type { get; set; }
    public virtual User User { get; set; }
}