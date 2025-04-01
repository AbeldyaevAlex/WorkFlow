using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class StandartSortingViewModel
    {
        public long Id { get; set; }
        public long? link_obozn { get; set; }
        public string kt_ { get; set; }
        public int? dlt_ { get; set; }
        public string tip_ { get; set; }
        public int? dl_1 { get; set; }
        public string tr_1 { get; set; }
        public int? dl_2 { get; set; }
        public string tr_2 { get; set; }
        public int? dl_3 { get; set; }
        public string tr_3 { get; set; }
        public int? dl_4 { get; set; }
        public string tr_4 { get; set; }
        public int? dl_5 { get; set; }
        public string tr_5 { get; set; }
        public int? dl_6 { get; set; }
        public string tr_6 { get; set; }
        public int? dl_7 { get; set; }
        public string tr_7 { get; set; }
        public int? link_status { get; set; }
        public string sort { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}