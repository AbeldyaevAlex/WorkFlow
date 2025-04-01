using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_prim_dse
    {
        public long Id { get; set; }
        public long? link_poln_rascex { get; set; }
        public int? link_izd { get; set; }
        public long? link_grrazdizd { get; set; }
        public long? link_grprim { get; set; }
        public long? link_specif { get; set; }
        public string ss { get; set; }
        public string spo { get; set; }
        public string n_list { get; set; }
        public string n_poz { get; set; }
        public int? kizd { get; set; }
        public int? kp1 { get; set; }
        public int? kp2 { get; set; }
        public int? kp3 { get; set; }
        public string tk1 { get; set; }
        public string tk2 { get; set; }
        public string tk3 { get; set; }
        public decimal? masizd { get; set; }
        public long? link_kts { get; set; }
        public long? link_obozn { get; set; }
        public long? link_obozn_mater { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
        public string prim_konstruktor { get; set; }
        public string prim_tehnol { get; set; }
        public string prim_prinadlegn { get; set; }
        public string prim_izmen_chast { get; set; }
    }
}