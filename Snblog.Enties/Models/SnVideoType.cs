using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnVideoType
    {
        public SnVideoType()
        {
            Videos = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
