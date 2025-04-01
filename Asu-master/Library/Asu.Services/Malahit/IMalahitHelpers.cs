using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Asu.Mapping.Malahit
{
    public partial interface IMalahitHelpers
    {
        void SetMeorandumIdToCookies(string zakaz, DateTime startDate, DateTime endDate);

        bool GetMeorandumFromCookies(string zakaz, DateTime startDate, DateTime endDate);


    }
}
