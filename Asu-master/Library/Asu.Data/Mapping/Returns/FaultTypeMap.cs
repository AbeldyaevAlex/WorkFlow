namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class FaultTypeMap : NopEntityTypeConfiguration<FaultType>
    {
        public FaultTypeMap()
        {
            this.ToTable("vw_crm_FaultTypes");
            this.HasKey(i => i.Id);
            this.Property(i => i.Name).IsRequired();
        }
    }
}