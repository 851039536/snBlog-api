using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Snblog.Models
{
    public partial class SnInterface
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int TypeId { get; set; }
        public int? UserId { get; set; }
        public bool Identity { get; set; }

        //public virtual SnInterfaceType Type { get; set; }
        //public virtual SnUser User { get; set; }
    }
}
