using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_obozn : BaseEntity
    {
        public string Obozn { get; set; }

        public string Var { get; set; }

        public int PkpId { get; set; }

        public int NaimId { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public string Obozn_p { get; set; }

        public string Obozn_dos { get; set; }

        public string Stsort_kt { get; set; }

        public string Stsort_tip { get; set; }

        public string Stsort_tr_1 { get; set; }

        public string Stsort_tr_2 { get; set; }

        public string Stsort_tr_3 { get; set; }

        public string Stsort_tr_4 { get; set; }

        public string Stsort_tr_5 { get; set; }

        public string Stsort_tr_6 { get; set; }

        public string Stsort_tr_7 { get; set; }

        public string PerIzd { get; set; }

        public int? PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
