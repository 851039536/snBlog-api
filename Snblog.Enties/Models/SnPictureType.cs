using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snblog.Models
{
    public partial class SnPictureType
    {
        public int Id { get; set; }
        public int? PictureTypeId { get; set; }
        public string PictureTypeName { get; set; }
    }
}
