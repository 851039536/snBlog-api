using System;
using System.Collections.Generic;

namespace Snblog.Enties.ModelsDto
{
    public partial class SnippetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public int? TypeId { get; set; }

        public int? TypeSubId { get; set; }

        public int? TagId { get; set; }

        public int? UserId { get; set; }

        public int? SnippetVersionId { get; set; }

        public DateTime? TimeCreate { get; set; }

        public DateTime? TimeUpdate { get; set; }

        public virtual SnippetTag Tag { get; set; }

        public virtual SnippetType Type { get; set; }

        public virtual SnippetTypeSub TypeSub { get; set; }

        public virtual User User { get; set; }
    }
}