using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnPicture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Itle { get; set; }
        public int? TypeId { get; set; }

        public virtual SnPictureType Type { get; set; }
    }
}
