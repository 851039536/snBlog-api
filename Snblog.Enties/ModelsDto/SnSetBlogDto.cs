using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnSetblogDto
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
