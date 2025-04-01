using System.Collections.Generic;
using Asu.Core.Domain.Orders;

namespace Asu.Services.Orders
{
    /// <summary>
    /// Represents a PlaceOrderResult
    /// </summary>
    public partial class PlaceOrderResult
    {
        public IList<string> Errors { get; set; }

        public PlaceOrderResult() 
        {
            this.Errors = new List<string>();
        }

        public bool Success
        {
            get { return (this.Errors.Count == 0); }
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        
        /// <summary>
        /// Gets or sets the placed order
        /// </summary>
        public Order PlacedOrder { get; set; }
    }
}
