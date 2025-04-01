using Asu.Data.Mapping;
using Asu.Core.Domain.Shipping;

namespace Asu.Data.Mapping.Shipping
{
    public partial class ZipCodeMap : NopEntityTypeConfiguration<ZipCode>
    {
        public ZipCodeMap()
        {
            this.ToTable("WCS_ZipCode");

            //Map the primary key
            //HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.ZIPCode).HasMaxLength(10).IsRequired();
            Property(m => m.Latitude).HasMaxLength(50).IsRequired();
            Property(m => m.Longitude).HasMaxLength(50).IsRequired();
            Property(m => m.City).HasMaxLength(200).IsRequired();
            Property(m => m.State).HasMaxLength(50).IsRequired();
            Property(m => m.County).HasMaxLength(70).IsRequired();
            Property(m => m.ZipClass).HasMaxLength(50).IsRequired();
        }
    }
}
