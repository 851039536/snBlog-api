using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnSetblogType
    {
        public SnSetblogType()
        {
            SnSetblogs = new HashSet<SnSetblog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SnSetblog> SnSetblogs { get; set; }
    }
}
