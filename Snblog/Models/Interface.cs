using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class Interface
{
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 类别
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 显示隐藏
    /// </summary>
    public bool Identity { get; set; }

    public virtual InterfaceType Type { get; set; }

    public virtual User User { get; set; }
}
