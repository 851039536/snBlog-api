using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models;

public partial class SnPictureType
{
    public SnPictureType()
    {
        SnPictures = new HashSet<SnPicture>();
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<SnPicture> SnPictures { get; set; }
}