using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class ArticleTagDto
    {
        public ArticleTagDto()
        {
            SnArticles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Article> SnArticles { get; set; }
    }
}
