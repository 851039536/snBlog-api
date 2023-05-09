using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
            Diaries = new HashSet<Diary>();
            Interfaces = new HashSet<Interface>();
            SnLeaves = new HashSet<SnLeave>();
            SnNavigations = new HashSet<SnNavigation>();
            SnPictures = new HashSet<SnPicture>();
            SnSetblogs = new HashSet<SnSetblog>();
            SnTalks = new HashSet<SnTalk>();
            SnUserTalks = new HashSet<SnUserTalk>();
            Snippets = new HashSet<Snippet>();
            Videos = new HashSet<Video>();
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

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual ICollection<Interface> Interfaces { get; set; }
        public virtual ICollection<SnLeave> SnLeaves { get; set; }
        public virtual ICollection<SnNavigation> SnNavigations { get; set; }
        public virtual ICollection<SnPicture> SnPictures { get; set; }
        public virtual ICollection<SnSetblog> SnSetblogs { get; set; }
        public virtual ICollection<SnTalk> SnTalks { get; set; }
        public virtual ICollection<SnUserTalk> SnUserTalks { get; set; }
        public virtual ICollection<Snippet> Snippets { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
