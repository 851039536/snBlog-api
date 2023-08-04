using System;
using System.Collections.Generic;

namespace Snblog.Enties.Models;

/// <summary>
/// 片段的历史版本表
/// </summary>
public partial class SnippetVersion
{
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
    /// 关联文章主键
    /// </summary>
    public int SnippetId { get; set; }

    /// <summary>
    /// 版本变更次数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime TimeCreate { get; set; }
}
