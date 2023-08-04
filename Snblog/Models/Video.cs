using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class Video
{
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }

    /// <summary>
    /// 链接路径
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int TypeId { get; set; }

    public int UserId { get; set; }

    /// <summary>
    /// 时间
    /// </summary>
    public DateTime TimeCreate { get; set; }

    public DateTime TimeModified { get; set; }

    public virtual SnVideoType Type { get; set; }

    public virtual User User { get; set; }
}
