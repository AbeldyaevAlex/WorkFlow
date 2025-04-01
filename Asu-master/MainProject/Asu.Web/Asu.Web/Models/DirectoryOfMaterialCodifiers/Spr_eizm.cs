using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.DirectoryOfMaterialCodifiers
{
    public class Spr_eizm
    {
        public long Id { get; set; }
        public string krat_naim_eizm { get; set; }
        public string poln_naim_eizm { get; set; }
        public int? link_status { get; set; }
        public string link_user { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public int? link_pvi { get; set; }
    }
}