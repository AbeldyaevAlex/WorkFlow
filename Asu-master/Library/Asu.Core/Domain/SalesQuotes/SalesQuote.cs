namespace Asu.Core.Domain.SalesQuotes
{
    using System;
    using System.Collections.Generic;

    public class SalesQuote : BaseEntity
    {
        private ICollection<SalesQuoteLine> lines;

        public int? OrderId { get; set; }

        public int CreatedBy { get; set; }

        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string Note { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<SalesQuoteLine> Lines
        {
            get => this.lines ?? (this.lines = new List<SalesQuoteLine>());
            protected set => this.lines = value;
        }
    }
}
