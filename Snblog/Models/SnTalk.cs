using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnTalk
{
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Describe { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 阅读量
    /// </summary>
    public int Read { get; set; }

    /// <summary>
    /// 点赞
    /// </summary>
    public int Give { get; set; }

    /// <summary>
    /// 评论
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public int UserId { get; set; }

    public int TypeId { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public DateTime TimeCreate { get; set; }

    public DateTime TimeModified { get; set; }

    public virtual SnTalkType Type { get; set; }

    public virtual User User { get; set; }
}
