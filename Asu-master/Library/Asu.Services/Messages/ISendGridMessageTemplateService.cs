using System.Collections.Generic;
using Asu.Core.Domain.Messages;

namespace Asu.Services.Messages
{
    /// <summary>
    /// Message template service
    /// </summary>
    public partial interface ISendGridMessageTemplateService
    {
        /// <summary>
        /// Delete a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        void DeleteMessageTemplate(SendGridMessageTemplate messageTemplate);

        /// <summary>
        /// Inserts a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        void InsertMessageTemplate(SendGridMessageTemplate messageTemplate);

        /// <summary>
        /// Updates a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        void UpdateMessageTemplate(SendGridMessageTemplate messageTemplate);

        /// <summary>
        /// Gets a message template by identifier
        /// </summary>
        /// <param name="messageTemplateId">Message template identifier</param>
        /// <returns>Message template</returns>
        SendGridMessageTemplate GetMessageTemplateById(int messageTemplateId);

        /// <summary>
        /// Gets a message template by name
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <returns>Message template</returns>
        SendGridMessageTemplate GetMessageTemplateByName(string messageTemplateName, int storeId);

        /// <summary>
        /// Gets all message templates
        /// </summary>
        /// <param name="storeId">Store identifier; pass 0 to load all records</param>
        /// <returns>Message template list</returns>
        IList<SendGridMessageTemplate> GetAllMessageTemplates();

        /// <summary>
        /// Create a copy of message template with all depended data
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Message template copy</returns>
        SendGridMessageTemplate CopyMessageTemplate(SendGridMessageTemplate messageTemplate);
    }
}
