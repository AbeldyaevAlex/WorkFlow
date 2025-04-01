using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;


namespace Asu.Core.Domain.TypicalTechnologicalOperations
{
    public partial class Spr_tto : BaseEntity
    {
        public int KodTTOId { get; set; }

        public int KodKompId { get; set; }

        public int CizgId { get; set; }

        public int PrkmId { get; set; }

        public int PrpokrId { get; set; }

        public decimal? Nrm { get; set; }

        public int? Vpost { get; set; }

        public decimal? Nrvp { get; set; }

        public int? Krat { get; set; }

        public int? VpostSh { get; set; }

        public int? SortKodTTO { get; set; }

        public int? SortKodKomp { get; set; }

        public int DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int Spr_pviId { get; set; }




        public virtual Spr_prpokr Spr_prpokr { get; set; }

        public virtual SprSkm SprSkm { get; set; }

        public virtual SprPrKm SprPrKm { get; set; }

        public virtual Spr_cex Spr_cex { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}
