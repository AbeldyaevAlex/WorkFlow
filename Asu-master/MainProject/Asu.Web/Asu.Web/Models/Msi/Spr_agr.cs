using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_agr
    {
        public long Id { get; set; }
        public string gr_konstr { get; set; }
        public string agrk_k { get; set; }
        public string agrk_p { get; set; }
        public long? link_agrgr { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}