namespace Asu.Core.Domain.Customization
{
    public partial class OrderProductToReview : BaseEntity
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int ManufacturerPictureId { get; set; }
        public string Email { get; set; }
        public string CustomerFullName { get; set; }
    }
}