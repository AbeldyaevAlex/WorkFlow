namespace Asu.Services.Customization
{
    public interface IRatingService
    {
        bool AddRatingToProduct(int productId, double ratingScore);
        bool GetProductRating(int productId, out int ratingCount, out double ratingScore);
    }
}
