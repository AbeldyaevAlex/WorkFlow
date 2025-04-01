using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_pvi
    {
        public int Id { get; set; }
        public string pvi { get; set; }
        public string naim_pvi { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public DateTime? operation_date { get; set; }
    }
}