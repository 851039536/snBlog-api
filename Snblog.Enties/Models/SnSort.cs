using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnSort
    {
        public int SortId { get; set; }
        public string SortName { get; set; }
        public string SortAlias { get; set; }
        public string SortDescription { get; set; }
        public int? ParentSortId { get; set; }
    }
}
