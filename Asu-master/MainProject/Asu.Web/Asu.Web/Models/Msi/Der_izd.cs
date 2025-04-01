using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Der_izd
    {
        public long Id { get; set; }
        public long? link_izd { get; set; }
        public long? link_rod { get; set; }
        public int? dlin { get; set; }
        public int? sort { get; set; }
        public long? link_komplekt { get; set; }
        public long? link_kts { get; set; }
        public long? link_obozn { get; set; }
        public int? kol { get; set; }
        public string var { get; set; }
        public long? link_agr { get; set; }
        public long? link_razdizd { get; set; }
        public long? link_grrazdizd { get; set; }
        public string ss { get; set; }
        public string spo { get; set; }
        public string vhodim { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public int? link_pvi { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}