using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnInterfaceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public bool Identity { get; set; }

        public virtual SnInterfaceType Type { get; set; }
        public virtual SnUser User { get; set; }
    }
}
