using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class Spr_mash
    {
        public long Id { get; set; }
        public string nom_mash { get; set; }
        public string naim_mash { get; set; }
        public int? link_izd { get; set; }
        public string ser_s { get; set; }
        public string ser_po { get; set; }
        public long? link_zakaz { get; set; }
        public int? kol_kompl { get; set; }
        public int? shag { get; set; }
        public int? gr_kol { get; set; }
        public int? pr_zrasc { get; set; }
        public int? pr_kompl { get; set; }
        public string ud_ss { get; set; }
        public string ud_spo { get; set; }
        public string soot_s { get; set; }
        public string soot_po { get; set; }
        public string soot_ud_ss { get; set; }
        public string soot_ud_spo { get; set; }
        public string kompl1 { get; set; }
        public string kompl2 { get; set; }
        public string sort { get; set; }
        public string rspo { get; set; }
        public int? kmash { get; set; }
        public string kod_o { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}