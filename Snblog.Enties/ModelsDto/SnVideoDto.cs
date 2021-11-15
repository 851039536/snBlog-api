using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnVideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Url { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual SnVideoType Type { get; set; }
        public virtual SnUser User { get; set; }
    }
}
