using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnLabels
    {
        //public SnLabels()
        //{
        //    SnArticle = new HashSet<SnArticle>();
        //}
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public string LabelAlias { get; set; }
        public string LabelDescription { get; set; }

       // public virtual ICollection<SnArticle> SnArticle { get; set; }
    }
}
