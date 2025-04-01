namespace Asu.Services.Customization
{
    using System;
    using System.Linq;
    using System.Net;
    using System.IO;

    using Core.Data;

    using Asu.Core.Domain.Catalog;

    public class ShopperApprovedRevewsService : IShopperApprovedReviewsService
    {
        private readonly IRepository<Core.Domain.Catalog.ShopperApprovedReview> repository;
        private readonly ShopperApprovedSettings shopperApprovedSettings;

        public ShopperApprovedRevewsService(IRepository<Core.Domain.Catalog.ShopperApprovedReview> repository, ShopperApprovedSettings shopperApprovedSettings)
        {
            this.repository = repository;
            this.shopperApprovedSettings = shopperApprovedSettings;
        }

        public DateTime? GetLastReviewDate()
        {
            if (this.repository.Table.Count() < 0)
            {
                return null;
            }

            return this.repository.Table.Max(d => d.DisplayDate);
        }

        public void InsertReview(Core.Domain.Catalog.ShopperApprovedReview review)
        {
            if (review == null)
            {
                throw new ArgumentNullException("review");
            }

            if (this.repository.GetById(review.Id) == null)
            {
                this.repository.Insert(review);
            }
        }


        public string Request(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                var response = request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        return null;
                    }

                    using (var reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public ShopperApprovedReview[] GetReviews(DateTime? from = null, DateTime? to = null, int? page = null)
        {
            var endPoint = this.shopperApprovedSettings.EndPoint;
            var siteId = this.shopperApprovedSettings.SiteId;
            var token = this.shopperApprovedSettings.Token;
            var sort = this.shopperApprovedSettings.Sort;
            var url = endPoint + "?siteid=" + siteId + "&token=" + token;

            if (from.HasValue)
            {
                url += "&from=" + from.Value.ToString("yyyy-MM-dd");
            }

            if (to.HasValue)
            {
                url += "&to=" + to.Value.ToString("yyyy-MM-dd");
            }

            if (page.HasValue)
            {
                url += "&page=" + page.Value;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                url += "&sort=" + sort;
            }

            var response = this.Request(url);
            if (string.IsNullOrEmpty(response))
            {
                return new ShopperApprovedReview[0];
            }

            var reviews = response.XmlDeserializeFromString<ShopperApprovedReviews>();

            return reviews.Reviews;
        }

        
    }
}
