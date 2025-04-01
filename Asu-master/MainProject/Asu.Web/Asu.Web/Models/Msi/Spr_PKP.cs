using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_PKP
    {
        public long Id { get; set; }
        public string pkp { get; set; }
        public string nm_pkp { get; set; }
        public long? link_razd { get; set; }
        public long? link_razd_dse { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string ImageFileName { get; set; }
        public string pkp_dos { get; set; }
        public int? link_pvi { get; set; }
    }
}