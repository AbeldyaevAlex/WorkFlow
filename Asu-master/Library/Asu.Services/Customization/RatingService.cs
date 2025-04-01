using Asu.Core.Data;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customization;

namespace Asu.Services.Customization
{
    public sealed class RatingService : IRatingService
    {
        #region Fields

        private readonly IRepository<ProductExtra> productExtraRepository;
        private readonly IRepository<Product> productRepository;

        #endregion

        #region Ctor

        public RatingService(IRepository<ProductExtra> productExtraRepository,
            IRepository<Product> productRepository)
        {
            this.productExtraRepository = productExtraRepository;
            this.productRepository = productRepository;
        }

        #endregion

        #region Methods

        public bool AddRatingToProduct(int productId, double ratingScore)
        {
            var product = productRepository.GetById(productId);
            if (product == null)
                return false;
            var productExtra = product.ProductExtra;
            if (productExtra == null)
            {
                productExtraRepository.Insert(new ProductExtra() { RatingCount = 1, RatingScore = ratingScore, Product = product });
            }
            else
            {
                double newScore = (productExtra.RatingScore * productExtra.RatingCount + ratingScore) / (productExtra.RatingCount + 1);
                productExtra.RatingCount++;
                productExtra.RatingScore = newScore;
                productExtraRepository.Update(productExtra);
            }

            return true;
        }
        public bool GetProductRating(int productId, out int ratingCount, out double ratingScore)
        {
            ratingCount = 0;
            ratingScore = 0;

            var product = productRepository.GetById(productId);
            if (product == null)
                return false;
            var productExtra = product.ProductExtra;
            if (productExtra == null)
            {
                return false;
            }
            else
            {
                ratingCount = productExtra.RatingCount;
                ratingScore = productExtra.RatingScore;
            }

            return true;
        }

        #endregion
    }
}
