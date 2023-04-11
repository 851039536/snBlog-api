using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public partial class UserDto
    {
        /// <summary>
        /// UserDto
        /// </summary>
        public UserDto()
        {
            Articles = new HashSet<Article>();
            Interfaces = new HashSet<Interface>();
            SnLeaves = new HashSet<SnLeave>();
            SnNavigations = new HashSet<SnNavigation>();
            SnOnes = new HashSet<SnOne>();
            SnPictures = new HashSet<SnPicture>();
            SnSetblogs = new HashSet<SnSetblog>();
            SnTalks = new HashSet<SnTalk>();
            SnUserTalks = new HashSet<SnUserTalk>();
            SnVideos = new HashSet<Video>();
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 账户名
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
        /// 头像标识
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
        /// 别名
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Brief { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Interface> Interfaces { get; set; }
        public virtual ICollection<SnLeave> SnLeaves { get; set; }
        public virtual ICollection<SnNavigation> SnNavigations { get; set; }
        public virtual ICollection<SnOne> SnOnes { get; set; }
        public virtual ICollection<SnPicture> SnPictures { get; set; }
        public virtual ICollection<SnSetblog> SnSetblogs { get; set; }
        public virtual ICollection<SnTalk> SnTalks { get; set; }
        public virtual ICollection<SnUserTalk> SnUserTalks { get; set; }
        public virtual ICollection<Video> SnVideos { get; set; }
    }
}
