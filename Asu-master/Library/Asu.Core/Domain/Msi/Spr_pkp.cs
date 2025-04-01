using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;


namespace Asu.Core.Domain.Msi
{
    public partial class Spr_pkp : BaseEntity
    {
        public string Pkp { get; set; }

        public string NmPkp { get; set; }

        public int RazdId { get; set; }

        public int RazdDse { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public string ImageFileName { get; set; }

        public string PkpDos { get; set; }

        public int? PviId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }
        //public PviLevel PviLevel
        //{
        //    get
        //    {
        //        return (PviLevel)this.link_pvi;
        //    }
        //    set
        //    {
        //        this.link_pvi = (int)value;
        //    }
        //}
    }
}
