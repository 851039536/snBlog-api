using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Sketch { get; set; }
        public string Text { get; set; }
        public short Read { get; set; }
        public short Give { get; set; }
        public string Img { get; set; }
        public short CommentId { get; set; }
        public int LabelId { get; set; }
        public int SortId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual SnLabel Label { get; set; }
        public virtual SnSort Sort { get; set; }
        public virtual SnUser User { get; set; }
    }
}
