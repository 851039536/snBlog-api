using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnVideoType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
