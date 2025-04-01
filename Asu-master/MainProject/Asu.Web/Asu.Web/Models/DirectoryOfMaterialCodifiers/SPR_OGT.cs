using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.DirectoryOfMaterialCodifiers
{
    public class SPR_OGT
    {
        public long Id { get; set; }
        public string naim_ogt { get; set; }
        public int? OGT { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_gr_mater { get; set; }
        public long? link_prkm { get; set; }
        public long? link_sort { get; set; }
        public int? ksim_km { get; set; }
        public int? link_pvi { get; set; }
    }
}