using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class UserTalk
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    /// <summary>
    /// 说说内容
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 发表时间
    /// </summary>
    public DateTime? TimeCreate { get; set; }

    public int? Read { get; set; }

    public int? Give { get; set; }

    /// <summary>
    /// 评论id
    /// </summary>
    public int? CommentId { get; set; }

    public virtual User User { get; set; }
}
