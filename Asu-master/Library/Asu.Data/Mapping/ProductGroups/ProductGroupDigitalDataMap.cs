namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public class ProductGroupDigitalDataMap : NopEntityTypeConfiguration<ProductGroupDigitalData>
    {
        public ProductGroupDigitalDataMap()
        {
            this.ToTable("vw_ProductGroup_DigitalData");
            this.HasKey(i => i.Id);

            this.HasRequired(i => i.DigitalData)
                .WithMany(p => p.ProductGroupDigitalData)
                .HasForeignKey(i => i.DigitalDataId);


            //this.HasRequired(i => i.ProductGroup)
            //    .WithMany(p => p.ProductGroupDigitalData)
            //    .HasForeignKey(i => i.ProductGroupId);
        }
    }
}
