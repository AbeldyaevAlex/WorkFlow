using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Msi
{
    public partial interface ISprPerizdService
    {
        List<Spr_Perizd> GetAllistIzd();
    }
}
