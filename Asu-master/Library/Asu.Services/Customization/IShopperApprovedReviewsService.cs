namespace Asu.Services.Customization
{
    using System;

    using Model = Core.Domain.Catalog;

    public interface IShopperApprovedReviewsService
    {
        DateTime? GetLastReviewDate();

        void InsertReview(Model.ShopperApprovedReview reviews);

        string Request(string url);

        ShopperApprovedReview[] GetReviews(DateTime? from, DateTime? to, int? page);
    }
}
