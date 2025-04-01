using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class ProductReviewCustomerNotificationMap : NopEntityTypeConfiguration<ProductReviewCustomerNotification>
    {
        public ProductReviewCustomerNotificationMap()
        {
            this.ToTable("WCS_ProductReviewCustomerNotification");
            this.HasKey(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.CreatedOnUtc).IsRequired();
        }
    }
}