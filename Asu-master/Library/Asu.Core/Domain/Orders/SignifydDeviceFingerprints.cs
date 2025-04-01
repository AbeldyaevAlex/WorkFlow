
namespace Asu.Core.Domain.Orders
{
    public class SignifydDeviceFingerprints: BaseEntity
    {
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the session identifier
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

    }
}