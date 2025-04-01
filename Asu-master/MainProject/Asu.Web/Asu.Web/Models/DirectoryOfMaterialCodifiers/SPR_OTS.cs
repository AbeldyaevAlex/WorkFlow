using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.DirectoryOfMaterialCodifiers
{
    public class SPR_OTS
    {
        public long Id { get; set; }
        public int? kod_sklad { get; set; }
        public string per { get; set; }
        public int? ots { get; set; }
        public string nomer_sklad { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_pvi { get; set; }
    }
}