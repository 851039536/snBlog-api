using System;
using System.Collections.Generic;

namespace Snblog.Models;

/// <summary>
/// 日记分类
/// </summary>
public partial class DiaryType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Diary> Diaries { get; set; } = new List<Diary>();
}
