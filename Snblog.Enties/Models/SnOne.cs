﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnOne
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
        public int Read { get; set; }
        public int Give { get; set; }
        public int UserId { get; set; }
        public uint CommentId { get; set; }
        public int TypeId { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual SnOneType Type { get; set; }
        public virtual User User { get; set; }
    }
}
