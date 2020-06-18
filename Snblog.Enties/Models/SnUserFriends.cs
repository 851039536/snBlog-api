using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnUserFriends
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? UserFriendsId { get; set; }
        public string UserNote { get; set; }
        public string UserStatus { get; set; }
    }
}
