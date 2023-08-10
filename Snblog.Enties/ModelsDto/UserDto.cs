using Snblog.Models;
using System;
using System.Collections.Generic;

namespace Snblog.Enties.ModelsDto;

public partial class UserDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ip地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Photo { get; set; }

    /// <summary>
    /// 注册时间
    /// </summary>
    public DateTime TimeCreate { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime TimeModified { get; set; }

    /// <summary>
    /// 称呼
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string Brief { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Diary> Diaries { get; set; } = new List<Diary>();

    public virtual ICollection<Interface> Interfaces { get; set; } = new List<Interface>();

    public virtual ICollection<PhotoGallery> PhotoGalleries { get; set; } = new List<PhotoGallery>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Navigation> Navigations { get; set; } = new List<Navigation>();

    public virtual ICollection<SnPicture> SnPictures { get; set; } = new List<SnPicture>();

    public virtual ICollection<SnSetblog> SnSetblogs { get; set; } = new List<SnSetblog>();

    public virtual ICollection<SnTalk> SnTalks { get; set; } = new List<SnTalk>();

    public virtual ICollection<Snippet> Snippets { get; set; } = new List<Snippet>();

    public virtual ICollection<UserTalk> UserTalks { get; set; } = new List<UserTalk>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
