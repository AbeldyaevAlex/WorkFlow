using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_specif : BaseEntity
    {
        public int KtsId { get; set; }

        public int OboznId { get; set; }

        public int PkpTTVId { get; set; }

        public int KdanId { get; set; }

        public int RazdDetId { get; set; }

        public int? Ksb { get; set; }

        public int KomplektId { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int SpecId { get; set; }

        public int SvyzPrimId { get; set; }

        public int PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
