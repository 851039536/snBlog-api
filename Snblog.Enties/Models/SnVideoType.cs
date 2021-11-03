using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnVideoType
    {
        public SnVideoType()
        {
            SnVideos = new HashSet<SnVideo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SnVideo> SnVideos { get; set; }
    }
}
