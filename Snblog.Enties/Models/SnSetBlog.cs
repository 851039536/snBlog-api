﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnSetblog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RouterUrl { get; set; }
        public bool Isopen { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }

        public virtual SnSetblogType Type { get; set; }
        public virtual SnUser User { get; set; }
    }
}
