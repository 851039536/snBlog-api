using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnTalk
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public string Text { get; set; }
        public int Read { get; set; }
        public int Give { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual SnTalkType Type { get; set; }
        public virtual SnUser User { get; set; }
    }
}
