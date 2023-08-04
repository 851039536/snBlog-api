using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnSetblog
{
    public int Id { get; set; }

    /// <summary>
    /// 设置的内容名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路由链接
    /// </summary>
    public string RouterUrl { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Isopen { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 关联用户表
    /// </summary>
    public int UserId { get; set; }

    public virtual SnSetblogType Type { get; set; }

    public virtual User User { get; set; }
}
