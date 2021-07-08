using System;
using System.Collections.Generic;

namespace Snblog.Models
{
    public partial class SnUser
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public int UserId { get; set; }
        public string UserIp { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPwd { get; set; }
        public string UserPhoto { get; set; }
        public string UserTime { get; set; }
        public string UserNickname { get; set; }
        public string UserBrief { get; set; }

    }
}
