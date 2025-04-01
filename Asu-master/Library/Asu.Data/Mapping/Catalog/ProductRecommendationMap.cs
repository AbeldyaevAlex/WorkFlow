using Asu.Core.Domain.Catalog;

namespace Asu.Data.Mapping.Catalog
{
    public class ProductRecommendationMap : NopEntityTypeConfiguration<ProductRecommendation>
    {
        public ProductRecommendationMap()
        {
            this.ToTable("Product_Recommendation");
            this.HasKey(productRecommend => productRecommend.Id);


            this.HasRequired(productRecommend => productRecommend.Product)
                .WithMany(o => o.ProductRecommendations)
                .HasForeignKey(productRecommend => productRecommend.ProductId);

            /*this.HasRequired(productRecommend => productRecommend.ParentProduct)
                .WithMany(o => o.ProductRecommendations)
                .HasForeignKey(productRecommend => productRecommend.ParentProductId);*/
        }
    }
}