namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnReasonMap : NopEntityTypeConfiguration<ReturnReason>
    {
        public ReturnReasonMap()
        {
            this.ToTable("vw_crm_ReturnReasons");
            this.HasKey(i => i.Id);
            this.Property(i => i.Name).IsRequired();
            this.Property(i => i.InitiationType).IsRequired().HasColumnName("InitiationTypeId");
            this.HasRequired(i => i.FaultType).WithMany().HasForeignKey(i => i.FaultTypeId);
        }
    }
}