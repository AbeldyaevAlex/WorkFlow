using Asu.Core.Domain.Customization;

namespace Asu.Data.Mapping.Customization
{
    public partial class OrderProductToReviewMap : NopEntityTypeConfiguration<OrderProductToReview>
    {
        public OrderProductToReviewMap()
        {
            this.ToTable("vw_OrderProductToReview");
            this.HasKey(or => or.OrderId);

            this.Ignore(or => or.Id);

            this.Property(or => or.OrderId).IsRequired();
            this.Property(or => or.ProductId).IsRequired();
            this.Property(or => or.ProductName).IsRequired();
            this.Property(or => or.ManufacturerPictureId).IsRequired();
            this.Property(or => or.CustomerFullName).IsRequired();
            this.Property(or => or.Email).IsRequired();
        }
    }
}