namespace Asu.Services.Warranty
{
    using System.Collections.Generic;
    using Asu.Core.Domain.Orders;
    using Asu.Core.Domain.Warranty;

    public interface IWarrantyService
    {
        void Process(IList<ShoppingCartItem> cart, int productId = 0, int warrantyId = 0);

        void SaveAssociations(int orderId, ICollection<OrderItem> orderItems);

        void SaveForAmazonPay(int orderId, int newOrderId, ICollection<OrderItem> orderItems);

        IList<WarrantyProductAssociation> GetByOrderId(int orderId);

        IList<WarrantyProductAssociation> GetAllAssociations();

        void SaveInsurance(int orderId, ICollection<OrderItem> orderItems);

        void SaveReturnExtension(int orderId, ICollection<OrderItem> orderItems);

        void Update(WarrantyProductAssociation association);
    }
}
