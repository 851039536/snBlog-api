using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnArticle
    {
        public int ArticleId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string TitleText { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public int? LabelId { get; set; }
        public int? Read { get; set; }
        public int? Give { get; set; }
        public string Comment { get; set; }
        public int? SortId { get; set; }
        public string TypeTitle { get; set; }
        public string UrlImg { get; set; }
    }
}
