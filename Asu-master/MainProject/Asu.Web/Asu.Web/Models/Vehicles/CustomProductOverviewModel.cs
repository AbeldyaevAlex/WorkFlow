using Asu.Web.Models.Catalog;
using Asu.Web.Models.Media;
using System.Collections.Generic;

namespace Asu.Web.Models.Vehicles
{
    public class CustomProductOverviewModel : ProductOverviewModel
    {
        public CustomProductOverviewModel()
        {
            this.Manufacturer = new ManufacturerOverviewModel();
            this.ProductSpecifications = new List<ProductSpecificationModel>();
        }

        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int StockQuantity { get; set; }
        public bool IsPriceHidden { get; set; }
        public bool IsShippingFromManufacturer { get; set; }
        public bool IsFreeShipping { get; set; }
        public decimal Price { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal ClubMemberPrice { get; set; }
        public decimal? ClubMemberMinPrice { get; set; }
        public decimal? ClubMemberMaxPrice { get; set; }
        public bool IsClubMember { get; set; }
        public bool IsThirdPartyApiGroup { get; set; }

        public bool IsBestseller { get; set; }

        public List<ProductSpecificationModel> ProductSpecifications { get; set; }

        public ManufacturerOverviewModel Manufacturer { get; set; }
        
        public class ManufacturerOverviewModel
        {
            public ManufacturerOverviewModel()
            {
                this.Logo = new PictureModel();
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string SeName { get; set; }
            public PictureModel Logo { get; set; }
        }

        public CustomProductReviewOverviewModel ReviewOverviewModel { get; set; }
        public bool IsImageLoader { get; set; }
        public double Score { get; set; }
        public string SearchRankExplanation { get; set; }
        public string ParsedQueryString { get; set; }
        public string BrandSlug { get; set; }
        public bool IsGroup { get; set; }
    }
}