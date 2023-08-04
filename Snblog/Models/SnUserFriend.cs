using System;
using System.Collections.Generic;

namespace Snblog.Models;

public partial class SnUserFriend
{
    public int Id { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// 好友id
    /// </summary>
    public int? UserFriendsId { get; set; }

    /// <summary>
    /// 好友备注
    /// </summary>
    public string UserNote { get; set; }

    /// <summary>
    /// 好友状态
    /// </summary>
    public string UserStatus { get; set; }
}
