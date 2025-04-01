using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models.Msi
{
    public class DocumentStatus
    {
        public int Id { get; set; }
        public string status { get; set; }
        public int? link_pvi { get; set; }
    }
}