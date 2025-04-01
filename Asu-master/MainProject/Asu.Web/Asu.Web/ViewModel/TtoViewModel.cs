using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class TtoViewModel
    {
        public long Id { get; set; }
        public long? link_kod_TTO { get; set; }
        public long? link_kod_komp { get; set; }
        public int? link_cizg { get; set; }
        public long? link_prkm { get; set; }
        public long? link_prpokr { get; set; }
        [Required(ErrorMessage = "nrm is required")]
        public decimal? nrm { get; set; }
        public int? vpost { get; set; }
        public decimal? nrvp { get; set; }
        public int? krat { get; set; }
        public int? vpost_sh { get; set; }
        public int? sort_kod_TTO { get; set; }
        public int? sort_kod_komp { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string km_tto { get; set; }
        public string km_komp { get; set; }
    }
}