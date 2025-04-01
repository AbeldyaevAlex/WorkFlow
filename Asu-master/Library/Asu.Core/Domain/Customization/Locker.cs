using System;

namespace Asu.Core.Domain.Customization
{
    public class Locker : BaseEntity
    {
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
