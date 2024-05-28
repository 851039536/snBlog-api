using System;
using System.Collections.Generic;

namespace Snblog.Enties.Models;

public partial class SnippetTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<SnippetTypeSub> SnippetTypeSubs { get; set; } = new List<SnippetTypeSub>();

    public virtual ICollection<Snippet> Snippets { get; set; } = new List<Snippet>();
}