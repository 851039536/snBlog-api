using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Snblog.Models
{
    public partial class SnUser
    {
        public SnUser()
        {
            //SnArticle = new HashSet<SnArticle>();
            //SnInterface = new HashSet<SnInterface>();
            //SnUserTalk = new HashSet<SnUserTalk>();
        }

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
        //public virtual ICollection<SnInterface> SnInterface { get; set; }
        //public virtual ICollection<SnUserTalk> SnUserTalk { get; set; }
    }
}
