using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_rascex_poln : BaseEntity
    {
        public int RascexId { get; set; }

        public int? CI11 { get; set; }

        public int? CP1 { get; set; }

        public int? CI12 { get; set; }

        public int? CP2 { get; set; }

        public int? CI13 { get; set; }

        public int? CP3 { get; set; }

        public int? CI2 { get; set; }

        public int? CI3 { get; set; }

        public int? CI4 { get; set; }

        public int? CI5 { get; set; }

        public int? CI6 { get; set; }

        public int? CI7 { get; set; }

        public int? CTO { get; set; }

        public int? CPK1 { get; set; }

        public int? CPK2 { get; set; }

        public int? CPK3 { get; set; }

        public int? CPK4 { get; set; }

        public int? CUS1 { get; set; }

        public int? CUS2 { get; set; }

        public int? CUS3 { get; set; }

        public int? CUS4 { get; set; }

        public int? CUS5 { get; set; }

        public int? CUS6 { get; set; }

        public int? CUS7 { get; set; }

        public int CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public DateTime? OperationDate { get; set; }

        public int? CI8 { get; set; }

        public int? CI9 { get; set; }

        public int? CI10 { get; set; }

        public int? CUS8 { get; set; }

        public int? CUS9 { get; set; }

        public int? CUS10 { get; set; }

        public string RascexSmall { get; set; }

        public int PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
