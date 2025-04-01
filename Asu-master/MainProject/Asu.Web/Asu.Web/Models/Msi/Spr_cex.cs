using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_cex
    {
        public int Id { get; set; }
        public string cex { get; set; }
        public string naim_cex { get; set; }
        public int? link_status { get; set; }
        public string nm_cex_krat { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_cex_real { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public int? link_pvi { get; set; }
    }
}