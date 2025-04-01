namespace Asu.Web.Models.Returns
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ReturnShipmentModel 
    {
        public ReturnShipmentModel()
        {
            this.CarrierId = 0;
            this.ReturnItems = new List<ReturnRequestItemModel>();
        }

        public string TrackingNumber { get; set; }

        [Display(Name = "Carrier")]
        public int CarrierId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CarrierName { get; set; }

        public string CarrierUrl { get; set; }

        public int RmaId { get; set; }

        public string RmaNumber { get; set; }

        public List<ReturnRequestItemModel> ReturnItems { get; set; }

        public IList<SelectListItem> Carriers { get; set; }
    }
}