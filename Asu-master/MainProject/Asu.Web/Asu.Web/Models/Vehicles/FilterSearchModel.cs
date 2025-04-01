using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core.Domain.Catalog;

namespace Asu.Web.Models.Vehicles
{
    using Asu.Framework.Mvc;
    using Asu.Framework.UI;

    using Asu.Core.Domain.Vehicles;

    using Asu.Core;
    //using Asu.Core.Domain.Solr;
    using Asu.Core.Infrastructure;
    using Asu.Services.Configuration;
    using Asu.Web.Models.Home;

    public class FilterSearchModel : BaseNopModel
    {
        // private string searchCategoryIds;
        private string selectedCategoryIds;
        private string selectedManufacturerIds;
        private string selectedPriceRangeIds;
        // private string primaryFilterOriginalIds;
        private string selectedPerformanceAttributeValues;
        private string selectedTireLoadAttributeValues;
        private string selectedTireSpeedAttributeValues;
        private string selectedTireTreadTypeAttributeValues;
        private string selectedTireSidewallAttributeValues;
        private string selectedLoadRangeAttributeValues;
        private string selectedUtqgAttributeValues;
        private string selectedServiceDescriptionAttributeValues;
        private string selectedTireSizeAttributeValues;
        private string selectedTireRimSizeAttributeValues;

        public FilterSearchModel()
        {
            var storeId = EngineContext.Current.Resolve<IStoreContext>().CurrentStore.Id;
            this.SearchTerms = string.Empty;
            this.SearchCategoryIdsArray = new List<int>();
            this.CId = 0;
            this.MId = 0;

            this.OriginalCategories = new List<Category>();
            this.OriginalSubCategories = new List<Category>();
            this.OriginalManufacturers = new List<Manufacturer>();

            this.SelectedCategoryIdsArray = new List<int>();
            this.SelectedManufacturerIdsArray = new List<int>();
            this.SelectedPriceRangeIdsArray = new List<int>();

            this.FilterManufacturers = new List<CheckBoxListItem>();
            this.PriceRanges = new List<CheckBoxListItem>();
            this.FilterCategories = new List<CheckBoxListItem>();

            this.Products = new List<CustomProductOverviewModel>();

            this.PFC = new PagingFilteringModel();
            this.V = new FilterVehicle();

            // Tire attributes
            this.PerformanceAttributes = new List<CheckBoxListItem>();
            this.PerformanceAttributeFacets = new List<int>();
            this.SelectedPerformanceAttributes = new List<int>();

            this.TireLoadAttributes = new List<CheckBoxListItem>();
            this.TireLoadAttributeFacets = new List<int>();
            this.SelectedTireLoadAttributes = new List<int>();

            this.TireSpeedAttributes = new List<CheckBoxListItem>();
            this.TireSpeedAttributeFacets = new List<int>();
            this.SelectedTireSpeedAttributes = new List<int>();

            this.TreadTypeAttributes = new List<CheckBoxListItem>();
            this.TreadTypeAttributeFacets = new List<int>();
            this.SelectedTreadTypeAttributes = new List<int>();

            this.SidewallAttributes = new List<CheckBoxListItem>();
            this.SidewallAttributeFacets = new List<int>();
            this.SelectedSidewallAttributes = new List<int>();

            this.LoadRangeAttributes = new List<CheckBoxListItem>();
            this.LoadRangeAttributeFacets = new List<int>();
            this.SelectedLoadRangeAttributes = new List<int>();

            this.UtqgAttributes = new List<CheckBoxListItem>();
            this.UtqgAttributeFacets = new List<int>();
            this.SelectedUtqgAttributes = new List<int>();

            this.ServiceDescriptionAttributes = new List<CheckBoxListItem>();
            this.ServiceDescriptionAttributeFacets = new List<int>();
            this.SelectedServiceDescriptionAttributes = new List<int>();

            this.TireSizeAttributes = new List<CheckBoxListItem>();
            this.TireSizeAttributeFacets = new List<int>();
            this.SelectedTireSizeAttributes = new List<int>();

            this.TireRimSizeAttributes = new List<CheckBoxListItem>();
            this.TireRimSizeAttributeFacets = new List<int>();
            this.SelectedTireRimSizeAttributes = new List<int>();
        }

