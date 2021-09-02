using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnTalk
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime TalkTime { get; set; }
        public string TalkTitle { get; set; }
        public string TalkBrief { get; set; }
        public string TalkText { get; set; }
        public int TalkRead { get; set; }
        public int TalkGive { get; set; }
        public int TalkComment { get; set; }
        public int TalkTypeId { get; set; }

        //public virtual SnUser User { get; set; }
    }
}
