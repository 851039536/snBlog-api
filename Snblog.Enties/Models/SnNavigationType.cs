using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnNavigationType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NavType { get; set; }
    }
}
