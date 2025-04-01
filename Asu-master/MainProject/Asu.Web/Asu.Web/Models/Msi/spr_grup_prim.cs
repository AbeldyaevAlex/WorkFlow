using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class spr_grup_prim
    {
        public int Id { get; set; }
        public string nm_grprim { get; set; }
        public string nm_grprim_k { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_cex_real { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public int? link_status { get; set; }
        public int? link_pvi { get; set; }
    }
}