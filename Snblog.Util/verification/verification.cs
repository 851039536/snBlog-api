using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Util.verification
{
   public static class verification
    {
        public static bool IsNotNull(string obj)
        {
            if (obj =="" || obj == null) {
                return false;
            };
            return true;
        }
    }
}
