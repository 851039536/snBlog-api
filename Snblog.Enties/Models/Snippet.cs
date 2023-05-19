using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
    {
    public partial class Snippet
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int? TypeId { get; set; }
        public int? TagId { get; set; }
        public int? UserId { get; set; }
        public int? LabelId { get; set; }

        public virtual SnippetLabel Label { get; set; }
        public virtual SnippetTag Tag { get; set; }
        public virtual SnippetType Type { get; set; }
        public virtual User User { get; set; }
        }
    }
