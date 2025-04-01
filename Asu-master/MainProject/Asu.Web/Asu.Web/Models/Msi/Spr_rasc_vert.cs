using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_rasc_vert
    {
        public long Id { get; set; }
        public long? link_rascex_poln { get; set; }
        public decimal? npp { get; set; }
        public int? link_cex { get; set; }
        public long? link_cex_prizn { get; set; }
        public bool? pr_cex_osn { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_cex_potr { get; set; }
        public int? link_pvi { get; set; }
    }
}