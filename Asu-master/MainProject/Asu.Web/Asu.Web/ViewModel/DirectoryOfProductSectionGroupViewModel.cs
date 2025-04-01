using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfProductSectionGroupViewModel
    {
        public long Id { get; set; }
        public int? link_izd { get; set; }
        public long? link_razdizd { get; set; }
        public string shifr { get; set; }
        public string nm_grup { get; set; }
        public long? link_gol_sb { get; set; }
        public long? link_zakaz { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}