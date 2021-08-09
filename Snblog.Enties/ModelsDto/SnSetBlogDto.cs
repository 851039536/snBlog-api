using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Models
{
    public partial class SnSetBlogDto
    {
        public int Id { get; set; }
        public string SetName { get; set; }
        public int UserId { get; set; }
        public bool SetIsopen { get; set; }
        public string SetUrl { get; set; }
        public sbyte SetType { get; set; }
    }
}
