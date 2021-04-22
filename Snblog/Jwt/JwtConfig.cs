﻿namespace Snblog.Jwt
{
    public class JwtConfig
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 所属者
        /// </summary>
        public string Issuer { get; set; }
 
        public string Audience { get; set; }
 
        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expiration { get; set; }
    }
}
