using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_rasc_vert : BaseEntity
    {
        public int RascexPoln { get; set; }

        public decimal? Npp { get; set; }

        public int CexId { get; set; }

        public int CexPriznId { get; set; }

        public bool? PrCexOsn { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PerioCloseDate { get; set; }

        public int CexPotrId { get; set; }

        public int PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
