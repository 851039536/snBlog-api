using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnLeave
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual User User { get; set; }
    }
}
