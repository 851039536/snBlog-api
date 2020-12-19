using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnTalkType
    {
        public int Id { get; set; }
        public int? TalkId { get; set; }
        public string Type { get; set; }
    }
}
