using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto;

public partial class NavigationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Describe { get; set; }
    public string Img { get; set; }
    public string Url { get; set; }
    public int TypeId { get; set; }
    public int UserId { get; set; }
    public DateTime TimeCreate { get; set; }
    public DateTime TimeModified { get; set; }

    public virtual NavigationType Type { get; set; }
    public virtual User User { get; set; }
}