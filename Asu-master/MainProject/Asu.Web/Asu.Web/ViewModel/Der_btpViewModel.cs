using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class Der_btpViewModel
    {
        public long Id { get; set; }
        public long? link_rod { get; set; }
        public int? dlin { get; set; }
        public int? sort { get; set; }
        public long? link_kts { get; set; }
        public long? link_obozn { get; set; }
        public int? kol { get; set; }
        public string tk { get; set; }
        public long? link_agr { get; set; }
        public int? link_izd { get; set; }
        public string sz { get; set; }
        public string n_specif { get; set; }
        public string zakaz { get; set; }
        public string serii { get; set; }
        public string bull { get; set; }
        public int? nm_gr_sz { get; set; }
        public int? poz { get; set; }
        public int? list { get; set; }
        public string rascex { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}