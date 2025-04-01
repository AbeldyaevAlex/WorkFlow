using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class SprOts : BaseEntity
    {
        public int? KodSklad { get; set; }

        public string Per { get; set; }

        public int? Ots { get; set; }

        public string Nomer_Sklad { get; set; }

        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int PviId { get; set; }



        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
