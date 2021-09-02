using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnVideoType
    {
        //public SnVideoType()
        //{
        //    SnVideo = new HashSet<SnVideo>();
        //}
        public int VId { get; set; }
        public string VType { get; set; }

        //public virtual ICollection<SnVideo> SnVideo { get; set; }
    }
}
