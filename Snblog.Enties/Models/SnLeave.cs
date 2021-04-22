using System;

namespace Snblog.Enties.Models
{
    public partial class SnLeave
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Time { get; set; }
        public int? UserId { get; set; }
    }
}
