namespace Asu.Core.Domain.Returns
{
    using System;
    using System.Collections.Generic;

    public class ReturnRequest : BaseEntity
    {
        private ICollection<ReturnRequestItem> items;

        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsManual { get; set; }

        public Guid? CrmUserId { get; set; }

        public int? SiteUserId { get; set; }

        public int? FreshdeskTicketId { get; set; }

        public virtual CrmReturnImport Import { get; set; }

        public virtual ICollection<ReturnRequestItem> Items
        {
            get { return this.items ?? (this.items = new List<ReturnRequestItem>()); }
            set { this.items = value; }
        }
    }
}
