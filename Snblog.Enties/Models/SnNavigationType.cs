using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnNavigationType
    {
        public SnNavigationType()
        {
            SnNavigations = new HashSet<SnNavigation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<SnNavigation> SnNavigations { get; set; }
    }
}
