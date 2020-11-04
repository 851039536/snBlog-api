using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnOneType
    {
        public int Id { get; set; }
        public int? SoTypeId { get; set; }
        public string SoTypeTitle { get; set; }
    }
}
