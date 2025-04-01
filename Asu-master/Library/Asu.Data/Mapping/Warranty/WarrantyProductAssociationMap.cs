namespace Asu.Data.Mapping.Warranty
{
    using Asu.Core.Domain.Warranty;

    public class WarrantyProductAssociationMap : NopEntityTypeConfiguration<WarrantyProductAssociation>
    {
        public WarrantyProductAssociationMap()
        {
            this.ToTable("WCS_Product_Warranty_Association");
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.OrderId, m.ProductId });
        }
    }
}
