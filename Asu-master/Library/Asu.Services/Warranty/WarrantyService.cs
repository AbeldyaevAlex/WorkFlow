namespace Asu.Services.Warranty
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Asu.Core;
    using Asu.Core.Data;
    using Asu.Core.Domain.Customers;
    using Asu.Core.Domain.Logging;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Warranty;
    using Asu.Services.Common;
    using Asu.Services.Customization;
    using Asu.Services.Logging;
    using Asu.Services.Orders;

    public class WarrantyService : IWarrantyService
    {
        private readonly IRepository<WarrantyProductAssociation> warrantyRepository;
        private readonly IGenericAttributeService genericAttributeService;
        private readonly IStoreContext storeContext;
        private readonly IWorkContext workContext;
        private readonly IShoppingCartService shoppingCartService;
        private readonly ILogger logger;
        private readonly IShippingInsuranceService shippingInsuranceService;
        private readonly IReturnExtensionService returnExtensionService;

        public WarrantyService(IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IWorkContext workContext,
            IShoppingCartService shoppingCartService,
            IRepository<WarrantyProductAssociation> warrantyRepository,
            ILogger logger,
            IShippingInsuranceService shippingInsuranceService,
            IReturnExtensionService returnExtensionService)
        {
            this.genericAttributeService = genericAttributeService;
            this.storeContext = storeContext;
            this.workContext = workContext;
            this.shoppingCartService = shoppingCartService;
            this.warrantyRepository = warrantyRepository;
            this.logger = logger;
            this.shippingInsuranceService = shippingInsuranceService;
            this.returnExtensionService = returnExtensionService;
        }

        public Dictionary<int, int> Associations => this.workContext.CurrentCustomer.GetAttribute<Dictionary<int, int>>(
                                SystemCustomerAttributeNames.MontlyWarranty,
                                this.storeContext.CurrentStore.Id)
                            ?? new Dictionary<int, int>();

        public void Process(IList<ShoppingCartItem> cart, int productId = 0, int warrantyId = 0)
        {
            var warrantyProductAssociations = this.Associations;
            try
            {
                
                if (productId > 0 && warrantyId > 0)
                {
                    if (!warrantyProductAssociations.ContainsKey(productId))
                    {
                        warrantyProductAssociations.Add(productId, warrantyId);
                    }
                }

                warrantyProductAssociations = warrantyProductAssociations.Join(cart, a => a.Value, b => b.ProductId, (a, b) => a).ToDictionary(a => a.Key, a => a.Value);
                warrantyProductAssociations = warrantyProductAssociations.Join(cart, a => a.Key, b => b.ProductId, (a, b) => a).ToDictionary(a => a.Key, a => a.Value);

                if (warrantyProductAssociations.Any())
                {
                    this.genericAttributeService.SaveAttribute(this.workContext.CurrentCustomer,
                        SystemCustomerAttributeNames.MontlyWarranty,
                        warrantyProductAssociations,
                        this.storeContext.CurrentStore.Id);

                    this.UpdateShoppingCart(cart);
                }
            }
            catch (Exception ex)
            {
                var serialized = CommonHelper.To<string>(warrantyProductAssociations);
                this.logger.InsertLog(LogLevel.Error, $"Error when saving customer warranty attibute. {ex.Message}", serialized, this.workContext.CurrentCustomer);
            }
        }

        public void SaveAssociations(int orderId, ICollection<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                return;
            }

            var warrantyProductAssociations = this.Associations;
            try
            {
                warrantyProductAssociations = warrantyProductAssociations.Join(orderItems, a => a.Key, b => b.ProductId, (a, b) => a).ToDictionary(a => a.Key, a => a.Value);
                var entities = warrantyProductAssociations.Select(association => new WarrantyProductAssociation
                {
                    OrderId = orderId,
                    OrderItemId = orderItems.SingleOrDefault(i => i.ProductId == association.Key)?.Id ?? 0,
                    ProductId = association.Key,
                    WarrantyProductId = association.Value,
                    WarrantyOrderItemId = orderItems.Single(i => i.ProductId == association.Value)?.Id ?? 0
                }).Where(m => m.OrderItemId > 0 && m.WarrantyOrderItemId > 0)
                .ToList();

                this.Save(entities);
            }
            catch (Exception ex)
            {
                var serialized = CommonHelper.To<string>(warrantyProductAssociations);
                this.logger.InsertLog(LogLevel.Error, $"Error when saving customer warranty attibute. {ex.Message}", serialized, this.workContext.CurrentCustomer);
            }
        }

        public void SaveForAmazonPay(int orderId, int newOrderId, ICollection<OrderItem> orderItems)
        {
            if (orderId == 0 || newOrderId == 0 || orderItems == null || !orderItems.Any())
            {
                this.logger.InsertLog(LogLevel.Error, $"Error when saving customer warranty for order paid by 'Amazon Pay'. Order Id: {orderId}; New Order Id: {newOrderId}.");
            }

            var existingWarranties = this.GetByOrderId(orderId);
            this.Save(existingWarranties.Select(i => new WarrantyProductAssociation
            {
                OrderId = newOrderId,
                ProductId = i.ProductId,
                OrderItemId = i.OrderItemId,
                WarrantyOrderItemId = i.WarrantyOrderItemId,
                WarrantyProductId = i.WarrantyProductId
            }).ToList());
        }

        private void UpdateShoppingCart(IList<ShoppingCartItem> cart)
        {
            var customer = this.workContext.CurrentCustomer;
            var warrantyProductAssociations = this.Associations;
            if (warrantyProductAssociations == null)
            {
                return;
            }

            var warranties = cart.Where(m => m.Product?.ProductExtra != null && m.Product.ProductExtra.IsWarranty).Select(m => m).ToList();
            var notAssociated = new List<ShoppingCartItem>();
            foreach (var warranty in warranties)
            {
                var quantity = cart.Join(warrantyProductAssociations.Where(m => m.Value == warranty.ProductId), a => a.ProductId, b => b.Key, (a, b) => a.Quantity).Sum();
                if (quantity < 1)
                {
                    notAssociated.Add(warranty);
                }
                else if (quantity != warranty.Quantity)
                {
                    this.shoppingCartService.UpdateShoppingCartItem(customer, warranty.Id, warranty.AttributesXml, warranty.CustomerEnteredPrice, quantity, true);
                }
            }

            foreach (var warranty in notAssociated)
            {
                this.shoppingCartService.DeleteShoppingCartItem(warranty);
                cart.Remove(warranty);
            }
        }

        public IList<WarrantyProductAssociation> GetByOrderId(int orderId)
        {
            var warranties = from m in this.warrantyRepository.Table
                             where m.OrderId == orderId
                             select m;

            return warranties.ToList();
        }

        public IList<WarrantyProductAssociation> GetAllAssociations()
        {
            var warranties = from m in this.warrantyRepository.Table
                             where !m.UpdatedOn.HasValue
                             select m;

            return warranties.ToList();
        }

        public void Update(WarrantyProductAssociation association)
        {
            if (association == null)
            {
                throw new ArgumentNullException(nameof(association));
            }

            this.warrantyRepository.Update(association);
        }

        private void Save(List<WarrantyProductAssociation> associations)
        {
            if (associations == null || !associations.Any())
            {
                return;
            }

            try
            {
                this.warrantyRepository.Insert(associations);
            }
            catch (Exception ex)
            {
                this.logger.InsertLog(LogLevel.Error, $"Error when saving customer warranty. Save(). {ex.Message}");
            }

            
        }

        public void SaveInsurance(int orderId, ICollection<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                return;
            }

            try
            {
                var insurance = orderItems.FirstOrDefault(oi => this.shippingInsuranceService.IsProductInsurance(oi.Product));
                var entities = orderItems.Where(oi => oi.ProductId != insurance.ProductId).Select(oi => new WarrantyProductAssociation
                {
                    OrderId = orderId,
                    OrderItemId = oi.Id,
                    ProductId = oi.ProductId,
                    WarrantyProductId = insurance.ProductId,
                    WarrantyOrderItemId = insurance.Id
                }).Where(m => m.OrderItemId > 0 && m.WarrantyOrderItemId > 0)
                .ToList();

                this.Save(entities);
            }
            catch (Exception ex)
            {
                this.logger.InsertLog(LogLevel.Error, $"Error when saving package delivery insurance. {ex.Message}", null, this.workContext.CurrentCustomer);
            }
        }

        public void SaveReturnExtension(int orderId, ICollection<OrderItem> orderItems)
        {
            if (orderItems == null)
            {
                return;
            }

            try
            {
                var returnExtension = orderItems.FirstOrDefault(oi => this.returnExtensionService.IsProductReturnExtension(oi.Product));
                var entities = orderItems.Where(oi => oi.ProductId != returnExtension.ProductId).Select(oi => new WarrantyProductAssociation
                {
                    OrderId = orderId,
                    OrderItemId = oi.Id,
                    ProductId = oi.ProductId,
                    WarrantyProductId = returnExtension.ProductId,
                    WarrantyOrderItemId = returnExtension.Id
                }).Where(m => m.OrderItemId > 0 && m.WarrantyOrderItemId > 0)
                .ToList();

                this.Save(entities);
            }
            catch (Exception ex)
            {
                this.logger.InsertLog(LogLevel.Error, $"Error when saving package return extension. {ex.Message}", null, this.workContext.CurrentCustomer);
            }
        }
    }
}
