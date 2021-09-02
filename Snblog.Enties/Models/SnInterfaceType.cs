using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Snblog.Models
{
    public partial class SnInterfaceType
    {
        public SnInterfaceType()
        {
            //SnInterface = new HashSet<SnInterface>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<SnInterface> SnInterface { get; set; }
    }
}
