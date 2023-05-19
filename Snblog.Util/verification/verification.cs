namespace Snblog.Util.verification
{
    public static class Verification
    {
        public static bool IsNotNull(string obj)
        {
            if (string.IsNullOrEmpty(obj)) {
                return false;
            };
            return true;
        }
    }
}
