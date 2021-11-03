using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnTalkType
    {
        public SnTalkType()
        {
            SnTalks = new HashSet<SnTalk>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SnTalk> SnTalks { get; set; }
    }
}
