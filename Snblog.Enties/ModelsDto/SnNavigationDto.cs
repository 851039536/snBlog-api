using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.ModelsDto
{
    public partial class SnNavigationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Describe { get; set; }
        public string Img { get; set; }
        public int TypeId { get; set; }
        public string Url { get; set; }

        public virtual SnInterfaceType Type { get; set; }
    }
}
