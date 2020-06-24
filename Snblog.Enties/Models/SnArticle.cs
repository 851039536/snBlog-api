using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnArticle
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ArticleId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文章简介
        /// </summary>
        public string TitleText { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
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
