using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core
{
    public interface IDbHelper
    {
        T CallStoredProcedure<T>(string name, params object[] parameters);
    }
}
