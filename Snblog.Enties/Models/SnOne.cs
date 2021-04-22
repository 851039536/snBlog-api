using System;

namespace Snblog.Enties.Models
{
    public partial class SnOne
    {
        public int OneId { get; set; }
        public string OneTitle { get; set; }
        public string OneText { get; set; }
        public string OneImg { get; set; }
        public int? OneTypeId { get; set; }
        public string OneAuthor { get; set; }
        public DateTime? OneData { get; set; }
        public int? OneRead { get; set; }
        public int? OneGive { get; set; }
        public uint? OneComment { get; set; }
    }
}
