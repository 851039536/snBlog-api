using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnUserTalk
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string TalkText { get; set; }
        public DateTime? TalkTime { get; set; }
        public int? TalkRead { get; set; }
        public int? TalkGive { get; set; }
        public int? CommentId { get; set; }
    }
}
