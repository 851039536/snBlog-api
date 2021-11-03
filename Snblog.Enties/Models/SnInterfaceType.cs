using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnInterfaceType
    {
        public SnInterfaceType()
        {
            SnInterfaces = new HashSet<SnInterface>();
            SnNavigations = new HashSet<SnNavigation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SnInterface> SnInterfaces { get; set; }
        public virtual ICollection<SnNavigation> SnNavigations { get; set; }
    }
}
