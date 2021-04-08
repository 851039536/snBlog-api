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

        public int article_id { get; set; }
        public int? user_id { get; set; }
        [Required(ErrorMessage = "标题不能为空")]
        public string title { get; set; }
        public string title_text { get; set; }
        [Required(ErrorMessage = "内容不能为空")]
        public string text { get; set; }
        public string time { get; set; }
        public int? label_id { get; set; }
        public int? read { get; set; }
        public int? give { get; set; }
        public string comment { get; set; }
        public int? sort_id { get; set; }
        public string type_title { get; set; }
        public string url_img { get; set; }
    }
}
