using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfTheListOfDocumentsViewModel
    {
        public long Id { get; set; }
        public long? link_prim_dse { get; set; }
        public string docum { get; set; }
        public DateTime? date_izm_docum { get; set; }
        public long? link_razd { get; set; }
        public string prim { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}