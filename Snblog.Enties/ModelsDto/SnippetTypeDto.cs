using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnippetTypeDto
        {
        public SnippetTypeDto()
        {
            Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}
