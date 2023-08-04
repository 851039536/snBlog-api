using System;
using System.Collections.Generic;

namespace Snblog.Enties.Models
{
    public partial class SnippetTag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; } = new List<Snippet>();
    }
}