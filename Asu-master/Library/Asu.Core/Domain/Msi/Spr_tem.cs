using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_tem : BaseEntity
    {
        public string Nm_tem_p { get; set; }

        public string Nm_tem_k { get; set; }

        public string Prim { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public int? PviId { get; set; }

        //public List<SelectListItem> Theme { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
