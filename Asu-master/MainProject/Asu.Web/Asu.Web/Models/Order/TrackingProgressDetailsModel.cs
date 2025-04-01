namespace Asu.Web.Models.Order
{
    using System;

    public class TrackingProgressDetailsModel
    {
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? EstimateDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}