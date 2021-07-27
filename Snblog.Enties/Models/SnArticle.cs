using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snblog.Models
{
    public partial class SnArticle
    {

        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string TitleText { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public int LabelId { get; set; }
        public int Read { get; set; }
        public int Give { get; set; }
        public sbyte Comment { get; set; }
        public int SortId { get; set; }
        public string TypeTitle { get; set; }
        public string UrlImg { get; set; }

        //public virtual SnLabels Label { get; set; }
        //public virtual SnSort Sort { get; set; }
        //public virtual SnUser User { get; set; }
    }
}
