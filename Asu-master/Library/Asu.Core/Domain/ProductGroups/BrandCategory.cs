namespace Asu.Core.Domain.ProductGroups
{
    using System.Collections.Generic;

    using Catalog;

    public class BrandCategory : BaseEntity
    {
        private ICollection<ProductGroup> productGroups;

        public string Name { get; set; }

        public string Description { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int DigitalDataId { get; set; }

        public int ManufacturerId { get; set; }

        public bool Active { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual DigitalData Picture { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups
        {
            get { return this.productGroups ?? (this.productGroups = new List<ProductGroup>()); }
            protected set { this.productGroups = value; }
        }
    }
}
