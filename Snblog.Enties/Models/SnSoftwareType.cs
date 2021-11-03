using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnSoftwareType
    {
        public SnSoftwareType()
        {
            SnSoftwares = new HashSet<SnSoftware>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SnSoftware> SnSoftwares { get; set; }
    }
}
