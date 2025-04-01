using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core.Caching;
using Asu.Core.Data;
using Asu.Core.Domain.Messages;
using Asu.Services.Events;

namespace Asu.Services.Messages
{
    public partial class SendGridMessageTemplateService : ISendGridMessageTemplateService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : store ID
        /// </remarks>
        private const string MESSAGETEMPLATES_ALL_KEY = "Nop.sendgridmessagetemplate.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : template name
        /// {1} : store ID
        /// </remarks>
        private const string MESSAGETEMPLATES_BY_NAME_KEY = "Nop.sendgridmessagetemplate.name-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string MESSAGETEMPLATES_PATTERN_KEY = "Nop.sendgridmessagetemplate.";

        #endregion

        #region Fields

        private readonly IRepository<SendGridMessageTemplate> _messageTemplateRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="messageTemplateRepository">Message template repository</param>
        /// <param name="eventPublisher">Event published</param>
        public SendGridMessageTemplateService(ICacheManager cacheManager,
            IRepository<SendGridMessageTemplate> messageTemplateRepository,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._messageTemplateRepository = messageTemplateRepository;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void DeleteMessageTemplate(SendGridMessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Delete(messageTemplate);

            _cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(messageTemplate);
        }

        /// <summary>
        /// Inserts a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void InsertMessageTemplate(SendGridMessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Insert(messageTemplate);

            _cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(messageTemplate);
        }

        /// <summary>
        /// Updates a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void UpdateMessageTemplate(SendGridMessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Update(messageTemplate);

            _cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(messageTemplate);
        }

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateId">Message template identifier</param>
        /// <returns>Message template</returns>
        public virtual SendGridMessageTemplate GetMessageTemplateById(int messageTemplateId)
        {
            if (messageTemplateId == 0)
                return null;

            return _messageTemplateRepository.GetById(messageTemplateId);
        }

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Message template</returns>
        public virtual SendGridMessageTemplate GetMessageTemplateByName(string messageTemplateName, int storeId)
        {
            if (string.IsNullOrWhiteSpace(messageTemplateName))
                throw new ArgumentException("messageTemplateName_" + storeId);
            try
            {
                string key = string.Format(MESSAGETEMPLATES_BY_NAME_KEY, messageTemplateName + storeId);
                return _cacheManager.Get(key, () =>
                {
                    var query = _messageTemplateRepository.TableNoTracking;
                    query = query.Where(t => t.Name == messageTemplateName && t.StoreId == storeId);
                    query = query.OrderBy(t => t.Id);
                    var templates = query.ToList();

                    return templates.FirstOrDefault();
                });
            }
            
            catch (Exception e)
            {
                
                return null;
            }
        }

        /// <summary>
        /// Gets all message templates
        /// </summary>
        /// <param name="storeId">Store identifier; pass 0 to load all records</param>
        /// <returns>Message template list</returns>
        public virtual IList<SendGridMessageTemplate> GetAllMessageTemplates()
        {
            string key = string.Format(MESSAGETEMPLATES_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = _messageTemplateRepository.Table;
                query = query.OrderBy(t => t.Name);

                return query.ToList();
            });
        }

        /// <summary>
        /// Create a copy of message template with all depended data
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Message template copy</returns>
        public virtual SendGridMessageTemplate CopyMessageTemplate(SendGridMessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            var mtCopy = new SendGridMessageTemplate
            {
                Name = messageTemplate.Name,
                TemplateId = messageTemplate.TemplateId,
            };

            InsertMessageTemplate(mtCopy);

            return mtCopy;
        }

        #endregion
    }
}
