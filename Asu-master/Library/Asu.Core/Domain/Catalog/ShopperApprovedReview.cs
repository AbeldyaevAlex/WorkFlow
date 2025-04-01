namespace Asu.Core.Domain.Catalog
{
    using System;

    public partial class ShopperApprovedReview : BaseEntity
    {
        public string CustomerName { get; set; }

        public DateTime DisplayDate { get; set; }

        public int? OrderId { get; set; }

        public string Comments { get; set; }

        public string Url { get; set; }

        public decimal? Overall { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
