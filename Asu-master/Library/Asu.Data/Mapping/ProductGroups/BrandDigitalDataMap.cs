namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public class BrandDigitalDataMap : NopEntityTypeConfiguration<BrandDigitalData>
    {
        public BrandDigitalDataMap()
        {
            this.ToTable("vw_Brand_DigitalData");
            this.HasKey(i => i.Id);

            this.HasRequired(i => i.DigitalData)
                .WithMany(p => p.BrandDigitalData)
                .HasForeignKey(i => i.DigitalDataId);


            this.HasRequired(i => i.Manufacturer)
                .WithMany(p => p.DigitalData)
                .HasForeignKey(i => i.ManufacturerId);
        }
    }
}