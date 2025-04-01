using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfDseViewModel
    {
        public long Id { get; set; }
        public long? link_pkp { get; set; }
        public long? link_obozn { get; set; }
        public int? link_cex { get; set; }
        public long? link_km { get; set; }
        public long? link_nm_metodic { get; set; }
        public long? link_prkm { get; set; }
        public decimal? bz { get; set; }
        public decimal? lz { get; set; }
        public decimal? nr { get; set; }
        public int? vpost { get; set; }
        public decimal? nrvp { get; set; }
        public long? link_primdse { get; set; }
        public int? vpost_sh { get; set; }
        public int? krat { get; set; }
        public string ndoc { get; set; }
        public long? link_tema { get; set; }
        public long? link_rascex { get; set; }
        public string ree { get; set; }
        public bool? pr_got_ci { get; set; }
        public bool? pr_othod { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string prascr { get; set; }
        public long? tehn_izgot { get; set; }
    }
}