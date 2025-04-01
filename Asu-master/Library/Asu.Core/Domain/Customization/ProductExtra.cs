using Asu.Core.Domain.Catalog;

namespace Asu.Core.Domain.Customization
{
    public partial class ProductExtra : BaseEntity
    {
        //public int ProductId { get; set; }
        public string ManufacturerPartNumberClean { get; set; }
        public string SkuClean { get; set; }
        public int RatingCount { get; set; }
        public double RatingScore { get; set; }
        public bool IsShippingOverridePerItem { get; set; }
        public decimal ShippingOverride { get; set; }
        public decimal? FixedRateShipping { get; set; }
        public bool IsPriceHidden { get; set; }
        public bool IsShippingFromManufacturer { get; set; }
        public bool IsUniversal { get; set; }
        public bool IsFreight { get; set; }
        public int PriceBelowUsQty { get; set; }
        public string ShippingType { get; set; }
        public virtual Product Product { get; set; }

        public bool IsWarranty { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool IsGroup { get; set; }
        public decimal? BundleDiscount { get; set; }
    }
}
