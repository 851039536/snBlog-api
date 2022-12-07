using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnInterface
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public bool Identity { get; set; }

        public virtual SnInterfaceType Type { get; set; }
        public virtual User User { get; set; }
    }
}
