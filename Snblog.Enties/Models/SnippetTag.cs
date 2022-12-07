using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnippetTag
    {
        public SnippetTag()
        {
            Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}
