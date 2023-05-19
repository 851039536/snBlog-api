using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
    {
    public partial class SnippetTagDto
        {
        public SnippetTagDto()
            {
            Snippets = new HashSet<Snippet>();
            }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
        }
    }
