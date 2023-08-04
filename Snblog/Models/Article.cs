using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class Article
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 标题 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 内容简述
    /// </summary>
    public string Sketch { get; set; }

    /// <summary>
    /// 博客内容
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 阅读次数
    /// </summary>
    public short Read { get; set; }

    /// <summary>
    /// 点赞
    /// </summary>
    public short Give { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }

    /// <summary>
    /// 评论
    /// </summary>
    public short CommentId { get; set; }

    /// <summary>
    /// 标签外键
    /// </summary>
    public int TagId { get; set; }

    /// <summary>
    /// 分类外键
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 用户外键id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public DateTime TimeCreate { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime TimeModified { get; set; }

    public virtual ArticleTag Tag { get; set; }

    public virtual ArticleType Type { get; set; }

    public virtual User User { get; set; }
}
