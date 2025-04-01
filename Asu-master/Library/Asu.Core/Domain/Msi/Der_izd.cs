using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Der_izd : BaseEntity
    {
        public long? RodId { get; set; }

        public int? Dlin { get; set; }

        public int? Sort { get; set; }

        public int? KtsId { get; set; }

        public string kts { get; set; }

        public string VarKTS { get; set; }

        public int? pkpKtsId { get; set; }

        public string pkpKTS { get; set; }

        public int? OboznId { get; set; }

        public string Obozn { get; set; }

        public string VarObozn { get; set; }

        public int? pkpOboznId { get; set; }

        public string pkpObozn { get; set; }

        public string Ss { get; set; }

        public string Spo { get; set; }

        public int? ksb { get; set; }

        public int?  kol_rod_sbork { get; set; }

        public int? Kizd { get; set; }

        public int? NaimDetId { get; set; }

        public string naim_det { get; set; }

        public string tk1 { get; set; }

        public string tk2 { get; set; }

        public string tk3 { get; set; }

        public int? kp1 { get; set; }

        public int? kp2 { get; set; }

        public int? kp3 { get; set; }

        public decimal? Mas1sh { get; set; }

        public decimal? Masizd { get; set; }

        public string Vhodimost { get; set; }

        public string Vhodim_str { get; set; }

        public string Vhodim_rod { get; set; }

        public int? PolnRascexId { get; set; }

        public string Rascex_poln { get; set; }

        public string n_list { get; set; }

        public string n_poz { get; set; }

        public int PviId { get; set; }

        public int PerizdId { get; set; }

        public string NmIzd { get; set; }

        public int? SpicifId { get; set; }

        public int kdanId { get; set; }

        public string KodDan { get; set; }

        public string PrimKonstruktor { get; set; }

        public string PrimTehnolog { get; set; }

        public string PrimPrinadlegn { get; set; }

        public string PrimIzmenChast { get; set; }

        public int? RazdizdId { get; set; }

        public int? GrPrimId { get; set; }

        public int GrRazdIzdId { get; set; }

        public int? AgrId { get; set; }

        public string AgrObozn { get; set; }

        public int? GrAgrId { get; set; }

        public string GrupAgr { get; set; }

        public int? KomplektId { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string status { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_agr Spr_agr { get; set; }

        public virtual Spr_Perizd Spr_Perizd { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
