namespace Asu.Web.Models.Returns
{
    using System;

    public class TrackingProgressBarModel 
    {
        public DateTime RmaIssued { get; set; }

        public DateTime ReturnIssued { get; set; }

        public DateTime? ShipDate { get; set; }

        public DateTime? EstimateDeliveryDate { get; set; }

        public DateTime? DeliveryDate { get; set; }
    }
}