using System;

namespace Snblog.Models
{
    public partial class SnArticleDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string TitleText { get; set; }
        public string Text { get; set; }
        public string Html { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime? TimeModified { get; set; }
        public short Read { get; set; }
        public short Give { get; set; }
        public short Comment { get; set; }
        public string TypeTitle { get; set; }
        public string UrlImg { get; set; }
        public int LabelId { get; set; }
        public int SortId { get; set; }
        public int UserId { get; set; }

        //public virtual SnLabels Label { get; set; }
        //public virtual SnSort Sort { get; set; }
        //public virtual SnUser User { get; set; }
    }
}
