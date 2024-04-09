using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models;

public partial class SnComment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Give { get; set; }
    public string Text { get; set; }
    public DateTime TimeCreate { get; set; }
    public DateTime TimeModified { get; set; }
}