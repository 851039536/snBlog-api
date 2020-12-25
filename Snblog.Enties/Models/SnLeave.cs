using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnLeave
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Time { get; set; }
        public int? UserId { get; set; }
    }
}
