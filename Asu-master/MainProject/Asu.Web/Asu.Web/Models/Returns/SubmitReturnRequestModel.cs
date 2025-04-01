using System;

namespace Asu.Web.Models.Returns
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using FluentValidation.Attributes;

    using Asu.Web.Validators.Customization;

    [Validator(typeof(SubmitReturnRequestValidator))]
    public class SubmitReturnRequestModel
    {
        public SubmitReturnRequestModel()
        {
            this.AvailableReturnReasons = new List<SelectListItem>();
            this.Order = new ReturnRequestOrderModel();
            this.ExistingReturnRequests = new List<ReturnRequestModel>();
            this.ReturnRequestItems = new List<ReturnRequestItemModel>();
            this.Refunds = new List<RefundModel>();
        }

        public Guid? CrmUserId { get; set; }

        public IList<SelectListItem> AvailableReturnReasons { get; set; }

        public ReturnRequestOrderModel Order { get; set; }

        public List<ReturnRequestItemModel> ReturnRequestItems { get; set; }

        public List<ReturnItemModel> PureCancels { get; set; }

        public decimal PureCancelsCreditAmount { get; set; }

        public List<ReturnRequestModel> ExistingReturnRequests { get; set; }

        public ReturnRequestHelpModel Helper { get; set; }

        public bool HasShipments { get; set; }

        public bool IsReturnAllowed { get; set; }

        public bool? IsManual { get; set; }

        public string ErrorMessage { get; set; }

        public List<RefundModel> Refunds { get; set; }
    }
}