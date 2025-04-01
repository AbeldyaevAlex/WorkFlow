using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfMaterialPriceViewModel
    {
        public long Id { get; set; }
        public long? link_SKM { get; set; }
        public decimal? cmat { get; set; }
        public long? link_Valuta { get; set; }
        public string god_prim_cen { get; set; }
        public long? link_Obosnov { get; set; }
        public long? link_Predpr { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}