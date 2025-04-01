using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class SpecificationReferenceViewModel
    {
        public long Id { get; set; }
        public long? link_kts { get; set; }
        public long? link_obozn { get; set; }
        public string T_TV { get; set; }
        public int? link_kdan { get; set; }
        public long? link_razdizd { get; set; }
        public int? ksb { get; set; }
        public long? link_komplekt { get; set; }
        public string prim_texn { get; set; }
        public string prim_konstrukt { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string sort { get; set; }
        public string prim_kts { get; set; }
    }
}