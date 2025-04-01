using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class TrudObViewModel
    {
        public long Id { get; set; }
        public int? link_izd { get; set; }
        public long? link_pkp { get; set; }
        public int? link_cex { get; set; }
        public long? link_obozn { get; set; }
        public int? uchastok { get; set; }
        public int? vidrabot { get; set; }
        public decimal? tr_sdel { get; set; }
        public decimal? rascen { get; set; }
        public decimal? tr_povr { get; set; }
        public string ndok { get; set; }
        public string vidrab_izg_usl { get; set; }
        public string vidrabi { get; set; }
        public string prim { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}