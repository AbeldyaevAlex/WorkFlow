using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_Tehnizg
    {
        public long Id { get; set; }
        public string Tehn_izg_k { get; set; }
        public string Tehn_izg_p { get; set; }
        public int? link_status { get; set; }
        public DateTime? period_close_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public string prim { get; set; }
        public string operation { get; set; }
        public string link_user { get; set; }
        public int? link_pvi { get; set; }
    }
}