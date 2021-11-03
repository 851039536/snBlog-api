using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnSetBlogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Isopen { get; set; }
        public string Url { get; set; }
        public sbyte Type { get; set; }
    }
}
