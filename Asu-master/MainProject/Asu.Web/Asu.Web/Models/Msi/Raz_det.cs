using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Raz_det
    {
        public long Id { get; set; }
        public string razd { get; set; }
        public string naim_razd { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? sort { get; set; }
        public int? link_pvi { get; set; }
    }
}