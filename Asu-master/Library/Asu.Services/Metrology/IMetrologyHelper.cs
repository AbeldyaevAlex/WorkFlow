using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Metrology
{
    public partial interface IMetrologyHelper
    {
        void SetParametrRodPoverkToCookies(string NmRodPoverk);
        void SetWorkshopIdToCookies(int WorkshopId);
        void SetWorkshopToCookies(int WorkshopId);

    }
}
