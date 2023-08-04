using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnPicture
{
    public int Id { get; set; }

    /// <summary>
    /// 图床名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    public string ImgUrl { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int TypeId { get; set; }

    public int UserId { get; set; }

    public virtual SnPictureType Type { get; set; }

    public virtual User User { get; set; }
}
