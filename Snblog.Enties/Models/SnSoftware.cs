using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models;

public partial class SnSoftware
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Img { get; set; }
    public int? TypeId { get; set; }
    public int? CommentId { get; set; }
    public DateTime? TimeCreate { get; set; }
    public DateTime? TimeModified { get; set; }

    public virtual SnSoftwareType Type { get; set; }
}