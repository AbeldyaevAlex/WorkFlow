using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class MemorandumBase : BaseEntity
    {
        public int NoMemorandumLine { get; set; }

        public string Description { get; set; }

        public string CommentForMsz { get; set; }

        public bool AtWork { get; set; }

        public int? DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }


        //public virtual DocumentStatus DocumentStatus { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
