using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Work
{
    public partial class DirectoryOfTypesOfWork : BaseEntity
    {
        public string ShorName { get; set; }

        public string FullName { get; set; }

        public string Prim { get; set; }

        public int SprCexId { get; set; }

        public int SprPviId { get; set; }

        public int CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_cex Spr_cex { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
