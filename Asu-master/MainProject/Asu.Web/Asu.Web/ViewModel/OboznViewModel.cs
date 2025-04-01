using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Asu.Web.Models;

namespace Asu.Web.ViewModel
{
    public class OboznViewModel
    {
        public long Id { get; set; }
        public string obozn { get; set; }
        public string var { get; set; }
        public long? link_pkp { get; set; }
        public long? link_naim { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string obozn_p { get; set; }
        public string obozn_dos { get; set; }
        public string stsort_kt { get; set; }
        public string stsort_tip { get; set; }
        public string stsort_tr_1 { get; set; }
        public string stsort_tr_2 { get; set; }
        public string stsort_tr_3 { get; set; }
        public string stsort_tr_4 { get; set; }
        public string stsort_tr_5 { get; set; }
        public string stsort_tr_6 { get; set; }
        public string stsort_tr_7 { get; set; }
        public string per_izd { get; set; }
        public int? link_pvi { get; set; }
    }
}