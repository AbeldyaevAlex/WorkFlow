using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Stores;

namespace Asu.Core.Domain.Messages
{
    /// <summary>
    /// Represents a message template
    /// </summary>
    public class SendGridMessageTemplate : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Template Id
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the store Id
        /// </summary>
        public int StoreId { get; set; }

    }
}
