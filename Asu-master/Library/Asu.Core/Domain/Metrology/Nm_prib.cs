using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Metrology
{
    public partial class Nm_prib : BaseEntity
    {
        public string nm_prib1 { get; set; }

        public int CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string operation { get; set; }

        public DateTime? operation_date { get; set; }

        public DateTime? period_open_date { get; set; }

        public DateTime? period_close_date { get; set; }

        public int? link_pvi { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}
