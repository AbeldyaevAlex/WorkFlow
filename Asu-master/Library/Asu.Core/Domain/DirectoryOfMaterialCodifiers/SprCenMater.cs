using System;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Directory;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class SprCenMater : BaseEntity
    {
        public int SkmId { get; set; }

        public decimal? Cmat { get; set; }

        public int? CurrencyId { get; set; }

        public string GodPrimCen { get; set; }

        public int ObosnovId { get; set; }

        public int PredprId { get; set; }

        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int Spr_pviId { get; set; }



        public virtual Currency Currency { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DokumObosnov DocumObosnov { get; set; }

        public virtual PredprPostav PredprPostav { get; set; }

    }
}
