using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public int link_nm_task { get; set; }
        public int? link_user { get; set; }
        public int? link_status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string operation { get; set; }
        public DateTime? operation_date { get; set; }
        public DateTime? period_open_date { get; set; }
        public DateTime? period_close_date { get; set; }
    }
}