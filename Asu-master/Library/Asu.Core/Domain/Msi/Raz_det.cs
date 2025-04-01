using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Raz_det : BaseEntity
    {
        public string Razd { get; set; }

        public string NaimRazd { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int? Sort { get; set; }

        public int? PviId { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
