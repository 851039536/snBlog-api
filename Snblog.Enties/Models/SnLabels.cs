using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnLabels
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string LabelAlias { get; set; }
        public string LabelDescription { get; set; }
    }
}
