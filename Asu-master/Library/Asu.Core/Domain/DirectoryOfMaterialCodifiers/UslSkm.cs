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
    public partial class UslSkm : BaseEntity
    {
        public int KolSimvol { get; set; }

        public string PerOgt { get; set; }

        public string UslSort { get; set; }

        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int Spr_pviId { get; set; }



        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
