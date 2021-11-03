using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnLabel
    {
        public SnLabel()
        {
            SnArticles = new HashSet<SnArticle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SnArticle> SnArticles { get; set; }
    }
}
