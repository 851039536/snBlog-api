using System;
using System.Collections.Generic;

namespace Snblog.Models;

/// <summary>
/// 日记表
/// </summary>
public partial class Diary
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
    /// 内容
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }

    /// <summary>
    /// 阅读数
    /// </summary>
    public int Read { get; set; }

    /// <summary>
    /// 点赞
    /// </summary>
    public int Give { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 评论
    /// </summary>
    public uint CommentId { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 时间
    /// </summary>
    public DateTime TimeCreate { get; set; }

    public DateTime TimeModified { get; set; }

    public virtual DiaryType Type { get; set; }

    public virtual User User { get; set; }
}
