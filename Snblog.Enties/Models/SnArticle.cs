using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnArticle
    {
        /// <summary>
        /// 文章主键
        /// </summary>

        public int ArticleId { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        public string TitleText { get; set; }
        [Required(ErrorMessage = "内容不能为空")]
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
