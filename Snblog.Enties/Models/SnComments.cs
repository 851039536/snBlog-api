using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnComments
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? ArticleId { get; set; }
        public int? CommentCount { get; set; }
        public string CommentDate { get; set; }
        public string CommentText { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
