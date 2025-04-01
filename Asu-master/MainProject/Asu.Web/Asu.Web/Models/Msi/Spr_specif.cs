using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_specif
    {
        public long Id { get; set; }
        public long? link_kts { get; set; }
        public long? link_obozn { get; set; }
        public long? link_pkp_T_TV { get; set; }
        public int? link_kdan { get; set; }
        public long? link_razd_det { get; set; }
        public int? ksb { get; set; }
        public long? link_komplekt { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public long? link_spec { get; set; }
        public long? link_svyz_prim { get; set; }
        public int? link_pvi { get; set; }
    }
}