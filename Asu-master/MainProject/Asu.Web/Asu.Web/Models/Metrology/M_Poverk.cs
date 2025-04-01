using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Metrology
{
    public class M_Poverk
    {
        public int Id { get; set; }
        public string mesto { get; set; }
        public string p_nm_mesto { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}