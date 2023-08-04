using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnNavigationType
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    public virtual ICollection<SnNavigation> SnNavigations { get; set; } = new List<SnNavigation>();
}
