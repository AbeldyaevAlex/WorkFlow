using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Metrology
{
    public class Spr_metrol
    {
        public int Id { get; set; }
        public int? link_podgrupp { get; set; }
        public int? link_group { get; set; }
        public int? link_naznach { get; set; }
        public int? link_cex { get; set; }
        public int? link_period_poverk { get; set; }
        public int? link_mesto_poverk { get; set; }
        public int? link_rod_poverk { get; set; }
        public int? link_tip_pribora { get; set; }
        public int? link_konserv { get; set; }
        public decimal? n_pasporta { get; set; }
        public string n_zavod { get; set; }
        public DateTime? data_pover { get; set; }
        public DateTime? data_pred_pov { get; set; }
        public int? link_usl { get; set; }
        public int? link_ree { get; set; }
        public int? link_stan { get; set; }
        public string god_vip { get; set; }
        public int? link_predpr { get; set; }
        public DateTime? data_izm { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}