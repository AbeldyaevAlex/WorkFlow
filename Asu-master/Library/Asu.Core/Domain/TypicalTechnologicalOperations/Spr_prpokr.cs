using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;


namespace Asu.Core.Domain.TypicalTechnologicalOperations
{
    public partial class Spr_prpokr : BaseEntity
    {
        public string Prpokr { get; set; }

        public string NmPrpokr { get; set; }

        public int DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int Spr_pviId { get; set; }



        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}
