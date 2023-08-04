using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnTalkType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<SnTalk> SnTalks { get; set; } = new List<SnTalk>();
}
