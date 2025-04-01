namespace Asu.Core.Domain.Shipping
{
    using System;

    public class RoundShippingLog : BaseEntity
    {
        public int CustomerId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ObjectData { get; set; }

        public string StackTrace { get; set; }

    }
}
