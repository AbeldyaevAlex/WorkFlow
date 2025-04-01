namespace Asu.Core.Domain.Customization
{
    using System;

    public partial class OrderShippedEmailSendTaskLocker : BaseEntity
    {
        public bool IsBusy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}