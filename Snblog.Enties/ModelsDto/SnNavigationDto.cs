using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnNavigationDto
    {
        public int NavId { get; set; }
        public string NavTitle { get; set; }
        public string NavText { get; set; }
        public string NavImg { get; set; }
        public string NavType { get; set; }
        public string NavUrl { get; set; }
    }
}
