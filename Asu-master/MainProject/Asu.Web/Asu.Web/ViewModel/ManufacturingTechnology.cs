using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class ManufacturingTechnology
    {
        public long Id { get; set; }
        public string Tehn_izg_k { get; set; }
        public string Tehn_izg_p { get; set; }
        public int? Status_id { get; set; }
        public DateTime? data_o { get; set; }
        public DateTime? data_z { get; set; }
    }
}