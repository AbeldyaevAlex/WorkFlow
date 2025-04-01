using Asu.Core.Domain.Common;
using Asu.Core.Domain.Directory;

namespace Asu.Core.Domain.Orders
{
    public class CrmAddress : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public virtual CrmState State { get; set; }

        public int? StateId { get; set; }

        public string Zip { get; set; }

        public int CountryId { get; set; }

        public virtual CrmCountry Country { get; set; }
    }
}
