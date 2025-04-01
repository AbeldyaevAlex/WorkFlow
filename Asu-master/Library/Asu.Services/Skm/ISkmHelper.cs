using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Mapping.Skm
{
    public partial interface ISkmHelper
    {
        void SetNmSkmIdToCookies(string listNmSkm);
        void SetMarkaSkmIdToCookies(string listMarkaSkm);
        void SetGostSkmIdToCookies(string listGost);
        void SetOgtIdToCookies(int OgtId);
        void SetNaimSkmToCookies(string NmSkm);
        void RemoveNmSkmToCookies();
        void ClearSkmCookies();
        void SetMarkaSkmIdToCookiesAfterChange(string markaskm);
        void SetGostSkmIdToCookiesAfterChange(string markaskm);
    }
}
