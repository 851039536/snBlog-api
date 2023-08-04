using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class ArticleType
{
    public int Id { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 分类描述
    /// </summary>
    public string Description { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
