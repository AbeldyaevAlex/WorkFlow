namespace Asu.Core.Domain.Returns
{
    using System;

    public class CrmReturnImport : BaseEntity
    {
        public int ReturnRequestId { get; set; }

        public int? ReturnId { get; set; }

        public DateTime ImportedOn { get; set; }

        public virtual ReturnRequest ReturnRequest { get; set; }

        public virtual Return Return { get; set; }
    }
}