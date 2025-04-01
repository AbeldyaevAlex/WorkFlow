using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Asu.Core;
using Asu.Core.Domain.Blogs;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Forums;
using Asu.Core.Domain.Messages;
using Asu.Core.Domain.News;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Domain.Stores;
using Asu.Core.Domain.Vendors;
using Asu.Services.Customers;
using Asu.Services.Events;
using Asu.Services.Localization;
using Asu.Services.Stores;

namespace Asu.Services.Messages
{
    using System.Globalization;
    using Asu.Core.Domain.Returns;
    using Asu.Core.Domain.SalesQuotes;
    using Asu.Core.Infrastructure;
    using Asu.Services.Orders;

    public partial class WorkflowMessageService : IWorkflowMessageService
    {
        #region Fields

        private static readonly Random _rnd = new Random();
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly ILanguageService _languageService;
        private readonly ISendGridMessageTemplateService _sendGridMessageTemplateService;
        private readonly IQueuedEmailSendGridService _queuedEmailSendGridService;
        private readonly ITokenizer _tokenizer;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IMessageTokenProvider _messageTokenProvider;
        private readonly IStoreService _storeService;
        private readonly IStoreContext _storeContext;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        public WorkflowMessageService(IMessageTemplateService messageTemplateService,
            IQueuedEmailService queuedEmailService,
            ILanguageService languageService,
            ISendGridMessageTemplateService sendGridMessageTemplateService,
            IQueuedEmailSendGridService queuedEmailSendGridService,
            ITokenizer tokenizer,
            IEmailAccountService emailAccountService,
            IMessageTokenProvider messageTokenProvider,
            IStoreService storeService,
            IStoreContext storeContext,
            EmailAccountSettings emailAccountSettings,
            IEventPublisher eventPublisher)
        {
            this._messageTemplateService = messageTemplateService;
            this._queuedEmailService = queuedEmailService;
            this._languageService = languageService;
            this._sendGridMessageTemplateService = sendGridMessageTemplateService;
            this._queuedEmailSendGridService = queuedEmailSendGridService;
            this._tokenizer = tokenizer;
            this._emailAccountService = emailAccountService;
            this._messageTokenProvider = messageTokenProvider;
            this._storeService = storeService;
            this._storeContext = storeContext;
            this._emailAccountSettings = emailAccountSettings;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Utilities

        protected virtual int SendNotification(MessageTemplate messageTemplate,
            EmailAccount emailAccount, int languageId, IEnumerable<Token> tokens,
            string toEmailAddress, string toName,
            string attachmentFilePath = null, string attachmentFileName = null,
            string replyToEmailAddress = null, string replyToName = null, string bcc = null)
        {
            //retrieve localized message template data
            if (string.IsNullOrEmpty(bcc))
            {
                bcc = messageTemplate.GetLocalized(mt => mt.BccEmailAddresses, languageId);
            }

            var subject = messageTemplate.GetLocalized(mt => mt.Subject, languageId);
            var body = messageTemplate.GetLocalized(mt => mt.Body, languageId);

            //Replace subject and body tokens 
            var subjectReplaced = _tokenizer.Replace(subject, tokens, false);
            var bodyReplaced = _tokenizer.Replace(body, tokens, true);

            var email = new QueuedEmail
            {
                Priority = 5,
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = toEmailAddress,
                ToName = toName,
                ReplyTo = replyToEmailAddress,
                ReplyToName = replyToName,
                CC = string.Empty,
                Bcc = bcc,
                Subject = subjectReplaced,
                Body = bodyReplaced,
                AttachmentFilePath = attachmentFilePath,
                AttachmentFileName = attachmentFileName,
                CreatedOnUtc = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            };

            this._queuedEmailService.InsertQueuedEmail(email);
            return email.Id;
        }

        protected virtual int SendNotification(SendGridMessageTemplate messageTemplate, string data, int languageId, string fromEmail = null, string fromName = null,
            string toEmailAddress = null, string toName = null, string subject = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            string replyToEmailAddress = null, string replyToName = null, string bcc = null)
        {
            var email = new QueuedEmailSendGrid
            {
                From = fromEmail,
                FromName = fromName,
                To = toEmailAddress,
                ToName = toName,
                ReplyTo = replyToEmailAddress,
                ReplyToName = replyToName,
                CC = string.Empty,
                Bcc = bcc,
                Subject = subject,
                Data = data,
                AttachmentFilePath = attachmentFilePath,
                AttachmentFileName = attachmentFileName,
                CreatedOnUtc = DateTime.UtcNow,
                TemplateId = messageTemplate.TemplateId
            };

            this._queuedEmailSendGridService.InsertQueuedEmail(email);
            return email.Id;
        }

        protected virtual MessageTemplate GetActiveMessageTemplate(string messageTemplateName, int storeId)
        {
            var messageTemplate = _messageTemplateService.GetMessageTemplateByName(messageTemplateName, storeId);

            //no template found
            if (messageTemplate == null)
                return null;

            //ensure it's active
            var isActive = messageTemplate.IsActive;
            if (!isActive)
                return null;

            return messageTemplate;
        }

        protected virtual EmailAccount GetEmailAccountOfMessageTemplate(MessageTemplate messageTemplate, int languageId)
        {
            var emailAccounId = messageTemplate.GetLocalized(mt => mt.EmailAccountId, languageId);
            var emailAccount = _emailAccountService.GetEmailAccountById(emailAccounId);
            if (emailAccount == null)
                emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
            if (emailAccount == null)
                emailAccount = _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
            return emailAccount;
        }

        protected virtual int EnsureLanguageIsActive(int languageId, int storeId)
        {
            //load language by specified ID
            var language = _languageService.GetLanguageById(languageId);

            if (language == null || !language.Published)
            {
                //load any language from the specified store
                language = _languageService.GetAllLanguages(storeId: storeId).FirstOrDefault();
            }
            if (language == null || !language.Published)
            {
                //load any language
                language = _languageService.GetAllLanguages().FirstOrDefault();
            }

            if (language == null)
                throw new Exception("No active language could be loaded");
            return language.Id;
        }

        #endregion

        #region Methods

        #region Customer workflow

        /// <summary>
        /// Sends 'New customer' notification message to a store owner
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendCustomerRegisteredNotificationMessage(Customer customer, int languageId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("NewCustomer.Notification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a welcome message to a customer
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendCustomerWelcomeMessage(Customer customer, int languageId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Customer.WelcomeMessage", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = customer.Email;
            var toName = customer.GetFullName();
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an email validation message to a customer
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendCustomerEmailValidationMessage(Customer customer, int languageId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Customer.EmailValidationMessage", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);


            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = customer.Email;
            var toName = customer.GetFullName();
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends password recovery message to a customer
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendCustomerPasswordRecoveryMessage(Customer customer, int languageId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Customer.PasswordRecovery", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);


            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = customer.Email;
            var toName = customer.GetFullName();
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        #endregion

        #region Order workflow

        /// <summary>
        /// Sends an order placed notification to a vendor
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="vendor">Vendor instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPlacedVendorNotification(Order order, Vendor vendor, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (vendor == null)
                throw new ArgumentNullException("vendor");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("OrderPlaced.VendorNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, order, languageId, vendor.Id);
            _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = vendor.Email;
            var toName = vendor.Name;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order placed notification to a store owner
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPlacedStoreOwnerNotification(Order order, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("OrderPlaced.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, order, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order paid notification to a store owner
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPaidStoreOwnerNotification(Order order, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("OrderPaid.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, order, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order paid notification to a customer
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPaidCustomerNotification(Order order, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("OrderPaid.CustomerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, order, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order paid notification to a vendor
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="vendor">Vendor instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPaidVendorNotification(Order order, Vendor vendor, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (vendor == null)
                throw new ArgumentNullException("vendor");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("OrderPaid.VendorNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, order, languageId, vendor.Id);
            _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = vendor.Email;
            var toName = vendor.Name;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order placed notification to a customer
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderPlacedCustomerNotification(Order order, int languageId,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("OrderPlaced.CustomerNotification", store.Id);
            if (messageTemplate == null)
                return 0;
            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;
            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, null,
                toEmail, toName, null,
                attachmentFilePath,
                attachmentFileName);
        }

        /// <summary>
        /// Sends a shipment sent notification to a customer
        /// </summary>
        /// <param name="shipment">Shipment</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendShipmentSentCustomerNotification(Shipment shipment, int languageId)
        {
            if (shipment == null)
            {
                throw new ArgumentNullException("shipment");
            }

            var order = shipment.Order;
            if (order == null)
            {
                throw new Exception("Order cannot be loaded");
            }
            
            var store = _storeService.GetStoreById(order.StoreId);
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("ShipmentSent.CustomerNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = order.ShippingAddress.Email;
            var toName = string.Format("{0} {1}", order.ShippingAddress.FirstName, order.ShippingAddress.LastName);
            var subject = $"Your order #{shipment.OrderId} from {store.CompanyName} has been shipped.";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddShipmentData(shipment, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddOrderData(shipment.Order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload, payload.GetType(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return SendNotification(messageTemplate, json,
                languageId, fromEmail, null,
                toEmail, toName, subject);
        }

        /// <summary>
        /// Sends a shipment delivered notification to a customer
        /// </summary>
        /// <param name="shipment">Shipment</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendShipmentDeliveredCustomerNotification(Shipment shipment, int languageId)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            var order = shipment.Order;
            if (order == null)
                throw new Exception("Order cannot be loaded");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("ShipmentDelivered.CustomerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddShipmentTokens(tokens, shipment, languageId);
            _messageTokenProvider.AddOrderTokens(tokens, shipment.Order, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, shipment.Order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends an order completed notification to a customer
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderCompletedCustomerNotification(Order order, int languageId,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);


            {
                var messageTemplate = GetActiveMessageTemplate("OrderCompleted.CustomerNotification", store.Id);
                if (messageTemplate == null)
                    return 0;

                //email account
                var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

                //tokens
                var tokens = new List<Token>();
                _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
                _messageTokenProvider.AddOrderTokens(tokens, order, languageId);
                _messageTokenProvider.AddCustomerTokens(tokens, order.Customer);

                //event notification
                _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

                var toEmail = order.BillingAddress.Email;
                var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
                return SendNotification(messageTemplate, emailAccount,
                    languageId, tokens,
                    toEmail, toName,
                    attachmentFilePath,
                    attachmentFileName);
            }
        }

        /// <summary>
        /// Sends an order cancelled notification to a customer
        /// </summary>
        /// <param name="order">Order instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendOrderCancelledCustomerNotification(Order order, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("OrderCancelled.CustomerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            var subject = string.Format("Your order has been cancelled #{0}", order.Id.ToString());

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddOrderCancelData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddCustomerData(order.Customer, dynamicTemplateData);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, null,
                toEmail, toName, subject);
        }

        public int SendManualOrderCancelledCustomerNotification(CrmSalesOrder order, int languageId)
        {
            if (order == null)
                throw new ArgumentNullException("OrderCancelled");

            var nopStoreId = order.ChannelId == (int)Channel.ManualOrdersAp ? NopStore.Autoplicity : NopStore.Thmotorsports;
            var nopStore = this._storeService.GetStoreById((int)nopStoreId) ?? _storeContext.CurrentStore;
            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("OrderCancelled.CustomerNotification", nopStore.Id);
            if (messageTemplate == null)
                return 0;

            var fromName = nopStore.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{nopStore.Name.ToLower()}.com";
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            var subject = string.Format("Your order cancelled #{0}", order.Number.ToString());

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddManualOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddOrderCancelData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(nopStore, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductManualOrderRecommendationsData(order, nopStoreId, dynamicTemplateData, 3, languageId);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, null,
                toEmail, toName, subject);
        }

        /// <summary>
        /// Sends a new order note added notification to a customer
        /// </summary>
        /// <param name="orderNote">Order note</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendNewOrderNoteAddedCustomerNotification(OrderNote orderNote, int languageId)
        {
            if (orderNote == null)
                throw new ArgumentNullException("orderNote");

            var order = orderNote.Order;

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Customer.NewOrderNote", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderNoteTokens(tokens, orderNote);
            _messageTokenProvider.AddOrderTokens(tokens, orderNote.Order, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, orderNote.Order.Customer);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a "Recurring payment cancelled" notification to a store owner
        /// </summary>
        /// <param name="recurringPayment">Recurring payment</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendRecurringPaymentCancelledStoreOwnerNotification(RecurringPayment recurringPayment, int languageId)
        {
            if (recurringPayment == null)
                throw new ArgumentNullException("recurringPayment");

            var store = _storeService.GetStoreById(recurringPayment.InitialOrder.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("RecurringPaymentCancelled.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddOrderTokens(tokens, recurringPayment.InitialOrder, languageId);
            _messageTokenProvider.AddCustomerTokens(tokens, recurringPayment.InitialOrder.Customer);
            _messageTokenProvider.AddRecurringPaymentTokens(tokens, recurringPayment);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        #endregion

        #region Newsletter workflow

        /// <summary>
        /// Sends a newsletter subscription activation message
        /// </summary>
        /// <param name="subscription">Newsletter subscription</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendNewsLetterSubscriptionActivationMessage(NewsLetterSubscription subscription,
            int languageId)
        {
            if (subscription == null)
                throw new ArgumentNullException("subscription");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("NewsLetterSubscription.ActivationMessage", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddNewsLetterSubscriptionTokens(tokens, subscription);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = subscription.Email;
            var toName = "";
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        #endregion

        #region Send a message to a friend

        /// <summary>
        /// Sends "email a friend" message
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="languageId">Message language identifier</param>
        /// <param name="product">Product instance</param>
        /// <param name="customerEmail">Customer's email</param>
        /// <param name="friendsEmail">Friend's email</param>
        /// <param name="personalMessage">Personal message</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendProductEmailAFriendMessage(Customer customer, int languageId,
            Product product, string customerEmail, string friendsEmail, string personalMessage)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (product == null)
                throw new ArgumentNullException("product");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Service.EmailAFriend", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);
            _messageTokenProvider.AddProductTokens(tokens, product, languageId);
            tokens.Add(new Token("EmailAFriend.PersonalMessage", personalMessage, true));
            tokens.Add(new Token("EmailAFriend.Email", customerEmail));

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = friendsEmail;
            var toName = "";
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends wishlist "email a friend" message
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="languageId">Message language identifier</param>
        /// <param name="customerEmail">Customer's email</param>
        /// <param name="friendsEmail">Friend's email</param>
        /// <param name="personalMessage">Personal message</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendWishlistEmailAFriendMessage(Customer customer, int languageId,
             string customerEmail, string friendsEmail, string personalMessage)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Wishlist.EmailAFriend", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);
            tokens.Add(new Token("Wishlist.PersonalMessage", personalMessage, true));
            tokens.Add(new Token("Wishlist.Email", customerEmail));

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = friendsEmail;
            var toName = "";
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        #endregion

        #region Forum Notifications

        /// <summary>
        /// Sends a forum subscription message to a customer
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="forumTopic">Forum Topic</param>
        /// <param name="forum">Forum</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public int SendNewForumTopicMessage(Customer customer,
            ForumTopic forumTopic, Forum forum, int languageId)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            var store = _storeContext.CurrentStore;

            var messageTemplate = GetActiveMessageTemplate("Forums.NewForumTopic", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddForumTopicTokens(tokens, forumTopic);
            _messageTokenProvider.AddForumTokens(tokens, forumTopic.Forum);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = customer.Email;
            var toName = customer.GetFullName();

            return SendNotification(messageTemplate, emailAccount, languageId, tokens, toEmail, toName);
        }

        /// <summary>
        /// Sends a forum subscription message to a customer
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <param name="forumPost">Forum post</param>
        /// <param name="forumTopic">Forum Topic</param>
        /// <param name="forum">Forum</param>
        /// <param name="friendlyForumTopicPageIndex">Friendly (starts with 1) forum topic page to use for URL generation</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public int SendNewForumPostMessage(Customer customer,
            ForumPost forumPost, ForumTopic forumTopic,
            Forum forum, int friendlyForumTopicPageIndex, int languageId)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            var store = _storeContext.CurrentStore;

            var messageTemplate = GetActiveMessageTemplate("Forums.NewForumPost", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddForumPostTokens(tokens, forumPost);
            _messageTokenProvider.AddForumTopicTokens(tokens, forumPost.ForumTopic,
                friendlyForumTopicPageIndex, forumPost.Id);
            _messageTokenProvider.AddForumTokens(tokens, forumPost.ForumTopic.Forum);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = customer.Email;
            var toName = customer.GetFullName();

            return SendNotification(messageTemplate, emailAccount, languageId, tokens, toEmail, toName);
        }

        /// <summary>
        /// Sends a private message notification
        /// </summary>
        /// <param name="privateMessage">Private message</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public int SendPrivateMessageNotification(PrivateMessage privateMessage, int languageId)
        {
            if (privateMessage == null)
            {
                throw new ArgumentNullException("privateMessage");
            }

            var store = _storeService.GetStoreById(privateMessage.StoreId) ?? _storeContext.CurrentStore;

            var messageTemplate = GetActiveMessageTemplate("Customer.NewPM", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddPrivateMessageTokens(tokens, privateMessage);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = privateMessage.ToCustomer.Email;
            var toName = privateMessage.ToCustomer.GetFullName();

            return SendNotification(messageTemplate, emailAccount, languageId, tokens, toEmail, toName);
        }

        #endregion

        #region Misc

        /// <summary>
        /// Sends a gift card notification
        /// </summary>
        /// <param name="giftCard">Gift card</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendGiftCardNotification(GiftCard giftCard, int languageId)
        {
            if (giftCard == null)
                throw new ArgumentNullException("giftCard");

            Store store = null;
            var order = giftCard.PurchasedWithOrderItem != null ?
                giftCard.PurchasedWithOrderItem.Order :
                null;
            if (order != null)
                store = _storeService.GetStoreById(order.StoreId);
            if (store == null)
                store = _storeContext.CurrentStore;

            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("GiftCard.Notification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddGiftCardTokens(tokens, giftCard);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);
            var toEmail = giftCard.RecipientEmail;
            var toName = giftCard.RecipientName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a product review notification message to a store owner
        /// </summary>
        /// <param name="productReview">Product review</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendProductReviewNotificationMessage(ProductReview productReview,
            int languageId)
        {
            if (productReview == null)
                throw new ArgumentNullException("productReview");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Product.ProductReview", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddProductReviewTokens(tokens, productReview);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a "quantity below" notification to a store owner
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendQuantityBelowStoreOwnerNotification(Product product, int languageId)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("QuantityBelow.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddProductTokens(tokens, product, languageId);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a "quantity below" notification to a store owner
        /// </summary>
        /// <param name="combination">Attribute combination</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendQuantityBelowStoreOwnerNotification(ProductVariantAttributeCombination combination, int languageId)
        {
            if (combination == null)
                throw new ArgumentNullException("combination");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("QuantityBelow.AttributeCombination.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            var product = combination.Product;

            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddProductTokens(tokens, product, languageId);
            _messageTokenProvider.AddAttributeCombinationTokens(tokens, combination, languageId);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a "new VAT sumitted" notification to a store owner
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="vatName">Received VAT name</param>
        /// <param name="vatAddress">Received VAT address</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendNewVatSubmittedStoreOwnerNotification(Customer customer,
            string vatName, string vatAddress, int languageId)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("NewVATSubmitted.StoreOwnerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddCustomerTokens(tokens, customer);
            tokens.Add(new Token("VatValidationResult.Name", vatName));
            tokens.Add(new Token("VatValidationResult.Address", vatAddress));

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a blog comment notification message to a store owner
        /// </summary>
        /// <param name="blogComment">Blog comment</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendBlogCommentNotificationMessage(BlogComment blogComment, int languageId)
        {
            if (blogComment == null)
                throw new ArgumentNullException("blogComment");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Blog.BlogComment", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddBlogCommentTokens(tokens, blogComment);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a news comment notification message to a store owner
        /// </summary>
        /// <param name="newsComment">News comment</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendNewsCommentNotificationMessage(NewsComment newsComment, int languageId)
        {
            if (newsComment == null)
                throw new ArgumentNullException("newsComment");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("News.NewsComment", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            _messageTokenProvider.AddNewsCommentTokens(tokens, newsComment);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAccount.Email;
            var toName = emailAccount.DisplayName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        /// <summary>
        /// Sends a 'Back in stock' notification message to a customer
        /// </summary>
        /// <param name="subscription">Subscription</param>
        /// <param name="languageId">Message language identifier</param>
        /// <returns>Queued email identifier</returns>
        public virtual int SendBackInStockNotification(BackInStockSubscription subscription, int languageId)
        {
            if (subscription == null)
                throw new ArgumentNullException("subscription");

            var store = _storeService.GetStoreById(subscription.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("BackInStock.CustomerNotification", store.Id);

            if (messageTemplate == null)
                return 0;

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = subscription.Customer.Email;
            var toName = subscription.Customer.GetFullName();

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddCustomerData(subscription.Customer, dynamicTemplateData);
            this._messageTokenProvider.AddProductBackInStockData(subscription, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddProductBackInStockRecommendationsData(subscription, dynamicTemplateData, 3, languageId);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, null,
                toEmail, toName, null);
        }

        #endregion

        #region WC

        public virtual int SendCouponNotification(string emailAddress, int languageId)
        {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentNullException("email");

            var store = _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = GetActiveMessageTemplate("Email.SignUp", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            //_messageTokenProvider.AddCustomerTokens(tokens, customer);


            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = emailAddress;
            var toName = string.Empty;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        public int SendShipmentSentCustomerNotification(Shipment shipment, int languageId, bool useBcc, bool askToLeaveReview)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            //var order = shipment.Order;
            //if (order == null)
            //    throw new Exception("Order cannot be loaded");

            //var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            //languageId = EnsureLanguageIsActive(languageId, store.Id);

            //// end date to send many reviews for ResellerRatings
            //var endDate = DateTime.ParseExact("02/28/2017", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //// end date to send ALL reviews for ResellerRatings
            //var allToResellerRatings = DateTime.ParseExact("05/30/2019", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            //MessageTemplate messageTemplate = null;
            //if (DateTime.UtcNow > endDate && (store.Id == 1 || store.Id == 3))
            //{
            //    if (DateTime.UtcNow <= allToResellerRatings)
            //    {
            //        if (store.Id == 1)
            //        {
            //            messageTemplate = this.GetActiveMessageTemplate(askToLeaveReview
            //            ? new[] { "ShipmentSent.CustomerNotification.WithReview",
            //                "ShipmentSent.CustomerNotification.WithReview.ResellerRatings" }[_rnd.Next(0, 1)]
            //            : "ShipmentSent.CustomerNotification", store.Id);
            //        }
            //        else if (store.Id == 3)
            //        {
            //            messageTemplate = this.GetActiveMessageTemplate(askToLeaveReview ? "ShipmentSent.CustomerNotification.WithReview.ResellerRatings" : "ShipmentSent.CustomerNotification", store.Id);
            //        }
            //    }
            //    else
            //    {
            //        messageTemplate = this.GetActiveMessageTemplate(askToLeaveReview
            //        ? new[] { "ShipmentSent.CustomerNotification.WithReview", /*"ShipmentSent.CustomerNotification.WithReview.Yelp",*/ "ShipmentSent.CustomerNotification.WithReview.Google",
            //            "ShipmentSent.CustomerNotification.WithReview.ResellerRatings" }[_rnd.Next(0, 2)]
            //        : "ShipmentSent.CustomerNotification", store.Id);
            //    }
            //}
            //else
            //{
            //    if (store.Id == 1 || store.Id == 3)
            //    {
            //        // Autoplicity
            //        messageTemplate = this.GetActiveMessageTemplate(askToLeaveReview
            //                        ? new[] { "ShipmentSent.CustomerNotification.WithReview", /*"ShipmentSent.CustomerNotification.WithReview.Yelp",*/ "ShipmentSent.CustomerNotification.WithReview.Google",
            //                                    "ShipmentSent.CustomerNotification.WithReview.ResellerRatings", "ShipmentSent.CustomerNotification.WithReview.ResellerRatings", "ShipmentSent.CustomerNotification.WithReview.ResellerRatings" }[_rnd.Next(0, 4)]
            //                        : "ShipmentSent.CustomerNotification", store.Id);
            //    }
            //    else
            //    {
            //        messageTemplate = this.GetActiveMessageTemplate("ShipmentSent.CustomerNotification", store.Id);
            //    }
            //}

            return SendShipmentSentCustomerNotification(shipment, languageId);
        }

        public int SendManualOrderShipment(CrmSalesOrder crmOrder, CrmShipment crmShipment, int languageId)
        {
            if (crmShipment == null)
                throw new ArgumentNullException("shipment");

            if (crmOrder == null)
                throw new Exception("Order cannot be loaded");

            var store = this._storeService.GetStoreById(crmOrder.CrmChannel.Id == 17 ? 1 : 3) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("ShipmentSent.CustomerNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = crmOrder.BillingAddress.Email;
            var toName = string.Format("{0} {1}", crmOrder.BillingAddress.FirstName, crmOrder.BillingAddress.LastName);
            var subject = $"Your order shipped #{crmOrder.Number}";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddManualOrderShipmentData(crmShipment, crmOrder.Id, store.Id, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddManualOrderData(crmOrder, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductManualOrderRecommendationsData(crmOrder, (NopStore)store.Id, dynamicTemplateData, 3, languageId);
            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, fromName,
                toEmail, toName, subject);
        }

        public int SendManualOrderDelivered(CrmSalesOrder crmOrder, CrmShipment crmShipment, int languageId)
        {
            if (crmShipment == null)
                throw new ArgumentNullException("shipment");

            if (crmOrder == null)
                throw new Exception("Order cannot be loaded");

            var store = this._storeService.GetStoreById(crmOrder.CrmChannel.Id == 17 ? 1 : 3) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("ShipmentDelivered.CustomerNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = crmOrder.BillingAddress.Email;
            var toName = string.Format("{0} {1}", crmOrder.BillingAddress.FirstName, crmOrder.BillingAddress.LastName);
            var subject = $"Your order delivered #{crmOrder.Number}";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddManualOrderShipmentData(crmShipment, crmOrder.Id, store.Id, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddManualOrderData(crmOrder, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductManualOrderRecommendationsData(crmOrder, (NopStore)store.Id, dynamicTemplateData, 3, languageId);
            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, fromName,
                toEmail, toName, subject);
        }


        public int SendOrderWithRebatesCustomerNotification(OrderWithRebates orderWithRebates, int languageId)
        {
            if (orderWithRebates == null)
                throw new ArgumentNullException("orderWithRebates");

            var store = _storeContext.CurrentStore;
            var messageTemplate = GetActiveMessageTemplate("Customer.OrderWithRebates", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddOrderWithRebatesTokens(tokens, orderWithRebates);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = orderWithRebates.Email;
            var toName = orderWithRebates.CustomerFullName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        public int SendProductReviewCustomerNotification(OrderProductToReview orderProductToReview, int languageId)
        {
            if (orderProductToReview == null)
                throw new ArgumentNullException("orderProductToReview");

            var store = _storeContext.CurrentStore;
            var messageTemplate = GetActiveMessageTemplate("ProductReview.CustomerNotification", store.Id);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddOrderProductToReviewTokens(tokens, orderProductToReview);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = orderProductToReview.Email;
            var toName = orderProductToReview.CustomerFullName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        public int SendOrderShipmentEtaNotification(OrderShipmentEta orderShipmentEta, int languageId)
        {
            if (orderShipmentEta == null)
                throw new ArgumentNullException("orderShipmentEta");

            var messageTemplate = GetActiveMessageTemplate("OrderETAUpdate.CustomerNotification", orderShipmentEta.StoreId);
            if (messageTemplate == null)
                return 0;

            //email account
            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            //tokens
            var tokens = new List<Token>();
            _messageTokenProvider.AddOrderShipmentEtaTokens(tokens, orderShipmentEta);

            //event notification
            _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

            var toEmail = orderShipmentEta.Email;
            var toName = orderShipmentEta.CustomerFullName;
            return SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        protected virtual int SendNotification(MessageTemplate messageTemplate,
            EmailAccount emailAccount, int languageId, IEnumerable<Token> tokens,
            string toEmailAddress, string toName, bool useBcc,
            string attachmentFilePath = null, string attachmentFileName = null,
            string replyToEmailAddress = null, string replyToName = null)
        {
            //retrieve localized message template data
            var bcc = useBcc ? messageTemplate.GetLocalized(mt => mt.BccEmailAddresses, languageId) : null;
            var subject = messageTemplate.GetLocalized(mt => mt.Subject, languageId);
            var body = messageTemplate.GetLocalized(mt => mt.Body, languageId);

            //Replace subject and body tokens 
            var subjectReplaced = _tokenizer.Replace(subject, tokens, false);
            var bodyReplaced = _tokenizer.Replace(body, tokens, true);

            var email = new QueuedEmail
            {
                Priority = 5,
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = toEmailAddress,
                ToName = toName,
                ReplyTo = replyToEmailAddress,
                ReplyToName = replyToName,
                CC = string.Empty,
                Bcc = bcc,
                Subject = subjectReplaced,
                Body = bodyReplaced,
                AttachmentFilePath = attachmentFilePath,
                AttachmentFileName = attachmentFileName,
                CreatedOnUtc = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            };

            this._queuedEmailService.InsertQueuedEmail(email);
            return email.Id;
        }

        public virtual int SendReturnRequestHelpSupportNotification(string orderNumber, string fullname, string message, string marketplace, string email, string phone, string toEmail, int languageId = 1)
        {
            var store = this._storeContext.CurrentStore;
            languageId = this.EnsureLanguageIsActive(languageId, store.Id);
            var messageTemplate = this.GetActiveMessageTemplate("ReturnRequestHelpNeeded.SupportTeamNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var emailAccount = this.GetEmailAccountOfMessageTemplate(messageTemplate, languageId);
            var tokens = new List<Token>();
            this._messageTokenProvider.AddReturnTokens(tokens, orderNumber, fullname, message, marketplace, email, phone);
            this._eventPublisher.MessageTokensAdded(messageTemplate, tokens);
            var toName = $"{marketplace} Support Team";

            return this.SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                toEmail, toName);
        }

        public virtual int SendSalesQuoteCustomerNotification(SalesQuote quote, int languageId = 0)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            var store = this._storeContext.CurrentStore;
            var messageTemplate = this.GetActiveMessageTemplate("SalesQuoteCreated.CustomerNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var emailAccount = this.GetEmailAccountOfMessageTemplate(messageTemplate, languageId);
            var tokens = new List<Token>();
            this._messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);
            this._messageTokenProvider.AddSalesQuoteTokens(tokens, quote);

            var customerService = EngineContext.Current.Resolve<ICustomerService>();
            var bccEmail = customerService.GetCustomerById(quote.Id)?.Email;

            return this.SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                quote.Email, quote.CustomerName);
        }

        public virtual int SendUpdateOrderDelayedOutstockLowestCostCustomerNotification(Order order, int shippingEta, int languageId = 0)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var messageTemplate = this.GetActiveMessageTemplate("OrderUpdate.DelayedNotInStockLowestCost", order.StoreId);
            if (messageTemplate == null)
            {
                return 0;
            }

            var orderService = EngineContext.Current.Resolve<IOrderService>();
            var storeService = EngineContext.Current.Resolve<IStoreService>();
            var storeName = Enum.GetName(typeof(NopStore), order.StoreId);
            var channel = Convert.ToInt32(Enum.Parse(typeof(Channel), Enum.GetNames(typeof(Channel)).SingleOrDefault(m => m == storeName)));
            var crmOrderId = orderService.GetCrmOrderIdByReference(order.Id.ToString(), channel);
            var crmOrder = orderService.GetCrmOrder(crmOrderId.Value);
            var emailAccount = this.GetEmailAccountOfMessageTemplate(messageTemplate, languageId);
            var store = storeService.GetStoreById(order.StoreId);


            var tokens = new List<Token>();
            this._messageTokenProvider.AddStoreTokens(tokens, storeService.GetStoreById(order.StoreId), emailAccount);
            this._messageTokenProvider.AddOrderTokens(tokens, order, languageId);
            this._messageTokenProvider.AddTopicTokens(tokens, store.Id);
            this._messageTokenProvider.AddShipmentDelayedTokens(tokens, order.OrderItems.ToArray(), crmOrder, shippingEta, store, order.ShippingAddress.Email);

            return this.SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                order.ShippingAddress.Email, order.ShippingAddress.FirstName);
        }

        public virtual int SendOldUpdateOrderDelayedOutstockLowestCostCustomerNotification(Order order, int languageId = 0)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var messageTemplate = this.GetActiveMessageTemplate("OrderUpdate.DelayedNotInStockLowestCost", order.StoreId);
            if (messageTemplate == null)
            {
                return 0;
            }

            var emailAccount = this.GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

            var tokens = new List<Token>();
            this._messageTokenProvider.AddOrderTokens(tokens, order, languageId);

            return this.SendNotification(messageTemplate, emailAccount,
                languageId, tokens,
                order.ShippingAddress.Email, order.ShippingAddress.FirstName);
        }

        public virtual int SendOrderReviewNotification(Order order, int languageId = 0)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var store = this._storeService.GetStoreById(order.StoreId);
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplate = this._sendGridMessageTemplateService.GetMessageTemplateByName("OrderReviewRequest.CustomerNotification", order.StoreId); 
            if (messageTemplate == null)
            {
                return 0;
            }

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.StoreId != 0 ? messageTemplate.Email : messageTemplate.Email + $"@{store.Name.ToLower()}.com";
            var toEmail = order.ShippingAddress.Email;
            var toName = string.Format("{0} {1}", order.ShippingAddress.FirstName, order.ShippingAddress.LastName);
            var subject = "Tell us what you think";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload, payload.GetType(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return SendNotification(messageTemplate, json, languageId, fromEmail, null, toEmail, toName, subject: subject);
        }

        public int SendEbayOrderDeliveryNotification(CrmSalesOrder order, int languageId = 0)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            languageId = EnsureLanguageIsActive(languageId, 0);
            var messageTemplate = this._sendGridMessageTemplateService.GetMessageTemplateByName("EbayOrderDelivered.CustomerNotification", 0);
            if (messageTemplate == null)
            {
                return 0;
            }

            var store = _storeService.GetStoreById((int)Channel.Autoplicity);
            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.Email;
            var toEmail = order.ShippingAddress.Email;
            var toName = string.Format("{0} {1}", order.ShippingAddress.FirstName, order.ShippingAddress.LastName);
            var subject = $"Has your order #{order.Number} been delivered?";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddSalesOrderData(order, dynamicTemplateData, languageId);
            //this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload, payload.GetType(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return SendNotification(messageTemplate, json, languageId, fromEmail, null, toEmail, toName, subject: subject);
        }

        public int SendCustomShipmentDeliveredCustomerNotification(Shipment shipment, int languageId = 0)
        {
            if (shipment == null)
            {
                throw new ArgumentNullException(nameof(shipment));
            }

            var order = shipment.Order;
            if (order == null)
            {
                throw new Exception("Order cannot be loaded");
            }

            var store = this._storeService.GetStoreById(order.StoreId);
            languageId = this.EnsureLanguageIsActive(languageId, store.Id);
            var messageTemplate = this._sendGridMessageTemplateService.GetMessageTemplateByName("ShipmentDelivered.CustomerNotification", store.Id);
            if (messageTemplate == null)
            {
                return 0;
            }

            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.Email;
            var toEmail = order.ShippingAddress.Email;
            var toName = string.Format("{0} {1}", order.ShippingAddress.FirstName, order.ShippingAddress.LastName);
            var subject = $"Your shipment has arrived!";

            var payload = this.GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            //this._messageTokenProvider.AddShipmentData(shipment, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddOrderData(shipment.Order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);

            var json = JsonConvert.SerializeObject(payload, payload.GetType(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return this.SendNotification(messageTemplate, json,
                languageId, fromEmail, null,
                toEmail, toName, subject);
        }

        private Root GetSendGridPayload(string fromName, string fromEmail, string toName, string toEmail, string templateId)
        {
            return new Root
            {
                From = new From
                {
                    Name = fromName,
                    Email = fromEmail
                },
                Filters = new Filters
                {
                    Ganalytics = new Ganalytics
                    {
                        Settings = new Settings
                        {
                            Enable = 0,
                            Source = "Transactional Email",
                            Medium = "email",
                            Content = string.Empty,
                            Campaign = string.Empty
                        }
                    }
                },
                Personalizations = new List<Personalization>
                {
                    new Personalization
                    {
                        To = new List<To>
                        {
                            new To
                            {
                                Email = toEmail,
                                Name = toName
                            }
                        },
                        DynamicTemplateData = new DynamicTemplateData()
                    }
                },
                TemplateId = templateId,
            };
        }

        #endregion

        #endregion

        #region Backorders

        public int SendOosWithEtaCustomerNotification(Order order, int languageId = 0)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            languageId = EnsureLanguageIsActive(languageId, order.StoreId); 
            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("Backorder.OOSWithETA.CustomerNotification", order.StoreId);
            if (messageTemplate == null)
            {
                return 0;
            }

            var store = this._storeService.GetStoreById(order.StoreId);
            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.Email;
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            var subject = $"Back Order Notification for Order #{order.Id}";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);
            this._messageTokenProvider.AddBackorderEtaData(order, dynamicTemplateData, languageId);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, fromName,
                toEmail, toName, subject);
        }

        public int SendUpdatedEtaCustomerNotification(Order order, DateTime eta, int languageId = 0)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            languageId = EnsureLanguageIsActive(languageId, order.StoreId);
            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("Backorder.UpdateETA.CustomerNotification", order.StoreId);
            if (messageTemplate == null)
            {
                return 0;
            }

            var store = this._storeService.GetStoreById(order.StoreId);
            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.Email;
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            var subject = $"Update to your order #{order.Id}";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);
            dynamicTemplateData.OrderETA = eta.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, fromName,
                toEmail, toName, subject);
        }

        public int SendUpdatedEtaNoDateCustomerNotification(Order order, int languageId = 0)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            languageId = EnsureLanguageIsActive(languageId, order.StoreId);
            var messageTemplate = _sendGridMessageTemplateService.GetMessageTemplateByName("Backorder.UpdateETANoDate.CustomerNotification", order.StoreId);
            if (messageTemplate == null)
            {
                return 0;
            }

            var store = this._storeService.GetStoreById(order.StoreId);
            var fromName = store.CompanyName;
            var fromEmail = messageTemplate.Email;
            var toEmail = order.BillingAddress.Email;
            var toName = string.Format("{0} {1}", order.BillingAddress.FirstName, order.BillingAddress.LastName);
            var subject = $"Update to your order #{order.Id}";

            var payload = GetSendGridPayload(fromName, fromEmail, toName, toEmail, messageTemplate.TemplateId);
            var dynamicTemplateData = payload.Personalizations.First().DynamicTemplateData;

            this._messageTokenProvider.AddOrderData(order, dynamicTemplateData, languageId);
            this._messageTokenProvider.AddStoreData(store, dynamicTemplateData, fromEmail);
            this._messageTokenProvider.AddProductRecommendationsData(order, dynamicTemplateData, order.OrderItems.Select(r => r.ProductId).ToArray(), 3, languageId);
            var json = JsonConvert.SerializeObject(payload);

            return SendNotification(messageTemplate, json.ToString(),
                languageId, fromEmail, fromName,
                toEmail, toName, subject);
        }

        #endregion
    }
}
