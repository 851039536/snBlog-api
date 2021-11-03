using System;
using System.Collections.Generic;

#nullable disable

namespace Snblog.Enties.Models
{
    public partial class SnUserFriend
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? UserFriendsId { get; set; }
        public string UserNote { get; set; }
        public string UserStatus { get; set; }
    }
}