        public string PageType { get; set; }
        public string SearchTerms { get; set; }

        public string Spec_Type { get; set; }
        public string Rim { get; set; }
        public string Section { get; set; }
        public string Aspect { get; set; }
        public string Size { get; set; }

        public string SectionText { get; set; }
        public string AspectText { get; set; }
        public string RimText { get; set; }

        public List<CheckBoxListItem> PerformanceAttributes { get; set; }
        public List<int> PerformanceAttributeFacets { get; set; }
        public List<int> SelectedPerformanceAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalPerformanceAttributes { get; set; }

        public List<CheckBoxListItem> TireLoadAttributes { get; set; }
        public List<int> TireLoadAttributeFacets { get; set; }
        public List<int> SelectedTireLoadAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalTireLoadAttributes { get; set; }

        public List<CheckBoxListItem> TireSpeedAttributes { get; set; }
        public List<int> TireSpeedAttributeFacets { get; set; }
        public List<int> SelectedTireSpeedAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalTireSpeedAttributes { get; set; }

        public List<CheckBoxListItem> TreadTypeAttributes { get; set; }
        public List<int> TreadTypeAttributeFacets { get; set; }
        public List<int> SelectedTreadTypeAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalTreadTypeAttributes { get; set; }

        public List<CheckBoxListItem> SidewallAttributes { get; set; }
        public List<int> SidewallAttributeFacets { get; set; }
        public List<int> SelectedSidewallAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalSidewallAttributes { get; set; }

        public List<CheckBoxListItem> LoadRangeAttributes { get; set; }
        public List<int> LoadRangeAttributeFacets { get; set; }
        public List<int> SelectedLoadRangeAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalLoadRangeAttributes { get; set; }

        public List<CheckBoxListItem> UtqgAttributes { get; set; }
        public List<int> UtqgAttributeFacets { get; set; }
        public List<int> SelectedUtqgAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalUtqgAttributes { get; set; }

        public List<CheckBoxListItem> ServiceDescriptionAttributes { get; set; }
        public List<int> ServiceDescriptionAttributeFacets { get; set; }
        public List<int> SelectedServiceDescriptionAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalServiceDescriptionAttributes { get; set; }

        public List<CheckBoxListItem> TireSizeAttributes { get; set; }
        public List<int> TireSizeAttributeFacets { get; set; }
        public List<int> SelectedTireSizeAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalTireSizeAttributes { get; set; }

        public List<CheckBoxListItem> TireRimSizeAttributes { get; set; }
        public List<int> TireRimSizeAttributeFacets { get; set; }
        public List<int> SelectedTireRimSizeAttributes { get; set; }
        public List<SpecificationAttributeOption> OriginalTireRimSizeAttributes { get; set; }

        public TireConfiguratorModel TireConfigurator { get; set; }

