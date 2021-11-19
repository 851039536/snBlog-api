using System.Collections.Generic;

namespace Snblog.Util.components
{
    public class Res<T>
    {
        public T entity = default;
        public List<T> entityList = default;
        public int entityInt;
    }
    public class ResDto<T>
    {
        public T entity = default;
        public List<T> entityList = default;
        public string[] name = default;
    }


}
