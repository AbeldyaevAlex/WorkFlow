using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class SprOgt : BaseEntity
    {
        public string NaimOgt { get; set; }

        public int? OGT { get; set; }

        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int GrMaterId { get; set; }

        public int PrkmId { get; set; }

        public int SortMaterId { get; set; }

        public int KsimKm { get; set; }

        public int Spr_pviId { get; set; }

        public int SortamMaterId { get; set; }


        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual SprGrMater SprGrMater { get; set; }

        public virtual SortMater SortMater { get; set; }

        public virtual SprPrKm SprPrKm { get; set; }

        public virtual SprSortam SprSortam { get; set; }
    }
}
