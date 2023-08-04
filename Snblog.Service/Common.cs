namespace Snblog.Service
{
    /// <summary>
    /// 常量字符
    /// </summary>
    public static class Common
    {
        // 常量字符串。这些常量字符串可以在代码中多次使用，而不必担心它们的值会被修改。
        public const string Bid = "BYID_";
        public const string Sum = "SUM_";
        public const string Contains = "CONTAINS_";
        public const string Paging = "PAGING_";
        public const string All = "ALL_";
        public const string Del = "DEL_";
        public const string Add = "ADD_";
        public const string Up = "UP_";
        public const string UpPortiog = "UP_PORTIOG_";
        public const string Condition = "Condition";


        /// <summary>
        /// 输出log并对CacheKey赋值
        /// </summary>
        /// <param name="info"></param>
        public static string CacheInfo(string info)
        {
            CacheKey = info;
            Log.Information(CacheKey);
            return CacheKey;
        }

        /// <summary>
        /// 缓存Key
        /// </summary>
        public static string CacheKey;
    }
}   