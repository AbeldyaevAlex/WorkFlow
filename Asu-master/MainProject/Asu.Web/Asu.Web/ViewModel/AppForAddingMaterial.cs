using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class AppForAddingMaterial
    {
        public long Id { get; set; }
        public string kod_TTO { get; set; }
        public string kod_komp { get; set; }
        public string cizg { get; set; }
        public string prkm { get; set; }
        public string prpokr { get; set; }
        public decimal? nrm { get; set; }
        public int? vpost { get; set; }
        public decimal? nrvp { get; set; }
        public int? krat { get; set; }
        public int? vpost_sh { get; set; }
        public string prim { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}