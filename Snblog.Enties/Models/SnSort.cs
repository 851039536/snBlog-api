using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnSort
    {
        //public SnSort()
        //{
        //    SnArticle = new HashSet<SnArticle>();
        //}
        public int SortId { get; set; }
        public string SortName { get; set; }
        public string SortAlias { get; set; }
        public string SortDescription { get; set; }
        public int? ParentSortId { get; set; }

        //public virtual ICollection<SnArticle> SnArticle { get; set; }
    }
}
