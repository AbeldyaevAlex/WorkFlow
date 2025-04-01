using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_mash : BaseEntity
    {
        public string NomMash { get; set; }

        public string NaimMash { get; set; }

        public int IzdId { get; set; }

        public string Ser_s { get; set; }

        public string Ser_po { get; set; }

        public int ZakazId { get; set; }

        public int? KolKompl { get; set; }

        public int? Shag { get; set; }

        public int? GrKol { get; set; }

        public int? PrZrasc { get; set; }

        public int? PrKompl { get; set; }

        public string Ud_ss { get; set; }

        public string Ud_spo { get; set; }

        public string Soot_s { get; set; }

        public string Soot_po { get; set; }

        public string Soot_ud_ss { get; set; }

        public string Soot_ud_spo { get; set; }

        public string Kompl1 { get; set; }

        public string Kompl2 { get; set; }

        public string Sort { get; set; }

        public string Rspo { get; set; }

        public int? Kmash { get; set; }

        public string Kod_o { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int PviId { get; set; }

        public bool exception { get; set; }
        



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
