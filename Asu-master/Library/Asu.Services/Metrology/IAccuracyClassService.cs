using Asu.Core.Domain.Metrology;
using Asu.Core.Domain.Msi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial interface IAccuracyClassService
    {
        List<Spr_klass_tochn> GetAllAccuracyClass();
    }
}
