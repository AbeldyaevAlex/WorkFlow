using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Metrology
{
    public partial class Podgr_prib : BaseEntity
    {
        public int? link_vidiz { get; set; }

        public int? n_podgrupp { get; set; }

        public int? link_nmprib { get; set; }

        //public string FullNmPrib { get { return string.Format("{0} {1}", n_podgrupp, Nm_prib.nm_prib1); } }

        public int CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string operation { get; set; }

        public DateTime? operation_date { get; set; }

        public DateTime? period_open_date { get; set; }

        public DateTime? period_close_date { get; set; }

        public int? link_pvi { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Nm_prib Nm_prib { get; set; }

        public virtual Vid_izmer Vid_izmer { get; set; }
    }
}
