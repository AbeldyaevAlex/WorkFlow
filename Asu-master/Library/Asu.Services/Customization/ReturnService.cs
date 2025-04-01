using System.Security.Cryptography;
using Asu.Core;
using Asu.Core.Domain.Customers;
using Asu.Services.Orders;

namespace Asu.Services.Customization
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    using System.Web;
    using System.Web.Security;

    using Core.Data;

    using Data;

    using Core.Domain.FreshdeskTickets;
    using Core.Domain.Orders;
    using Core.Domain.Returns;
    using Logging;
    using Messages;

    using Asu.Core.Domain.Logging;

    using IsolationLevel = System.Data.IsolationLevel;
    using Asu.Core.Infrastructure;
    using Asu.Services.Configuration;
    using Asu.Services.Customers;

    public class ReturnService : IReturnService
    {
        private readonly IOrderService orderService;
        private readonly IFreshdeskTicketService freshdeskTicketService;
        private readonly ICustomHelper customHelper;
        private readonly IRepository<ThubOrder> thubOrderRepository;
        private readonly IRepository<CrmSalesOrder> crmOrderRepository;
        private readonly IRepository<ReturnRequest> returnRequestRepository;
        private readonly IRepository<ReturnRequestItem> returnRequestItemRepository;
        private readonly IRepository<ReturnReason> returnReasonRepository;
        private readonly IRepository<ThubOrderItem> thubOrderItemRepository;
        private readonly IRepository<RmaShipment> rmaShipmentRepository;
        private readonly IRepository<Rma> rmaRepository;
        private readonly IWorkContext workContext;
        private readonly IDbContext dbContext;
        private readonly ILogger logger;
        private readonly IWorkflowMessageService workflowMessageService;
        private readonly IRepository<Return> crmReturnRepository;
        private readonly IRepository<CrmRmaShipment> crmRmaShipmentRepository;
        private readonly IRepository<FreshDeskUser> freshDeskUserRepository;
        private readonly FreshdeskSettings freshdeskSettings;
        private readonly IStoreContext storeContext;

        public ReturnService(IStoreContext storeContext,
            IOrderService orderService,
            IFreshdeskTicketService freshdeskTicketService, 
            ICustomHelper customHelper, 
            IRepository<ThubOrder> thubOrderRepository,
            IRepository<CrmSalesOrder> crmOrderRepository,
            IRepository<ReturnRequest> returnRequestRepository, 
            IRepository<ReturnRequestItem> returnRequestItemRepository,
            IRepository<ReturnReason> returnReasonRepository,
            IRepository<Rma> rmaRepository,
            IWorkContext workContext,
            IDbContext dbContext, 
            ILogger logger,
            IWorkflowMessageService workflowMessageService,
            IRepository<ThubOrderItem> thubOrderItemRepository,
            IRepository<RmaShipment> rmaShipmentRepository,
            IRepository<Return> crmReturnRepository,
            IRepository<CrmRmaShipment> crmRmaShipmentRepository,
            IRepository<FreshDeskUser> freshDeskUserRepository,
            FreshdeskSettings freshdeskSettings)
        {
            this.orderService = orderService;
            this.freshdeskTicketService = freshdeskTicketService;
            this.customHelper = customHelper;
            this.thubOrderRepository = thubOrderRepository;
            this.crmOrderRepository = crmOrderRepository;
            this.returnRequestRepository = returnRequestRepository;
            this.returnReasonRepository = returnReasonRepository;
            this.returnRequestItemRepository = returnRequestItemRepository;
            this.workContext = workContext;
            this.dbContext = dbContext;
            this.rmaRepository = rmaRepository;
            this.logger = logger;
            this.workflowMessageService = workflowMessageService;
            this.thubOrderItemRepository = thubOrderItemRepository;
            this.rmaShipmentRepository = rmaShipmentRepository;
            this.crmReturnRepository = crmReturnRepository;
            this.crmRmaShipmentRepository = crmRmaShipmentRepository;
            this.freshDeskUserRepository = freshDeskUserRepository;
            this.freshdeskSettings = freshdeskSettings;
            this.storeContext = storeContext;
        }

        public IList<Return> GetReturns(int crmOrderId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from i in this.crmReturnRepository.Table
                            where i.OrderId == crmOrderId
                            select i;

                var entities = query.ToList();
                //scope.Complete();
                return entities;
            }
        }

        public CrmSalesOrder SearchCrmOrder(string orderNumber, string zip)
        {
            string searchZip;
            if (zip.IndexOf('-') > 0)
            {
                searchZip = zip.Substring(0, zip.IndexOf('-'));
                if (searchZip.Length < 5)
                {
                    searchZip = zip;
                }
            }
            else
            {
                searchZip = zip.Trim();
            }

            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.crmOrderRepository.Table
                            where o.Number == orderNumber && o.ShippingAddress.Zip.StartsWith(searchZip)
                            orderby o.Id descending 
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public IList<RmaShipment> GetRmaShipments(int rmaId)
        {
            var query = from s in this.rmaShipmentRepository.Table
                        where s.RmaId == rmaId
                        select s;

            return query.ToList();
        }

        public IList<CrmShipment> GetCrmRmaShipments(int rmaId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from s in this.crmRmaShipmentRepository.Table
                            where s.RmaId == rmaId
                            select s.Shipment;

                var entities = query.ToList();
                //scope.Complete();
                return entities;
            }
        }

        public ThubOrderItem GetThubOrderItem(long orderItemId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {

                var query = from i in this.thubOrderItemRepository.Table where i.OrderItemId == orderItemId select i;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public Rma GetRma(int id)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {
                var query = from rma in this.rmaRepository.Table
                            where rma.Id == id
                            select rma;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public CrmSalesOrder GetCrmOrderByRma(int rmaId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from r in this.rmaRepository.Table
                            where r.Id == rmaId && r.Return != null
                            join o in this.crmOrderRepository.Table on r.Return.OrderId equals o.Id
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public CrmSalesOrder SearchCrmOrder(int crmOrderId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel =  System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.crmOrderRepository.TableNoTracking
                            where o.Id == crmOrderId
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public ThubOrder GetByOrderReference(string orderReference, int channelId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel =  System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.thubOrderRepository.TableNoTracking
                            where o.DisplayOrderReference == orderReference && o.ChannelId == channelId
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public int? GetCrmOrderIdByOrderReference(string orderReference, int channelId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.crmOrderRepository.TableNoTracking
                            where o.Number == orderReference && o.ChannelId == channelId
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity == null ? (int?)null : entity.Id;
            }
        }

        public List<ReturnRequest> GetReturnRequests(int crmOrderId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from r in this.returnRequestRepository.Table
                            where r.OrderId == crmOrderId
                            select r;
                var entities = query.OrderBy(i => i.CreatedOn).ToList();
                //scope.Complete();
                return entities;
            }
        }

        public ReturnRequest GetReturnRequest(int requestId)
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.returnRequestRepository.Table
                            where o.Id == requestId
                            select o;

                var entity = query.SingleOrDefault();
                //scope.Complete();
                return entity;
            }
        }

        public List<ReturnReason> GetReturnReasons()
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from o in this.returnReasonRepository.TableNoTracking
                            select o;

                var entities = query.ToList();
                //scope.Complete();
                return entities;
            }
        }

        public void CreateCheckOrderCookie(CrmSalesOrder order)
        {
            this.customHelper.AddToCookie("rrauth", Protect(order.Id.ToString(CultureInfo.InvariantCulture), order.ShippingAddress.Email, order.ChannelId.ToString(CultureInfo.InvariantCulture)), DateTime.UtcNow.AddHours(24));
        }

        public bool IsCustomerAuthorized(int crmOrderId, Guid? crmUserId = null, string hash = null)
        {
            var order = this.SearchCrmOrder(crmOrderId);
            if (order == null)
            {
                return false;
            }

            if (this.workContext.CurrentCustomer.IsAdmin()) 
            {
                var orderSettings = EngineContext.Current.Resolve<OrderSettings>();
                var allowedCustomerIds = orderSettings.ReturnRequestAllowedAdmins.Split(',').Select(ac => int.Parse(ac));

                return allowedCustomerIds.Contains(this.workContext.CurrentCustomer.Id);
            }

            if (!this.workContext.CurrentCustomer.IsGuest() && this.workContext.CurrentCustomer.IsRegistered() && order.ChannelId == (int)Channel.Autoplicity)
            {
                int apOrderId;
                if (int.TryParse(order.Number, out apOrderId))
                {
                    var apOrder = this.orderService.GetOrderById(apOrderId);
                    if (apOrder != null && !apOrder.Deleted && apOrder.CustomerId == this.workContext.CurrentCustomer.Id)
                    {
                        this.CreateCheckOrderCookie(order);
                        return true;
                    }
                }
            }

            if (!crmUserId.HasValue && !string.IsNullOrEmpty(hash))
            {
                int apOrderId;
                if (int.TryParse(order.Number, out apOrderId))
                {
                    var apOrder = this.orderService.GetOrderById(apOrderId);
                    if (apOrder != null && !apOrder.Deleted)
                    {
                        if (string.Equals(Sha256Hash($"{order.Id}-{order.Number}-{order.CreatedOn.ToUniversalTime().Ticks}"), hash, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.CreateCheckOrderCookie(order);
                            return true;
                        }
                    }
                }

                return false;
            }

            if (crmUserId.HasValue 
                && !string.IsNullOrEmpty(hash) 
                && string.Equals(Sha256Hash($"{crmOrderId}-{crmUserId.Value.ToString().Replace("-", string.Empty).ToUpper()}").Substring(0, 32), hash, StringComparison.InvariantCultureIgnoreCase)
                && (crmUserId.Value.ToString() == "63FCED37-1B36-4148-8F3F-2A4451F9E461" || crmUserId.Value.ToString() == "8BF8ED3B-AADC-4563-AA85-71AF2BAABA92"
                || crmUserId.Value.ToString() == "6E38BC9B-B0E5-4317-805E-35BDB9F86686" || crmUserId.Value.ToString() == "DD63AF9B-1025-43E4-93B5-0D613F54C537"
                || crmUserId.Value.ToString() == "01B6C247-A363-42DC-8640-7BFE5ECFCCFA" || crmUserId.Value.ToString() == "97DF65BE-1918-45E9-B6D8-B88D361A785B"
                || crmUserId.Value.ToString() == "3C8C01DC-0893-4E9F-9A85-B9790F61B983" || crmUserId.Value.ToString() == "0BFA301B-E70B-44EE-8330-CBFD83ACA4D1"
                || crmUserId.Value.ToString() == "B6C485D9-5C11-4BE8-A599-04EF3DC7B287" || crmUserId.Value.ToString() == "0AFEC028-3771-41B9-8165-0919C95549FF"
                || this.workContext.CurrentCustomer.Id == 1364997339))
            {
                this.CreateCheckOrderCookie(order);
                return true;
            }

            var cookieValue = this.customHelper.GetCookieValue("rrauth");
            if (string.IsNullOrEmpty(cookieValue))
            {
                return false;
            }

            var orderIdString = Unprotect(cookieValue, order.ShippingAddress.Email, order.ChannelId.ToString(CultureInfo.InvariantCulture));
            int orderIdDecrypted;
            if (!int.TryParse(orderIdString, out orderIdDecrypted))
            {
                return false;
            }

            return crmOrderId == orderIdDecrypted;
        }

        public int CreateReturnRequest(ReturnRequest request)
        {
            var returnRequestTable = new DataTable();
            returnRequestTable.Columns.Add("Id");
            returnRequestTable.Columns.Add("OrderId");
            returnRequestTable.Columns.Add("IsManual");
            returnRequestTable.Columns.Add("CrmUserId");
            returnRequestTable.Columns.Add("SiteUserId");

            var dr = returnRequestTable.NewRow();
            dr["Id"] = request.Id;
            dr["OrderId"] = request.OrderId;
            dr["IsManual"] = request.IsManual;
            dr["CrmUserId"] = request.CrmUserId;
            dr["SiteUserId"] = request.SiteUserId;
            returnRequestTable.Rows.Add(dr);

            var returnItemsTable = new DataTable();
            returnItemsTable.Columns.Add("OrderItemId");
            returnItemsTable.Columns.Add("OrderLineId");
            returnItemsTable.Columns.Add("ReturnId");
            returnItemsTable.Columns.Add("Quantity");
            returnItemsTable.Columns.Add("ReasonId");
            returnItemsTable.Columns.Add("ImagePath");
            returnItemsTable.Columns.Add("Comment");

            foreach (var item in request.Items)
            {
                var row = returnItemsTable.NewRow();
                row["OrderItemId"] = item.OrderItemId;
                row["OrderLineId"] = item.LineId;
                row["ReturnId"] = item.ReturnId;
                row["Quantity"] = item.Quantity;
                row["ReasonId"] = item.ReasonId;
                row["ImagePath"] = item.ImagePath;
                row["Comment"] = item.Comment;
                returnItemsTable.Rows.Add(row);
            }

            var returnRequests = new SqlParameter("ReturnRequests", SqlDbType.Structured)
            {
                Value = returnRequestTable,
                TypeName = "ReturnRequests"
            };
            
            var returnItems = new SqlParameter("ReturnItems", SqlDbType.Structured)
            {
                Value = returnItemsTable,
                TypeName = "ReturnItems"
            };

            var returnRequestId = new SqlParameter("ReturnRequestId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            this.dbContext.ExecuteSqlCommand("EXEC WCS_CreateReturnRequest @ReturnRequests, @ReturnItems, @ReturnRequestId OUTPUT", false, null, returnRequests, returnItems, returnRequestId);

            return (int)returnRequestId.Value;
        }

        public void RemoveReturnRequests(int crmOrderId)
        {
            List<ReturnRequest> requests;
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.Serializable
            //}))
            {

                var query = from r in this.returnRequestRepository.Table
                            where r.OrderId == crmOrderId
                            select r;

                requests = query.ToList();
                //scope.Complete();
            }


            foreach (var request in requests)
            {
                while (request.Items.Count > 0)
                {
                    this.returnRequestItemRepository.Delete(request.Items.First());
                }
                
                this.returnRequestRepository.Delete(request);
            }
        }

        private static string Protect(string text,  params string[] purpose)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var stream = Encoding.UTF8.GetBytes(text);
            var encodedValue = MachineKey.Protect(stream, purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        private static string Unprotect(string text, params string[] purpose)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var stream = HttpServerUtility.UrlTokenDecode(text);
            if (stream == null)
            {
                return null;
            }

            try
            {
                var decodedValue = MachineKey.Unprotect(stream, purpose);
                if (decodedValue != null)
                {
                    return Encoding.UTF8.GetString(decodedValue);
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public void CreateFreshdeskTicket(ReturnRequest request)
        {
            try
            {
                var order = this.SearchCrmOrder(request.OrderId);
                var builder = new StringBuilder(string.Format($"Return Request for the order <b>#{order.Number}:</b><br/><br/>"));
                builder.AppendFormat("<b>Marketplace:</b> {0}<br/><br/>", Enum.GetName(typeof(SalesPaymentChargeType), order.Channel));
                foreach (var item in request.Items)
                {
                    var productName = item.OrderLine.Product != null ? $"{item.OrderLine.Product.Name} - {item.OrderLine.Product.ManufacturerPartNumber}" : $"{item.OrderLine.Code}";
                    builder.AppendFormat($"<b>Product:</b> {productName} - qty {item.Quantity}<br/>"); 
                    builder.AppendFormat($"<b>Reason for Return:</b> {item.ReturnReason.Name}<br/>");
                    builder.AppendFormat($"<b>Customer Comments:</b> <i>{item.Comment}</i><br/><br/>");
                }

                var freshDeskAgentId = request.CrmUserId.HasValue 
                    ? this.freshDeskUserRepository.Table.SingleOrDefault(i => i.CrmUserId == request.CrmUserId.Value)?.FreshDeskUserId 
                    : request.SiteUserId.HasValue 
                        ? this.freshDeskUserRepository.Table.SingleOrDefault(i => i.SiteUserId == request.SiteUserId.Value)?.FreshDeskUserId 
                        : null;

                var channel = (Channel)order.ChannelId;
                var createTicket = new CreateTicket
                {
                    Email = order.ShippingAddress.Email,
                    Subject = string.Format($"Return request for the order #{order.Number}"),
                    Description = builder.ToString(),
                    Priority = (int)TicketPriority.Urgent,
                    Status = (int)TicketStatus.Open,
                    GroupId = this.freshdeskTicketService.GetReturnsDepartmentGroupId(channel),
                    EmailSupportId = this.freshdeskTicketService.GetSupportEmailId(channel).ToString(CultureInfo.InvariantCulture),
                    ResponderId = freshDeskAgentId
                };

                var response = this.freshdeskTicketService.CreateTicket(createTicket, channel);
                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    this.logger.InsertLog(LogLevel.Error, $"CreateFreshdeskTicket(): CrmOrderId = {request.OrderId}, Error: {response.ErrorMessage}", builder.ToString());
                    return;
                }

                request.FreshdeskTicketId = response.Ticket.DisplayId;
                this.returnRequestRepository.Update(request);
            }
            catch(Exception ex)
            {
                this.logger.Error(string.Concat("CreateFreshdeskTicket(): ", ex.Message), ex);
            }
        }

        public void SendMailToSupportInbox(string orderNumber, string fullname, string message, string marketplace, string email, string phone, long channelId)
        {
            var supportEmail = this.freshdeskTicketService.GetSupportEmail((Channel)channelId);
            this.workflowMessageService.SendReturnRequestHelpSupportNotification(orderNumber, fullname, message, marketplace, email, phone, supportEmail);
        }

        public List<ReturnRequest> GetFreshdeskTicketsReturnRequests()
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {

                var query = from a in this.returnRequestRepository.Table
                            where !a.FreshdeskTicketId.HasValue
                            select a;

                var entities = query.ToList();
                //scope.Complete();
                return entities;
            }
        }

        private static string Sha256Hash(string value)
        {
            return string.Concat(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)).Select(i => i.ToString("X2")));
        }
    }
}
