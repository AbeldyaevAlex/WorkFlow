using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_Perizd
    {
        public int Id { get; set; }
        public string izdelie { get; set; }
        public string kod_izd { get; set; }
        public string nm_izd { get; set; }
        public string ser_ss { get; set; }
        public string ser_spo { get; set; }
        public string kgk1_1 { get; set; }
        public string kgk1_n { get; set; }
        public string kgk1_m { get; set; }
        public string tek_ser_s { get; set; }
        public string tek_ser_po { get; set; }
        public long? link_tema { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public long? Model_tehnol_podgot_proizv { get; set; }
        public string prim { get; set; }
        public long? link_tematik { get; set; }
        public string pr_komplektov { get; set; }
        public string soot_ss { get; set; }
        public string soot_spo { get; set; }
        public long? link_zakaz { get; set; }
        public bool IsActive { get; set; }
        public int? link_pvi { get; set; }
    }
}