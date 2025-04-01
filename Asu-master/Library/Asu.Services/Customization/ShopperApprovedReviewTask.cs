namespace Asu.Services.Customization
{
    using System;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;
    using System.Configuration;
    using System.Threading;

    using Logging;
    using Tasks;
    using Catalog;
    using System.Linq;

    public class ShopperApprovedReviewTask : ITask
    {
        private readonly ICustomService customService;
        private readonly IShopperApprovedReviewsService reviewsService;
        private readonly ILogger logger;
        private const string LockerName = "ShopperApprovedReviewTaskLocker";
        private static readonly Random Randomizer = new Random();

        public ShopperApprovedReviewTask(ICustomService customService, IShopperApprovedReviewsService reviewsService, ILogger logger)
        {
            this.customService = customService;
            this.reviewsService = reviewsService;
            this.logger = logger;
        }

        public void Execute()
        {
            Thread.Sleep(Randomizer.Next(3000, 10000));
            try
            {
                if (this.customService.IsLocked(LockerName, 1200))
                {
                    return;
                }

                this.customService.SetLocked(LockerName);

                var from = this.reviewsService.GetLastReviewDate();
                var page = 0;

                ShopperApprovedReview[] reviews;

                do
                {
                    reviews = this.reviewsService.GetReviews(from, null, page++);

                    if (reviews == null || !reviews.Any())
                    {
                        break;
                    }

                    foreach (var review in reviews)
                    {
                        this.reviewsService.InsertReview(review.ToReviewModel());
                    }
                }
                while (reviews.Length == 100);
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("ShopperApprovedReviewTask error. {0}", ex.Message), ex);
                return;
            }

            this.customService.SetUnlocked(LockerName);
        }

    }
}