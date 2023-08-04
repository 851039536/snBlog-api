using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class PhotoGalleryType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; } = new List<PhotoGallery>();
}
