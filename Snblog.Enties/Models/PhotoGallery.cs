using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class PhotoGallery
{
    public int Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    public int TypeId { get; set; }

    public int UserId { get; set; }

    public string Tag { get; set; }

    public short Give { get; set; }

    public DateTime TimeCreate { get; set; }

    public DateTime TimeModified { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual PhotoGalleryType Type { get; set; }

    public virtual User User { get; set; }
}
