using System.Collections.Generic;
using Asu.Core.Domain.Vehicles;

namespace Asu.Services.Vehicles
{
    using Asu.Core;
    using Asu.Core.Domain.Catalog;

    using PriceRange = Asu.Core.Domain.Vehicles.PriceRange;

    public interface IVehicleService
    {
        IList<Year> GetYears(int? productId = null);
        Make GetMake(int id);
        IList<Make> GetMakes();
        IList<Make> GetMakes(int year);
        IList<Make> GetMakes(int year, int productId);

        IList<Make> GetPopularMakes(int year);
        IList<Model> GetPopularModels(int year, int makeId);

        Model GetModel(int id);
        IList<Model> GetModels();
        IList<Model> GetModels(int makeId);
        IList<Model> GetModels(int year, string make);
        IList<Model> GetModels(int yearId, int makeId);
        IList<Model> GetModels(int yearId, int makeId, int productId);

        IList<Year> GetYears(int makeId, int modelId);
        IList<Year> GetYearsByMake(int makeId);
        IList<Year> GetYearsByModel(int modelId);

        IList<SubModel> GetSubModels(int year, string make, string model);
        IList<SubModel> GetSubModels(int yearId, int makeId, int modelId);
        IList<SubModel> GetSubModels(int yearId, int makeId, int modelId, int minProducts);
        IList<SubModel> GetProductSubModels(int yearId, int makeId, int modelId, int productId);
        IList<Product> GetRelatedProducts(int productId, int yearId, int makeId, int modelId, int? subModelId);
        IList<Product> GetRelatedProducts(int productId, int yearId, int makeId, int modelId, int? subModelId,
                int section, int aspect, int rim);
        BaseVehicle GetBaseVehicle(int yearId, int makeId, int modelId);
        Vehicle GetVehicleById(int vehicleId);
        Vehicle GetVehicle(int year, string make, string model, string submodel);
        Vehicle GetVehicle(int yearId, int makeId, int modelId, int submodelId);
        int? SetVehicleToCookies(int yearId, int makeId, int modelId, int submodelId, bool showUniversal);
        void SetVehicleSeoToCookies(int? yearId, int? makeId, int? modelId);
        bool GetVehicleFromCookies(out int yearId, out int makeId, out int modelId, out int submodelId, out bool showUniversal);
        Vehicle GetVehicleFromCookies();
        void ClearVehicleCookies();
        IList<PriceRange> GetPriceRanges();
        IList<PriceRange> GetPriceRangesByIds(IList<int> ids);

        IPagedList<ProductOverview> SearchProducts(
            out IList<int> originalCategoryIds,
            out IList<int> originalManufacturerIds,
            out IList<int> originalPriceRangeIds,
            out IList<int> availableCategoryIds,
            out IList<int> availableManufacturerIds,
            out IList<int> availablePriceRangeIds,
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int vehicleId = 0,
            bool loadUniversalProducts = false,
            bool loadOutStockProducts = false,
            int pageIndex = 0,
            int pageSize = 2147483647,
            //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            IList<int> filteredSpecs = null,
            Core.Domain.Vehicles.ProductSortingEnum orderBy = Core.Domain.Vehicles.ProductSortingEnum.Position,
            bool showHidden = false,
            string filterableCategoryIds = null,
            string filterableManufacturerIds = null,
            string filterablePriceRangeIds = null,
            decimal? filterableMinPrice = null,
            decimal? filterableMaxPrice = null,
            PrimaryFilterEnum filterablePrimaryFilter = PrimaryFilterEnum.None);

        IPagedList<ProductOverview> VehicleSearchProducts(
            out IList<int> originalCategoryIds,
            out IList<int> originalManufacturerIds,
            out IList<int> originalPriceRangeIds,
            out IList<int> availableCategoryIds,
            out IList<int> availableManufacturerIds,
            out IList<int> availablePriceRangeIds,
            out IList<int> filterableSpecificationAttributeOptionIds,
            int makeId,
            int? modelId = null,
            int? yearId = null,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            bool loadUniversalProducts = false,
            bool loadOutStockProducts = false,
            int pageIndex = 0,
            int pageSize = 2147483647,
            //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            IList<int> filteredSpecs = null,
            Core.Domain.Vehicles.ProductSortingEnum orderBy = Core.Domain.Vehicles.ProductSortingEnum.Position,
            bool showHidden = false,
            string filterableCategoryIds = null,
            string filterableManufacturerIds = null,
            string filterablePriceRangeIds = null,
            decimal? filterableMinPrice = null,
            decimal? filterableMaxPrice = null,
            PrimaryFilterEnum filterablePrimaryFilter = PrimaryFilterEnum.None);

        IList<int> GetSubcategoryIdsByVehicle(int parentCategoryId, int vehicleId);

        IList<Make> GetMakesActiveForSeo();
        IList<KeyValuePair<Make, Model>> GetMakeModelsActiveForSeo();
        IList<Category> GetVehicleCategories(int make, int? model, int? year, int storeId);
        IList<HeaderCategories> GetHeaderCategories(int[] ids);
        void AddVehicleToCustomerGarage(int vehicleId, int customerId, bool isMain = false);
        void ClearCustomerGarage(int customerId);
        Vehicle RemoveVehicleFromCustomerGarage(int customerId, int vehicleId);
        void SetNoMainVehicleGarage(int customerId);
        void UpdateMainVehicleGarage(int vehicleId, int customerId);
    }
}
