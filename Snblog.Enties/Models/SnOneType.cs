using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnOneType
    {
        public int Id { get; set; }
        public int SoTypeId { get; set; }
        public string SoTypeTitle { get; set; }
    }
}
