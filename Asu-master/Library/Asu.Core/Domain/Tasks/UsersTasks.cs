using Asu.Core.Domain.Customers;
using Asu.Core.Domain.StatusDirectory;
using System;


namespace Asu.Core.Domain.Tasks
{
    public partial class UsersTasks : BaseEntity
    {
        public string ShortNaimTask { get; set; }

        public string NaimTask { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int? IdRoditel { get; set; }

        public byte[] Screen { get; set; }

        public string NaimScreen { get; set; }

        public string AlternativeText { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool? IsGroup { get; set; }

        public string RouteUrl { get; set; }

        public string Title { get; set; }

        public int CreatorId { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }


    }
}
