using System.ComponentModel.DataAnnotations;

namespace Snblog.Enties.Models
{
    public partial class SnArticle
    {
        /// <summary>
        /// 文章主键
        /// </summary>

        public int article_id { get; set; }

        /// <summary>
        /// 发表人id
        /// </summary>
        public int? user_id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string title { get; set; }

        /// <summary>
        /// 内容简述
        /// </summary>
        public string title_text { get; set; }
        /// <summary>
        /// 博客内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string text { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public int? label_id { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        public int? read { get; set; }
        /// <summary>
        /// 点赞
        /// </summary>
        public int? give { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int? sort_id { get; set; }
        /// <summary>
        /// 分类标题
        /// </summary>
        public string type_title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string url_img { get; set; }
    }
}
