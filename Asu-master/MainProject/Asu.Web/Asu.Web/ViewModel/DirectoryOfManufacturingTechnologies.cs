using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfManufacturingTechnologies
    {
        public long Id { get; set; }
        public string nm_GR_tematik_p { get; set; }
        public string nm_GR_tematik_k { get; set; }
        public string prim { get; set; }
        public int? link_status { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
    }
}