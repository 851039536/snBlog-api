using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnSoftware
{
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 图片路径
    /// </summary>
    public string Img { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int? TypeId { get; set; }

    /// <summary>
    /// 评论
    /// </summary>
    public int? CommentId { get; set; }

    /// <summary>
    /// 时间
    /// </summary>
    public DateTime? TimeCreate { get; set; }

    public DateTime? TimeModified { get; set; }

    public virtual SnSoftwareType Type { get; set; }
}
