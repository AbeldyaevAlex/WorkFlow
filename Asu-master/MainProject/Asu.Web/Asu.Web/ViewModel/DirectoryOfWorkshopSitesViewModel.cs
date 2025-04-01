using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class DirectoryOfWorkshopSitesViewModel
    {
        public int Id { get; set; }
        public int? link_cex { get; set; }
        public string nom_uch { get; set; }
        public string naim_uch { get; set; }
        public int? link_status { get; set; }
        public int? link_user { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}