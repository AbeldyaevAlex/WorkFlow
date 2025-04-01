using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial interface IRodPoverkService
    {
        List<Rod_poverk> GetRodPoverk();
        List<int> GetRodPoverkId(string rodPoverk);
        IList<Rod_poverk> GetRodPoverkList();
    }
}
