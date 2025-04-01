namespace Asu.Data.Mapping.Customization
{
    using Asu.Core.Domain.Customization;

    public partial class AmazonPaymentAdvancedMap : NopEntityTypeConfiguration<AmazonPaymentAdvanced>
    {
        public AmazonPaymentAdvancedMap()
        {
            this.ToTable("WCS_AmazonPaymentAdvanced");
            this.HasKey(ap => ap.Id);

            this.Property(ap => ap.AmazonAuthorizationId).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.AmazonCaptureId).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.AmazonRefundId).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.AuthorizeStatus).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.CaptureStatus).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.CreatedOn).IsOptional();
            this.Property(ap => ap.LastError).IsOptional();
            this.Property(ap => ap.OrderAmount).IsRequired();
            this.Property(ap => ap.OrderReferenceId).HasMaxLength(32).IsRequired();
            this.Property(ap => ap.OrderReferenceStatus).HasMaxLength(32).IsOptional();
            this.Property(ap => ap.RefundedAmount).IsRequired();
            this.Property(ap => ap.UpdatedOn).IsOptional();

            this.HasRequired(ap => ap.Customer)
                .WithMany(c => c.AmazonPaymentsAdvanced)
                .Map(ap => ap.MapKey("CustomerId"));

            this.HasOptional(ap => ap.Order)
                .WithOptionalDependent(o => o.AmazonPaymentAdvanced_asFirst)
                .Map(ap => ap.MapKey("OrderId"));

            this.HasOptional(ap => ap.SecondOrder)
                .WithOptionalDependent(o => o.AmazonPaymentAdvanced_asSecond)
                .Map(ap => ap.MapKey("SecondOrderId"));
        }
    }
}
