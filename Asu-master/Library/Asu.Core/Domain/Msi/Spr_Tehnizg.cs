using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_Tehnizg : BaseEntity
    {
        public string Tehn_izg_k { get; set; }

        public string Tehn_izg_p { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public string Prim { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public int? PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
