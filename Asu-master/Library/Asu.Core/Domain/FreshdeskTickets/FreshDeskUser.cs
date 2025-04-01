using System;

namespace Asu.Core.Domain.FreshdeskTickets
{
    public class FreshDeskUser : BaseEntity
    {
        public long FreshDeskUserId { get; set; }

        public Guid CrmUserId { get; set; }

        public int? SiteUserId { get; set; }
    }
}