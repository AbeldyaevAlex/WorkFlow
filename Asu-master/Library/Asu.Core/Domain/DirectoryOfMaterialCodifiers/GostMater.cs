using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class GostMater : BaseEntity
    {
        
        public string Gost { get; set; }
        [UIHint("TypeEditor")]
        public int StatusDocumentId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }
        [DisplayName("PeriodOpenDate")]
        [DataType(DataType.Date)]
        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int Spr_pviId { get; set; }



        public virtual Spr_pvi Spr_pvi { get; set; }
        [UIHint("TypeEditor")]
        public virtual DocumentStatus StatusDocument { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
