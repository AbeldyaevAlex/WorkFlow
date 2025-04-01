namespace Asu.Core.Domain.ProductGroups
{
    using System.Collections.Generic;

    using Catalog;

    public class ProductGroup : BaseEntity
    {
        private ICollection<ProductGroupDigitalData> productGroupDigitalData;

        public int ManufacturerId { get; set; }

        public int? CategoryId { get; set; }

        public string BrandCode { get; set; }

        public string LineCode { get; set; }

        public string MaterialCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public int TemplateId { get; set; }

        public decimal RatingScore { get; set; }

        public int RatingCount { get; set; }

        public bool IsFreeShipping { get; set; }

        public bool IsShippingFromManufacturer { get; set; }

        public bool Active { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual BrandCategory Category { get; set; }

        public virtual ProductTemplate Template { get; set; }

        public virtual ICollection<ProductGroupDigitalData> ProductGroupDigitalData
        {
            get { return this.productGroupDigitalData ?? (this.productGroupDigitalData = new List<ProductGroupDigitalData>()); }
            protected set { this.productGroupDigitalData = value; }
        }
    }
}
