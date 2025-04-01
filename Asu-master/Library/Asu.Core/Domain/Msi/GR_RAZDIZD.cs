using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class GR_RAZDIZD : BaseEntity
    {
        public int IzdId { get; set; }

        public int RazdIzdId { get; set; }

        public string Shifr { get; set; }

        public string NmGrup { get; set; }

        public int GolSborkId { get; set; }

        public int ZakazId { get; set; }

        public int DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
