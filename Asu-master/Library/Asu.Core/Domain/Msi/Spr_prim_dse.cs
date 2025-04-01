using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_prim_dse : BaseEntity
    {
        public int PolnRascexId { get; set; }

        public int IzdId { get; set; }

        public int GrRazdIzdId { get; set; }

        public int GrPrimId { get; set; }

        public int SpecifId { get; set; }

        public string Ss { get; set; }

        public string Spo { get; set; }

        public string n_list { get; set; }

        public string n_poz { get; set; }

        public int? Kizd { get; set; }

        public int? Kp1 { get; set; }

        public int? Kp2 { get; set; }

        public int? Kp3 { get; set; }

        public string Tk1 { get; set; }

        public string Tk2 { get; set; }

        public string Tk3 { get; set; }

        public decimal? Masizd { get; set; }

        public int KtsId { get; set; }

        public int OboznId { get; set; }

        public int LinkOboznMater { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int PviId { get; set; }

        public string PrimKonstruktor { get; set; }

        public string PrimTehnol { get; set; }

        public string PrimPrinadlegn { get; set; }

        public string PrimIzmenChast { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
