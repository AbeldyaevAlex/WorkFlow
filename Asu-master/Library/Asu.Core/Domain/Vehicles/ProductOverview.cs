using Asu.Core.Domain.Seo;

namespace Asu.Core.Domain.Vehicles
{
    public class ProductOverview : BaseEntity, ISlugSupported
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool CallForPrice { get; set; }
        public bool? IsPriceHidden { get; set; }
        public double? RatingScore { get; set; }
        public int? RatingCount { get; set; }
        public int? PictureId { get; set; }
        public string PictureMimeType { get; set; }
        public string SeName { get; set; }
        public int ManufacturerId { get; set; }
        public bool IsShippingFromManufacturer { get; set; }
        public bool IsUniversal { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool IsFreight { get; set; }
    }
}
