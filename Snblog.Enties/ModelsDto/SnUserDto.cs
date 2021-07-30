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
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPwd { get; set; }
        public string UserPhoto { get; set; }
        public string UserTime { get; set; }
        public string UserNickname { get; set; }
        public string UserBrief { get; set; }

        //public virtual ICollection<SnArticle> SnArticle { get; set; }
        //public virtual ICollection<SnTalk> SnTalk { get; set; }
        //public virtual ICollection<SnUserTalk> SnUserTalk { get; set; }
    }
}
