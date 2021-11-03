using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnOneType
    {
        public SnOneType()
        {
            SnOnes = new HashSet<SnOne>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<SnOne> SnOnes { get; set; }
    }
}
