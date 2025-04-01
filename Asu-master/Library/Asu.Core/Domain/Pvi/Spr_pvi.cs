using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using System;


namespace Asu.Core.Domain.Pvi
{
    public partial class Spr_pvi : BaseEntity
    {
        public string Pvi { get; set; }

        public int PviLevelId { get; set; }

        public string NaimPvi { get; set; }

        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public DateTime? OperationDate { get; set; }



        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }

        public PviLevel PviLevel
        {
            get
            {
                return (PviLevel)this.PviLevelId;
            }
            set
            {
                this.PviLevelId = (int)value;
            }
        }
    }
}
