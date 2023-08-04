using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class InterfaceType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Interface> Interfaces { get; set; } = new List<Interface>();
}
