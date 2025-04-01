namespace Asu.Services.Catalog
{
    using System.Collections.Generic;

    using Core.Domain.ProductGroups;
    using Core.Domain.UserDefinedTableTypes;

    public interface IProductGroupService
    {
        ProductGroup GetProductGroupById(int productId);

        BrandCategory GetBrandCategoryById(int categoryId);

        IList<ProductIdArray> InsertOrUpdateProducts(IList<ProductArray> products);

        void InsertOrUpdateProductCost(IList<ProductCostArray> productCosts);

        string GetDefaultPictureUrl(int productGroupId, int maxWidth = 0, int maxHeight = 0);

        string GetDefaultPictureUrl(ProductGroup productGroup, int maxWidth = 0, int maxHeight = 0);

        IList<CoverkingProductData> GetVendorData(string[] itemIds);
    }
}
