using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnPictureType
{
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; }

    public virtual ICollection<SnPicture> SnPictures { get; set; } = new List<SnPicture>();
}
