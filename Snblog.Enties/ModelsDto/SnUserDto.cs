using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnUserDto
    {
        public SnUserDto()
        {
            SnArticles = new HashSet<SnArticle>();
            SnInterfaces = new HashSet<SnInterface>();
            SnLeaves = new HashSet<SnLeave>();
            SnNavigations = new HashSet<SnNavigation>();
            SnOnes = new HashSet<SnOne>();
            SnPictures = new HashSet<SnPicture>();
            SnSetblogs = new HashSet<SnSetblog>();
            SnTalks = new HashSet<SnTalk>();
            SnUserTalks = new HashSet<SnUserTalk>();
            SnVideos = new HashSet<SnVideo>();
        }

        public int Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Photo { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }
        public string Nickname { get; set; }
        public string Brief { get; set; }

        public virtual ICollection<SnArticle> SnArticles { get; set; }
        public virtual ICollection<SnInterface> SnInterfaces { get; set; }
        public virtual ICollection<SnLeave> SnLeaves { get; set; }
        public virtual ICollection<SnNavigation> SnNavigations { get; set; }
        public virtual ICollection<SnOne> SnOnes { get; set; }
        public virtual ICollection<SnPicture> SnPictures { get; set; }
        public virtual ICollection<SnSetblog> SnSetblogs { get; set; }
        public virtual ICollection<SnTalk> SnTalks { get; set; }
        public virtual ICollection<SnUserTalk> SnUserTalks { get; set; }
        public virtual ICollection<SnVideo> SnVideos { get; set; }
    }
}
