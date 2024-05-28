using System;
using System.Collections.Generic;

namespace Snblog.Enties.Models;

public partial class Navigation
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 导航标题
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 标题描述
    /// </summary>
    public string Describe { get; set; }

    /// <summary>
    /// 图片路径
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

    /// <summary>
    /// 用户
    /// </summary>
    public int UserId { get; set; }

    public DateTime? TimeCreate { get; set; }

    public DateTime? TimeModified { get; set; }

    public virtual NavigationType Type { get; set; }

    public virtual User User { get; set; }
}
