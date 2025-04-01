namespace Asu.Web.Models.Returns
{
    using System;

    public class RefundModel
    {
        public DateTime CreatedOn { get; set; }

        public decimal CreditTotal { get; set; }

        public string Reason { get; set; }
    }
}