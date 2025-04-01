using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Msi
{
    public partial class Spr_Perizd : BaseEntity
    {
        public string Izdelie { get; set; }

        public string KodIzd { get; set; }

        public string NmIzd { get; set; }

        public string Ser_ss { get; set; }

        public string Ser_spo { get; set; }

        public string Kgk1_1 { get; set; }

        public string Kgk1_n { get; set; }

        public string Kgk1_m { get; set; }

        public string Tek_ser_s { get; set; }

        public string Tek_ser_po { get; set; }

        public int TemaId { get; set; }

        public DateTime? period_open_date { get; set; }

        public DateTime? period_close_date { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public long? Model_tehnol_podgot_proizv { get; set; }

        public string Prim { get; set; }

        public int TematikId { get; set; }

        public string PrKomplektov { get; set; }

        public string Soot_ss { get; set; }

        public string Soot_spo { get; set; }

        public int ZakazId { get; set; }

        public bool IsActive { get; set; }

        public int PviId { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
    }
}
