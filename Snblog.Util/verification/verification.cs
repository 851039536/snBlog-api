namespace Snblog.Util.verification
{
    public static class Verification
    {
        /// <summary>
        /// 验证是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }
    }
}
