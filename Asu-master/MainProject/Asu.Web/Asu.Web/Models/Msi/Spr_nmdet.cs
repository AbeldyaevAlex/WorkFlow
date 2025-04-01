using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_nmdet
    {
        public long Id { get; set; }
        public string naim_det { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public int? link_pvi { get; set; }
    }
}