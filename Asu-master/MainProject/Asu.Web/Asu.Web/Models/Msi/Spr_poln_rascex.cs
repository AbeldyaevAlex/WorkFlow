using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_poln_rascex
    {
        public long Id { get; set; }
        public long? link_rascizd  { get; set; }
        public int? link_cp1 { get; set; }
        public int? link_cp2 { get; set; }
        public int? link_cp3 { get; set; }
        public string rascex { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}