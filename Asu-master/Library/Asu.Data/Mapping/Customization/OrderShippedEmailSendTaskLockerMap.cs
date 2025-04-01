namespace Asu.Data.Mapping.Customization
{
    using Asu.Core.Domain.Customization;

    public partial class OrderShippedEmailSendTaskLockerMap : NopEntityTypeConfiguration<OrderShippedEmailSendTaskLocker>
    {
        public OrderShippedEmailSendTaskLockerMap()
        {
            this.ToTable("WCS_OrderShippedEmailSendTaskLocker");
            this.HasKey(apat => apat.Id);

            this.Property(apat => apat.IsBusy).IsRequired();
            this.Property(apat => apat.UpdatedOn).IsRequired();
        }
    }
}