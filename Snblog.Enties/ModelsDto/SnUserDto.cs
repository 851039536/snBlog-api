using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnUserDto
    {
        //public SnUser()
        //{
        //    //SnArticle = new HashSet<SnArticle>();
        //    //SnTalk = new HashSet<SnTalk>();
        //    //SnUserTalk = new HashSet<SnUserTalk>();
        //}

        public int UserId { get; set; }
        public string UserIp { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Photo { get; set; }
        public DateTime TimeCreate { get; set; }
        public string Nickname { get; set; }
        public string Brief { get; set; }
        public DateTime? TimeModified { get; set; }

        //public virtual ICollection<SnArticle> SnArticle { get; set; }
        //public virtual ICollection<SnTalk> SnTalk { get; set; }
        //public virtual ICollection<SnUserTalk> SnUserTalk { get; set; }
    }
}
