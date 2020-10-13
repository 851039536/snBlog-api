using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnVideo
    {
        public int VId { get; set; }
        public string VTitle { get; set; }
        public string VData { get; set; }
        public string VImg { get; set; }
        public int? VTypeid { get; set; }
        public string VUrl { get; set; }
    }
}
