using Newtonsoft.Json;

namespace Asu.Core.Domain.GoogleTagManager
{
    public class Product
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mpn")]
        public string Mpn { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("itemUrl")]
        public string ProductUrl { get; set; }

        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonProperty("categoryId")]
        public int? CategoryId { get; set; }

        [JsonProperty("parentCategoryId")]
        public int? ParentCategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("categoryBreadCrumb")]
        public string CategoryBreadCrumb { get; set; }

        [JsonProperty("brandId")]
        public int? ManufacturerId { get; set; }

        [JsonProperty("brandName")]
        public string ManufacturerName { get; set; }

        [JsonProperty("stockQty")]
        public int StockQty { get; set; }

        [JsonProperty("dropShip")]
        public bool ShipsFromManufacturer { get; set; }

        [JsonProperty("imageLoader")]
        public bool UsesImageLoader { get; set; }

        [JsonProperty("priceBelowUsQty")]
        public int PriceBelowUsQty { get; set; }

        [JsonProperty("shippingType")]
        public string ShippingType { get; set; }
    }
}
