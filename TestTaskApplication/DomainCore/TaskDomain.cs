using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DomainCore
{
    public class TaskDomain
    {
        public TaskDomain()
        {
            BrigadeList = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }
        public string Brigade { get; set; }
        public string FullCustomerName { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public IList<SelectListItem> BrigadeList { get; set; }
        public IList<SelectListItem> StatusList { get; set; }
    }
}
