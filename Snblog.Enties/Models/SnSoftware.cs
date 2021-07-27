using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnSoftware
    {
        public int SoId { get; set; }
        public string SoTitle { get; set; }
        public string SoData { get; set; }
        public string SoImg { get; set; }
        public int? SoTypeid { get; set; }
        public string SoComment { get; set; }
    }
}
