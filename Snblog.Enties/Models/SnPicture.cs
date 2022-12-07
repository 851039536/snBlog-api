using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnPicture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }

        public virtual SnPictureType Type { get; set; }
        public virtual User User { get; set; }
    }
}
