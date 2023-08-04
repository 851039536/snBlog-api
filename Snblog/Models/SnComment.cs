using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnComment
{
    /// <summary>
    /// 评论主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public int Give { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 评论日期
    /// </summary>
    public DateTime TimeCreate { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime TimeModified { get; set; }
}
