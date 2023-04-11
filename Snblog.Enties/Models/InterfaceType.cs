using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class InterfaceType
    {
        public InterfaceType()
        {
            Interfaces = new HashSet<Interface>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Interface> Interfaces { get; set; }
    }
}
