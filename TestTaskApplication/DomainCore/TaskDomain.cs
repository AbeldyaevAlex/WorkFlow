using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCore
{
    public class TaskDomain
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public Nullable<System.DateTime> OpenDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> RoleId { get; set; }
    }
}
