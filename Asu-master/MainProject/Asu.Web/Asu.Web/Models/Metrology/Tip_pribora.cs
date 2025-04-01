using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Metrology
{
    public class Tip_pribora
    {
        public int Id { get; set; }
        public string tip_naim { get; set; }
        public int? link_predel { get; set; }
        public int? link_cena_del { get; set; }
        public int? link_klass_tochn { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}