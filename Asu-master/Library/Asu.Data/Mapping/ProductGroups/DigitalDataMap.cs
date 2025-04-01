namespace Asu.Data.Mapping.ProductGroups
{
    using Asu.Core.Domain.ProductGroups;

    public class DigitalDataMap : NopEntityTypeConfiguration<DigitalData>
    {
        public DigitalDataMap()
        {
            this.ToTable("WCS_DigitalData");
            this.HasKey(i => i.Id);
        }
    }
}
