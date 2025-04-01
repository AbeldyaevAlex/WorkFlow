using Asu.Data.Mapping;
using Asu.Core.Domain.Shipping;

namespace Asu.Data.Mapping.Shipping
{
    public partial class ZipPrefixMap : NopEntityTypeConfiguration<ZipPrefix>
    {
        public ZipPrefixMap()
        {
            this.ToTable("WCS_ZipPrefix");

            //Map the primary key
            //HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.Prefix).HasMaxLength(255);
            Property(m => m.State).HasMaxLength(255);
        }
    }
}
