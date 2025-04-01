using Asu.Core.Domain.Metrology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial interface IKonservService
    {
        IList<Konserv> GetKonservList();
    }
}
