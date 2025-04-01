using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    using System;
    using System.Collections.Generic;

    using Localization;
    using Security;
    using Seo;
    using Stores;
    using Discounts;
    using ProductGroups;

    /// <summary>
    /// Represents a manufacturer
    /// </summary>
    public partial class Manufacturer : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        #region WC

        private ICollection<Discount> appliedDiscounts;

        private ICollection<ProductGroup> productGroups;

        private ICollection<BrandCategory> brandCategories;

        private ICollection<BrandDigitalData> digitalData;

        #endregion

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value of used manufacturer template identifier
        /// </summary>
        public int ManufacturerTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the parent picture identifier
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers can select the page size
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Gets or sets the available customer selectable page size options
        /// </summary>
        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the available price ranges
        /// </summary>
        public string PriceRanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        #region WC

        /// <summary>
        /// Gets or sets a value indicating whether this brand has discounts applied
        /// <remarks>The same as if we run manufacturer.AppliedDiscounts.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load Applied Discounts navifation property
        /// </remarks>
        /// </summary>
        public bool HasDiscountsApplied { get; set; }

        /// <summary>
        /// Gets or sets the collection of applied discounts
        /// </summary>
        public virtual ICollection<Discount> AppliedDiscounts
        {
            get { return this.appliedDiscounts ?? (this.appliedDiscounts = new List<Discount>()); }
            protected set { this.appliedDiscounts = value; }
        }

        public virtual ICollection<ProductGroup> ProductGroups
        {
            get { return this.productGroups ?? (this.productGroups = new List<ProductGroup>()); }
            protected set { this.productGroups = value; }
        }

        public virtual ICollection<BrandCategory> BrandCategories
        {
            get { return this.brandCategories ?? (this.brandCategories = new List<BrandCategory>()); }
            protected set { this.brandCategories = value; }
        }

        public virtual ICollection<BrandDigitalData> DigitalData
        {
            get { return this.digitalData ?? (this.digitalData = new List<BrandDigitalData>()); }
            protected set { this.digitalData = value; }
        }

        #endregion
    }
}
