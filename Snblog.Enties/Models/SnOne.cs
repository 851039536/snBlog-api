using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnOne
    {
        public int OneId { get; set; }
        public string OneTitle { get; set; }
        public string OneText { get; set; }
        public string OneImg { get; set; }
        public string OneClassify { get; set; }
        public string OneAuthor { get; set; }
        public string OneData { get; set; }
        public int? OneRead { get; set; }
        public int? OneGive { get; set; }
        public string OneComment { get; set; }
    }
}
