using System;

namespace Asu.Core.Domain.Customization
{
    public partial class QueuedMessagesSendTaskLocker : BaseEntity
    {
        public bool IsBusy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