        /// <summary>
        /// LoadOutStockProducts
        /// </summary>
        public bool OS { get; set; }
        public int PageIndex { get; set; }
        public int TotalProducts { get; set; }
        /// <summary>
        /// PrimaryFilter
        /// </summary>
        public PrimaryFilterEnum PF { get; set; }
        public IList<CheckBoxListItem> PriceRanges { get; set; }
        public IList<int> SelectedPriceRangeIdsArray { get; set; }
        /// <summary>
        /// SelectedPriceRangeIds
        /// </summary>
        public string SPRIds
        {
            get
            {
                this.selectedPriceRangeIds = string.Join(",", this.SelectedPriceRangeIdsArray);
                return this.selectedPriceRangeIds;
            }
            set
            {
                this.selectedPriceRangeIds = value;
                this.SelectedPriceRangeIdsArray.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedPriceRangeIds))
                {
                    this.SelectedPriceRangeIdsArray = this.selectedPriceRangeIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// SearchCategoryIds
        /// </summary>
        /*public string SCIds
        {
            get
            {
                this.searchCategoryIds = string.Join(",", this.SearchCategoryIdsArray);
                return this.searchCategoryIds;
            }
            set
            {
                this.searchCategoryIds = value;
                this.SearchCategoryIdsArray.Clear();
                if (!string.IsNullOrWhiteSpace(this.searchCategoryIds))
                {
                    this.SearchCategoryIdsArray = this.searchCategoryIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }*/

        public List<int> SearchCategoryIdsArray { get; set; }
        public List<Category> OriginalCategories { get; set; }
        public List<CheckBoxListItem> FilterCategories { get; set; }
        public List<int> SelectedCategoryIdsArray { get; set; }
        /// <summary>
        /// SelectedCategoryIds
        /// </summary>
        public string SelCIds
        {
            get
            {
                this.selectedCategoryIds = string.Join(",", this.SelectedCategoryIdsArray);
                return this.selectedCategoryIds;
            }
            set
            {
                this.selectedCategoryIds = value;
                this.SelectedCategoryIdsArray.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedCategoryIds))
                {
                    this.SelectedCategoryIdsArray = this.selectedCategoryIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }
        /// <summary>
        /// SearchCategoryId
        /// </summary>
        public int CId { get; set; }       //to exclude from filter checkbox list
        public string CategoryUrl { get; set; }
        /// <summary>
        /// SearchManufacturerId
        /// </summary>
        public int MId { get; set; }
        public IList<Manufacturer> OriginalManufacturers { get; set; }
        public IList<CheckBoxListItem> FilterManufacturers { get; set; }
        public IList<int> SelectedManufacturerIdsArray { get; set; }
        
        /// <summary>
        /// SelectedManufacturerIds
        /// </summary>
        public string SMIds
        {
            get
            {
                this.selectedManufacturerIds = string.Join(",", this.SelectedManufacturerIdsArray);
                return this.selectedManufacturerIds;
            }
            set
            {
                this.selectedManufacturerIds = value;
                this.SelectedManufacturerIdsArray.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedManufacturerIds))
                {
                    this.SelectedManufacturerIdsArray = this.selectedManufacturerIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TirePerformanceAttrs
        {
            get
            {
                this.selectedPerformanceAttributeValues = string.Join(",", this.SelectedPerformanceAttributes);
                return this.selectedPerformanceAttributeValues;
            }
            set
            {
                this.selectedPerformanceAttributeValues = value;
                this.PerformanceAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedPerformanceAttributeValues))
                {
                    this.SelectedPerformanceAttributes = this.selectedPerformanceAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireLoadAttrs
        {
            get
            {
                this.selectedTireLoadAttributeValues = string.Join(",", this.SelectedTireLoadAttributes);
                return this.selectedTireLoadAttributeValues;
            }
            set
            {
                this.selectedTireLoadAttributeValues = value;
                this.TireLoadAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireLoadAttributeValues))
                {
                    this.SelectedTireLoadAttributes = this.selectedTireLoadAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireSpeedAttrs
        {
            get
            {
                this.selectedTireSpeedAttributeValues = string.Join(",", this.SelectedTireSpeedAttributes);
                return this.selectedTireSpeedAttributeValues;
            }
            set
            {
                this.selectedTireSpeedAttributeValues = value;
                this.TireSpeedAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireSpeedAttributeValues))
                {
                    this.SelectedTireSpeedAttributes = this.selectedTireSpeedAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireTreadTypeAttrs
        {
            get
            {
                this.selectedTireTreadTypeAttributeValues = string.Join(",", this.SelectedTreadTypeAttributes);
                return this.selectedTireTreadTypeAttributeValues;
            }
            set
            {
                this.selectedTireTreadTypeAttributeValues = value;
                this.TreadTypeAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireTreadTypeAttributeValues))
                {
                    this.SelectedTreadTypeAttributes = this.selectedTireTreadTypeAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireSidewallAttrs
        {
            get
            {
                this.selectedTireSidewallAttributeValues = string.Join(",", this.SelectedSidewallAttributes);
                return this.selectedTireSidewallAttributeValues;
            }
            set
            {
                this.selectedTireSidewallAttributeValues = value;
                this.SidewallAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireSidewallAttributeValues))
                {
                    this.SelectedSidewallAttributes = this.selectedTireSidewallAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireLoadRangeAttrs
        {
            get
            {
                this.selectedLoadRangeAttributeValues = string.Join(",", this.SelectedLoadRangeAttributes);
                return this.selectedLoadRangeAttributeValues;
            }
            set
            {
                this.selectedLoadRangeAttributeValues = value;
                this.LoadRangeAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedLoadRangeAttributeValues))
                {
                    this.SelectedLoadRangeAttributes = this.selectedLoadRangeAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireUtqgAttrs
        {
            get
            {
                this.selectedUtqgAttributeValues = string.Join(",", this.SelectedUtqgAttributes);
                return this.selectedUtqgAttributeValues;
            }
            set
            {
                this.selectedUtqgAttributeValues = value;
                this.UtqgAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedUtqgAttributeValues))
                {
                    this.SelectedUtqgAttributes = this.selectedUtqgAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireServiceDescriptionAttrs
        {
            get
            {
                this.selectedServiceDescriptionAttributeValues = string.Join(",", this.SelectedServiceDescriptionAttributes);
                return this.selectedServiceDescriptionAttributeValues;
            }
            set
            {
                this.selectedServiceDescriptionAttributeValues = value;
                this.ServiceDescriptionAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedServiceDescriptionAttributeValues))
                {
                    this.SelectedServiceDescriptionAttributes = this.selectedServiceDescriptionAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireSizeAttrs
        {
            get
            {
                this.selectedTireSizeAttributeValues = string.Join(",", this.SelectedTireSizeAttributes);
                return this.selectedTireSizeAttributeValues;
            }
            set
            {
                this.selectedTireSizeAttributeValues = value;
                this.TireSizeAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireSizeAttributeValues))
                {
                    this.SelectedTireSizeAttributes = this.selectedTireSizeAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public string TireRimSizeAttrs
        {
            get
            {
                this.selectedTireRimSizeAttributeValues = string.Join(",", this.SelectedTireRimSizeAttributes);
                return this.selectedTireRimSizeAttributeValues;
            }
            set
            {
                this.selectedTireRimSizeAttributeValues = value;
                this.TireRimSizeAttributeFacets.Clear();
                if (!string.IsNullOrWhiteSpace(this.selectedTireRimSizeAttributeValues))
                {
                    this.SelectedTireRimSizeAttributes = this.selectedTireRimSizeAttributeValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.Trim())).ToList();
                }
            }
        }

        public List<PriceRange> OriginalPriceRanges { get; set; }
        public IList<CustomProductOverviewModel> Products { get; set; }
        public string Warning { get; set; }
        public bool NoResults { get; set; }

        public bool IsManufacturerPageRequested { get; set; }

        public bool IsRootCategoryPageRequested { get; set; }

        public List<Category> OriginalSubCategories { get; set; }

        public bool ShowMembersClubPrices { get; set; }

        /// <summary>
        /// PagingFilteringContext
        /// </summary>
        public PagingFilteringModel PFC { get; set; }
        /// <summary>
        /// Vehicle
        /// </summary>
        public FilterVehicle V { get; set; }

        public class FilterSearchJsonModel
        {
            public string ProductsHtml { get; set; }
            public string FilterHtml { get; set; }
            public string PagerHtml { get; set; }
            public int TotalProducts { get; set; }
            public bool HasNextPage { get; set; }
            public bool HasPreviousPage { get; set; }
            public int PageNumber { get; set; }
            public string DataLayerPush { get; set; }
            public string VehicleMessageHtml { get; set; }

            public string AsideFiltersHtml { get; set; }

            public string ResultsRangeHtml { get; set; }
        }

        public class FilterVehicle
        {
            public int Year { get; set; }
            public int Make { get; set; }
            public int Model { get; set; }
            public int SubModel { get; set; }

            public string FullName { get; set; }

            public string SearchTerm { get; set; }

            public int VehicleId { get; set; }

            public int BaseVehicleId { get; set; }
            /// <summary>
            /// ShowUniversal
            /// </summary>
            public bool SU { get; set; }
            /// <summary>
            /// Is Vehicle Seo page
            /// </summary>
            public bool Seo { get; set; }
        }
    }
}