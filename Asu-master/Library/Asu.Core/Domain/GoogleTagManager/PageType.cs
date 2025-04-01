namespace Asu.Core.Domain.GoogleTagManager
{
    public enum PageType
    {
        Home,
        Product,
        Category,
        Manufacturer,
        Search,
        ShoppingCart,
        BillingAndPayment,
        ShippingAddress,
        CheckoutCompleted,
        PayPalExpress,      // PayPal Express confirmation page (not order completed page)
        Cba,                // Checkout By Amazon page
        CbaCompleted,       // Checkout By Amazon completed
        Login,
        ProductGroup,
        ProductGroupBrand,
        ProductGroupCategory,
        ReturnRequest,
        CheckoutAddress,  // new design
        CheckoutPayment,    // new design
        Vehicle,
        Tires,
        Other,
        CustomerAccount
    }
}
