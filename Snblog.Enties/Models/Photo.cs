using System;
using System.Collections.Generic;

namespace Snblog.Models;

/// <summary>
/// 图册
/// </summary>
public partial class Photo
{
    public int Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 阅读
    /// </summary>
    public short Read { get; set; }

    /// <summary>
    /// 热度
    /// </summary>
    public short Give { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// 用户
    /// </summary>
    public int? UserId { get; set; }

    public int? PhotoGalleryId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? TimeCreate { get; set; }

    public virtual Enties.ModelsDto.PhotoGalleryDto PhotoGallery { get; set; }

    public virtual PhotoType Type { get; set; }

    public virtual User User { get; set; }
}
