using System.Web.Mvc;
using System.Web.Routing;
using Asu.Framework.Localization;
using Asu.Framework.Mvc.Routes;
using Asu.Framework.Seo;

namespace Asu.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //We reordered our routes so the most used ones are on top. It can improve performance.

            //home page
            routes.MapLocalizedRoute("HomePage",
                            "",
                            new { controller = "ImageSlider", action = "FullScreen" },
                            new[] { "Asu.Web.Controllers" });
            //home page
            routes.MapLocalizedRoute("CreateUserRole",
                            "",
                            new { controller = "UsersRoleMapping", action = "CreateUserRoleMapping" },
                            new[] { "Asu.Web.Controllers" });
            //widgets
            //we have this route for performance optimization because named routes are MUCH faster than usual Html.Action(...)
            //and this route is highly used
            routes.MapRoute("WidgetsByZone",
                            "widgetsbyzone/",
                            new { controller = "Widget", action = "WidgetsByZone" },
                            new[] { "Asu.Web.Controllers" });

            //login
            routes.MapLocalizedRoute("Login",
                            "login/",
                            new { controller = "Customer", action = "Login" },
                            new[] { "Asu.Web.Controllers" });
            //register
            routes.MapLocalizedRoute("Register",
                            "register/",
                            new { controller = "Customer", action = "Register" },
                            new[] { "Asu.Web.Controllers" });
            //logout
            routes.MapLocalizedRoute("Logout",
                            "logout/",
                            new { controller = "Customer", action = "Logout" },
                            new[] { "Asu.Web.Controllers" });
            //Address
            routes.MapLocalizedRoute("Addresses",
                            "addresses/",
                            new { controller = "Customer", action = "Addresses" },
                            new[] { "Asu.Web.Controllers" });

            //shopping cart
            routes.MapLocalizedRoute("ShoppingCart",
                            "cart/",
                            new { controller = "ShoppingCart", action = "Cart" },
                            new[] { "Asu.Web.Controllers" });
            //wishlist
            routes.MapLocalizedRoute("Wishlist",
                            "wishlist/{customerGuid}",
                            new { controller = "ShoppingCart", action = "Wishlist", customerGuid = UrlParameter.Optional },
                            new[] { "Asu.Web.Controllers" });

            //customer account links
            routes.MapLocalizedRoute("CustomerInfo",
                            "customer/info",
                            new { controller = "Customer", action = "Info" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerAddresses",
                            "customer/addresses",
                            new { controller = "Customer", action = "Addresses" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerOrders",
                            "order/history",
                            new { controller = "Order", action = "CustomerOrders" },
                            new[] { "Asu.Web.Controllers" });

            //contact us
            routes.MapLocalizedRoute("ContactUs",
                            "contactus",
                            new { controller = "Common", action = "ContactUs" },
                            new[] { "Asu.Web.Controllers" });
            //sitemap
            routes.MapLocalizedRoute("Sitemap",
                            "site-map",
                            new { controller = "Common", action = "Sitemap" },
                            new[] { "Asu.Web.Controllers" });

            //product search
            /*routes.MapLocalizedRoute("ProductSearch",
                            "search/",
                            new { controller = "Catalog", action = "Search" },
                            new[] { "Nop.Web.Controllers" });*/
            routes.MapLocalizedRoute("ProductSearch",
                            "search/",
                            new { controller = "Vehicle", action = "SearchPage" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("FilterSearch",
                            "filtersearch/",
                            new { controller = "Vehicle", action = "FilterSearch" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ProductSearchAutoComplete",
                            "catalog/searchtermautocomplete",
                            new { controller = "Catalog", action = "SearchTermAutoComplete" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("Tires",
                            "tires/",
                            new { controller = "Vehicle", action = "Tires" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("VehicleCategory",
                "veh/{slug}/{categoryId}/{makeId}/{modelId}/{yearId}",
                new { controller = "Vehicle", action = "VehicleCategory" },
                new[] { "Asu.Web.Controllers" });

            //change currency (AJAX link)
            routes.MapLocalizedRoute("ChangeCurrency",
                            "changecurrency/{customercurrency}",
                            new { controller = "Common", action = "SetCurrency" },
                            new { customercurrency = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //change language (AJAX link)
            routes.MapLocalizedRoute("ChangeLanguage",
                            "changelanguage/{langid}",
                            new { controller = "Common", action = "SetLanguage" },
                            new { langid = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //change tax (AJAX link)
            routes.MapLocalizedRoute("ChangeTaxType",
                            "changetaxtype/{customertaxtype}",
                            new { controller = "Common", action = "SetTaxType" },
                            new { customertaxtype = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //recently viewed products
            routes.MapLocalizedRoute("RecentlyViewedProducts",
                            "recentlyviewedproducts/",
                            new { controller = "Product", action = "RecentlyViewedProducts" },
                            new[] { "Asu.Web.Controllers" });
            //recently added products
            routes.MapLocalizedRoute("RecentlyAddedProducts",
                            "newproducts/",
                            new { controller = "Product", action = "RecentlyAddedProducts" },
                            new[] { "Asu.Web.Controllers" });
            //blog
            routes.MapLocalizedRoute("Blog",
                            "blog",
                            new { controller = "Blog", action = "List" },
                            new[] { "Asu.Web.Controllers" });
            //news
            routes.MapLocalizedRoute("NewsArchive",
                            "news",
                            new { controller = "News", action = "List" },
                            new[] { "Asu.Web.Controllers" });

            //forum
            routes.MapLocalizedRoute("Boards",
                            "boards",
                            new { controller = "Boards", action = "Index" },
                            new[] { "Asu.Web.Controllers" });

            //compare products
            routes.MapLocalizedRoute("CompareProducts",
                            "compareproducts/",
                            new { controller = "Product", action = "CompareProducts" },
                            new[] { "Asu.Web.Controllers" });

            //product tags
            routes.MapLocalizedRoute("ProductTagsAll",
                            "producttag/all/",
                            new { controller = "Catalog", action = "ProductTagsAll" },
                            new[] { "Asu.Web.Controllers" });

            //manufacturers
            routes.MapLocalizedRoute("ManufacturerList",
                            "manufacturer/all/",
                            new { controller = "Catalog", action = "ManufacturerAll" },
                            new[] { "Asu.Web.Controllers" });
            //vendors
            routes.MapLocalizedRoute("VendorList",
                            "vendor/all/",
                            new { controller = "Catalog", action = "VendorAll" },
                            new[] { "Asu.Web.Controllers" });


            //add product to cart (without any attributes and options). used on catalog pages.
            routes.MapLocalizedRoute("AddProductToCart-Catalog",
                            "addproducttocart/catalog/{productId}/{shoppingCartTypeId}/{quantity}",
                            new { controller = "ShoppingCart", action = "AddProductToCart_Catalog" },
                            new { productId = @"\d+", shoppingCartTypeId = @"\d+", quantity = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //add product to cart (with attributes and options). used on the product details pages.
            routes.MapLocalizedRoute("AddProductToCart-Details",
                            "addproducttocart/details/{productId}/{shoppingCartTypeId}",
                            new { controller = "ShoppingCart", action = "AddProductToCart_Details" },
                            new { productId = @"\d+", shoppingCartTypeId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //product tags
            routes.MapLocalizedRoute("ProductsByTag",
                            "producttag/{productTagId}/{SeName}",
                            new { controller = "Catalog", action = "ProductsByTag", SeName = UrlParameter.Optional },
                            new { productTagId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //comparing products
            routes.MapLocalizedRoute("AddProductToCompare",
                            "compareproducts/add/{productId}",
                            new { controller = "Product", action = "AddProductToCompareList" },
                            new { productId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //product email a friend
            routes.MapLocalizedRoute("ProductEmailAFriend",
                            "productemailafriend/{productId}",
                            new { controller = "Product", action = "ProductEmailAFriend" },
                            new { productId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //reviews
            routes.MapLocalizedRoute("ProductReviews",
                            "productreviews/{productId}",
                            new { controller = "Product", action = "ProductReviews" },
                            new[] { "Asu.Web.Controllers" });
            //back in stock notifications
            routes.MapLocalizedRoute("BackInStockSubscribePopup",
                            "backinstocksubscribe/{productId}",
                            new { controller = "BackInStockSubscription", action = "SubscribePopup" },
                            new { productId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //downloads
            routes.MapRoute("GetSampleDownload",
                            "download/sample/{productid}",
                            new { controller = "Download", action = "Sample" },
                            new { productid = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            #region Checkout

            /*routes.MapLocalizedRoute("Checkout",
                            "checkout/",
                            new { controller = "Checkout", action = "Index" },
                            new[] { "Nop.Web.Controllers" });*/
            routes.MapLocalizedRoute("CheckoutOnePage",
                            "onepagecheckout/",
                            new { controller = "Checkout", action = "OnePageCheckout" },
                            new[] { "Asu.Web.Controllers" });
            /*routes.MapLocalizedRoute("CheckoutShippingAddress",
                            "checkout/shippingaddress",
                            new { controller = "Checkout", action = "ShippingAddress" },
                            new[] { "Nop.Web.Controllers" });*/
            routes.MapLocalizedRoute("CheckoutSelectShippingAddress",
                            "checkout/selectshippingaddress",
                            new { controller = "Checkout", action = "SelectShippingAddress" },
                            new[] { "Asu.Web.Controllers" });
            /*routes.MapLocalizedRoute("CheckoutBillingAddress",
                            "checkout/billingaddress",
                            new { controller = "Checkout", action = "BillingAddress" },
                            new[] { "Nop.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutSelectBillingAddress",
                            "checkout/selectbillingaddress",
                            new { controller = "Checkout", action = "SelectBillingAddress" },
                            new[] { "Nop.Web.Controllers" });*/
            routes.MapLocalizedRoute("CheckoutShippingMethod",
                            "checkout/shippingmethod",
                            new { controller = "Checkout", action = "ShippingMethod" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutPaymentMethod",
                            "checkout/paymentmethod",
                            new { controller = "Checkout", action = "PaymentMethod" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutPaymentInfo",
                            "checkout/paymentinfo",
                            new { controller = "Checkout", action = "PaymentInfo" },
                            new[] { "Asu.Web.Controllers" });

            /*routes.MapLocalizedRoute("CheckoutConfirm",
                            "checkout/confirm",
                            new { controller = "Checkout", action = "Confirm" },
                            new[] { "Nop.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutCompleted",
                            "checkout/completed/{orderId}",
                            new { controller = "Checkout", action = "Completed", orderId = UrlParameter.Optional },
                            new { orderId = @"\d+" },
                            new[] { "Nop.Web.Controllers" });*/

            routes.MapLocalizedRoute("SimpleCheckoutApplePay",
              "checkout/applepay",
              new { controller = "SimpleCheckout", action = "ApplePayCheckout" },
              new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckout",
                            "checkout/",
                            new { controller = "SimpleCheckout", action = "Checkout" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutAddress",
                            "checkout/address",
                            new { controller = "SimpleCheckout", action = "CustomerAddress" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutPayment",
                            "checkout/payment",
                            new { controller = "SimpleCheckout", action = "Payment" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutEditAddress",
                           "checkout/edit-address/{id}",
                           new { controller = "SimpleCheckout", action = "EditAddress" },
                           new[] { "Asu.Web.Controllers" }); 

            routes.MapLocalizedRoute("SimpleCheckoutCreateAddressBilling",
                           "checkout/create-address-billing",
                           new { controller = "SimpleCheckout", action = "CreateAddressBilling" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutCreateAddressShipping",
                           "checkout/create-address-shipping",
                           new { controller = "SimpleCheckout", action = "CreateAddressShipping" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutEditBillingAddress",
                           "checkout/save-billing-address",
                           new { controller = "SimpleCheckout", action = "SaveBillingAddress" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutEditShippingAddress",
                           "checkout/save-shipping-address",
                           new { controller = "SimpleCheckout", action = "SaveShippingAddress" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutSetPaymentMethod",
                           "checkout/set-payment-method",
                           new { controller = "SimpleCheckout", action = "SetPaymentMethod" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutSetAddress",
                           "checkout/set-address/{id}",
                           new { controller = "SimpleCheckout", action = "SetAddress" },
                           new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutLogin",
                          "checkout/login",
                          new { controller = "SimpleCheckout", action = "Login" },
                          new[] { "Asu.Web.Controllers" }); 

            routes.MapLocalizedRoute("SimpleCheckoutSaveAddress",
                          "checkout/save-address",
                          new { controller = "SimpleCheckout", action = "SaveAddress" },
                          new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutSetPaymentInfo",
                          "checkout/set-payment-info",
                          new { controller = "SimpleCheckout", action = "SetPaymentInfo" },
                          new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutConfirmOrder",
                         "checkout/confirm-order",
                         new { controller = "SimpleCheckout", action = "ConfirmOrder" },
                         new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutAddresses",
                         "checkout/addresses/{type}/{IsInline}",
                         new { controller = "SimpleCheckout", action = "Address" },
                         new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutAmazonPay",
                       "checkout/amazon",
                       new { controller = "SimpleCheckout", action = "AmazonCheckout" },
                       new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutLwa",
                            "checkout/lwa",
                            new { controller = "SimpleCheckout", action = "Lwa" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutUpdateShipping",
                          "checkout/update-shipping-options",
                          new { controller = "SimpleCheckout", action = "UpdateShippingOptions" },
                          new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("SimpleCheckoutAmazonComplete",
                         "checkout/amazon/complete",
                         new { controller = "SimpleCheckout", action = "AmazonCheckoutComplete" },
                         new[] { "Asu.Web.Controllers" });


            routes.MapLocalizedRoute("SimpleCheckoutValidateCaptcha",
                "checkout/validatecaptcha",
                new { controller = "SimpleCheckout", action = "ValidateCaptcha" },
                new[] { "Asu.Web.Controllers" });

            #endregion

            #region WC

            routes.MapLocalizedRoute("Checkout",
                            "checkout/",
                            new { controller = "CustomCheckout", action = "Index" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutBillingAddress",
                            "checkout/shippingaddress",
                            new { controller = "CustomCheckout", action = "BillingAddress" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutSelectBillingAddress",
                            "checkout/selectbillingaddress",
                            new { controller = "CustomCheckout", action = "SelectExistBillingAddress" },
                            new[] { "Asu.Web.Controllers" });
            //routes.MapLocalizedRoute("CheckoutShippingAndPayment",
            //                "checkout/billingpayment",
            //                new { controller = "CustomCheckout", action = "ShippingAndPayment" },
            //                new[] { "Nop.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutConfirm",
                            "checkout/confirm",
                            new { controller = "CustomCheckout", action = "Confirm" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CheckoutCompleted",
                            "checkout/completed/{orderId}",
                            new { controller = "CustomCheckout", action = "Completed", orderId = UrlParameter.Optional },
                            new { orderId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //routes.MapLocalizedRoute("ImageLoader",
            //                "imageloader/{productId}",
            //                new { controller = "Customization", action = "ImageLoader" },
            //                new[] { "Nop.Web.Controllers" });

            routes.MapLocalizedRoute("RestoreShoppingCart", 
                            "shoppingcart/restore/{customerGuid}",
                            new { controller = "Customization", action = "RestoreShoppingCart" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ReturnRequestAuth",
                            "return/",
                            new { controller = "ReturnRequest", action = "Return" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("CustomReturnRequest",
                            "return/request/{crmOrderId}",
                            new { controller = "ReturnRequest", action = "ReturnRequest", crmOrderId = UrlParameter.Optional },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("RemoveReturnRequests",
                            "return/remove/{crmOrderId}",
                            new { controller = "ReturnRequest", action = "RemoveReturnRequests" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ReturnRequestSummary",
                            "return/summary/{requestId}",
                            new { controller = "ReturnRequest", action = "ReturnRequestSummary", requestId = UrlParameter.Optional },
                            new[] { "Asu.Web.Controllers" });


            routes.MapLocalizedRoute("SaveRmaShipment",
                            "return/savetracking",
                            new { controller = "ReturnRequest", action = "SaveRmaShipment"},
                            new[] { "Asu.Web.Controllers" });

            //routes.MapLocalizedRoute("CustomerMembersClub",
            //             "customer/members-club",
            //             new { controller = "Customer", action = "MembersClub" },
            //             new[] { "Nop.Web.Controllers" });

            #endregion

            //subscribe newsletters
            routes.MapLocalizedRoute("SubscribeNewsletter",
                            "subscribenewsletter",
                            new { controller = "Newsletter", action = "SubscribeNewsletter" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("FollowSubscribeNewsletter",
                     "followsubscribenewsletter",
                     new { controller = "Newsletter", action = "FollowSubscribeNewsletter" },
                     new[] { "Asu.Web.Controllers" });

            //email wishlist
            routes.MapLocalizedRoute("EmailWishlist",
                            "emailwishlist",
                            new { controller = "ShoppingCart", action = "EmailWishlist" },
                            new[] { "Asu.Web.Controllers" });

            //login page for checkout as guest
            routes.MapLocalizedRoute("LoginCheckoutAsGuest",
                            "login/checkoutasguest",
                            new { controller = "Customer", action = "Login", checkoutAsGuest = true },
                            new[] { "Asu.Web.Controllers" });
            //register result page
            routes.MapLocalizedRoute("RegisterResult",
                            "registerresult/{resultId}",
                            new { controller = "Customer", action = "RegisterResult" },
                            new { resultId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //check username availability
            routes.MapLocalizedRoute("CheckUsernameAvailability",
                            "customer/checkusernameavailability",
                            new { controller = "Customer", action = "CheckUsernameAvailability" },
                            new[] { "Asu.Web.Controllers" });

            //passwordrecovery
            routes.MapLocalizedRoute("PasswordRecovery",
                            "passwordrecovery",
                            new { controller = "Customer", action = "PasswordRecovery" },
                            new[] { "Asu.Web.Controllers" });
            //password recovery confirmation
            routes.MapLocalizedRoute("PasswordRecoveryConfirm",
                            "passwordrecovery/confirm",
                            new { controller = "Customer", action = "PasswordRecoveryConfirm" },                            
                            new[] { "Asu.Web.Controllers" });

            //topics
            routes.MapLocalizedRoute("TopicPopup",
                            "t-popup/{SystemName}",
                            new { controller = "Topic", action = "TopicDetailsPopup" },
                            new[] { "Asu.Web.Controllers" });
            
            //blog
            routes.MapLocalizedRoute("BlogByTag",
                            "blog/tag/{tag}",
                            new { controller = "Blog", action = "BlogByTag" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("BlogByMonth",
                            "blog/month/{month}",
                            new { controller = "Blog", action = "BlogByMonth" },
                            new[] { "Asu.Web.Controllers" });
            //blog RSS
            routes.MapLocalizedRoute("BlogRSS",
                            "blog/rss/{languageId}",
                            new { controller = "Blog", action = "ListRss" },
                            new { languageId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //news RSS
            routes.MapLocalizedRoute("NewsRSS",
                            "news/rss/{languageId}",
                            new { controller = "News", action = "ListRss" },
                            new { languageId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //set review helpfulness (AJAX link)
            routes.MapRoute("SetProductReviewHelpfulness",
                            "setproductreviewhelpfulness",
                            new { controller = "Product", action = "SetProductReviewHelpfulness" },
                            new[] { "Asu.Web.Controllers" });

            //customer account links
            routes.MapLocalizedRoute("CustomerReturnRequests",
                            "returnrequest/history",
                            new { controller = "ReturnRequest", action = "CustomerReturnRequests" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerDownloadableProducts",
                            "customer/downloadableproducts",
                            new { controller = "Customer", action = "DownloadableProducts" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerBackInStockSubscriptions",
                            "backinstocksubscriptions/manage",
                            new { controller = "BackInStockSubscription", action = "CustomerSubscriptions" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerBackInStockSubscriptionsPaged",
                            "backinstocksubscriptions/manage/{page}",
                            new { controller = "BackInStockSubscription", action = "CustomerSubscriptions", page = UrlParameter.Optional },
                            new { page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerRewardPoints",
                            "rewardpoints/history",
                            new { controller = "Order", action = "CustomerRewardPoints" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerChangePassword",
                            "customer/changepassword",
                            new { controller = "Customer", action = "ChangePassword" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerAvatar",
                            "customer/avatar",
                            new { controller = "Customer", action = "Avatar" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("AccountActivation",
                            "customer/activation",
                            new { controller = "Customer", action = "AccountActivation" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerForumSubscriptions",
                            "boards/forumsubscriptions",
                            new { controller = "Boards", action = "CustomerForumSubscriptions" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerForumSubscriptionsPaged",
                            "boards/forumsubscriptions/{page}",
                            new { controller = "Boards", action = "CustomerForumSubscriptions", page = UrlParameter.Optional },
                            new { page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerAddressDelete",
                            "customer/addressdelete/{addressId}",
                            new { controller = "Customer", action = "AddressDelete" },
                            new { addressId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerAddressEdit",
                            "customer/addressedit/{addressId}",
                            new { controller = "Customer", action = "AddressEdit" },
                            new { addressId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerAddressAdd",
                            "customer/addressadd",
                            new { controller = "Customer", action = "AddressAdd" },
                            new[] { "Asu.Web.Controllers" });
            //customer profile page
            routes.MapLocalizedRoute("CustomerProfile",
                            "profile/{id}",
                            new { controller = "Profile", action = "Index" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("CustomerProfilePaged",
                            "profile/{id}/page/{page}",
                            new { controller = "Profile", action = "Index" },
                            new { id = @"\d+", page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //orders
            routes.MapLocalizedRoute("OrderDetails",
                            "orderdetails/{orderId}/{hash}",
                            new { controller = "Order", action = "Details", hash = UrlParameter.Optional },
                            new { orderId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ShipmentDetails",
                            "shipment/{orderId}/{shipmentId}",
                            new { controller = "Order", action = "ShipmentDetails" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ReturnRequest",
                            "returnrequest/{orderId}",
                            new { controller = "ReturnRequest", action = "ReturnRequest" },
                            new { orderId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ReOrder",
                            "reorder/{orderId}",
                            new { controller = "Order", action = "ReOrder" },
                            new { orderId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("GetOrderPdfInvoice",
                            "orderdetails/pdf/{orderId}",
                            new { controller = "Order", action = "GetPdfInvoice" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PrintOrderDetails",
                            "orderdetails/print/{orderId}",
                            new { controller = "Order", action = "PrintOrderDetails" },
                            new[] { "Asu.Web.Controllers" });
            //order downloads
            routes.MapRoute("GetDownload",
                            "download/getdownload/{orderItemId}/{agree}",
                            new { controller = "Download", action = "GetDownload", agree = UrlParameter.Optional },
                            new { orderItemId = new GuidConstraint(false) },
                            new[] { "Asu.Web.Controllers" });
            routes.MapRoute("GetLicense",
                            "download/getlicense/{orderItemId}/",
                            new { controller = "Download", action = "GetLicense" },
                            new { orderItemId = new GuidConstraint(false) },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("DownloadUserAgreement",
                            "customer/useragreement/{orderItemId}",
                            new { controller = "Customer", action = "UserAgreement" },
                            new { orderItemId = new GuidConstraint(false) },
                            new[] { "Asu.Web.Controllers" });
            routes.MapRoute("GetOrderNoteFile",
                            "download/ordernotefile/{ordernoteid}",
                            new { controller = "Download", action = "GetOrderNoteFile" },
                            new { ordernoteid = @"\d+" },
                            new[] { "Asu.Web.Controllers" });


            //poll vote AJAX link
            routes.MapLocalizedRoute("PollVote",
                            "poll/vote",
                            new { controller = "Poll", action = "Vote" },
                            new[] { "Asu.Web.Controllers" });

            //comparing products
            routes.MapLocalizedRoute("RemoveProductFromCompareList",
                            "compareproducts/remove/{productId}",
                            new { controller = "Product", action = "RemoveProductFromCompareList" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ClearCompareList",
                            "clearcomparelist/",
                            new { controller = "Product", action = "ClearCompareList" },
                            new[] { "Asu.Web.Controllers" });

            //recently added products RSS
            routes.MapLocalizedRoute("RecentlyAddedProductsRSS",
                            "newproducts/rss",
                            new { controller = "Product", action = "RecentlyAddedProductsRss" },
                            new[] { "Asu.Web.Controllers" });
            
            //get state list by country ID  (AJAX link)
            routes.MapRoute("GetStatesByCountryId",
                            "country/getstatesbycountryid/",
                            new { controller = "Country", action = "GetStatesByCountryId" },
                            new[] { "Asu.Web.Controllers" });

            //EU Cookie law accept button handler (AJAX link)
            routes.MapRoute("EuCookieLawAccept",
                            "eucookielawaccept",
                            new { controller = "Common", action = "EuCookieLawAccept" },
                            new[] { "Asu.Web.Controllers" });

            //authenticate topic AJAX link
            routes.MapLocalizedRoute("TopicAuthenticate",
                            "topic/authenticate",
                            new { controller = "Topic", action = "Authenticate" },
                            new[] { "Asu.Web.Controllers" });

            //product attributes with "upload file" type
            routes.MapLocalizedRoute("UploadFileProductAttribute",
                            "uploadfileproductattribute/{attributeId}",
                            new { controller = "ShoppingCart", action = "UploadFileProductAttribute" },
                            new { attributeId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            //checkout attributes with "upload file" type
            routes.MapLocalizedRoute("UploadFileCheckoutAttribute",
                            "uploadfilecheckoutattribute/{attributeId}",
                            new { controller = "ShoppingCart", action = "UploadFileCheckoutAttribute" },
                            new { attributeId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            
            //forums
            routes.MapLocalizedRoute("ActiveDiscussions",
                            "boards/activediscussions",
                            new { controller = "Boards", action = "ActiveDiscussions" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ActiveDiscussionsRSS",
                            "boards/activediscussionsrss",
                            new { controller = "Boards", action = "ActiveDiscussionsRSS" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PostEdit",
                            "boards/postedit/{id}",
                            new { controller = "Boards", action = "PostEdit" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PostDelete",
                            "boards/postdelete/{id}",
                            new { controller = "Boards", action = "PostDelete" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PostCreate",
                            "boards/postcreate/{id}",
                            new { controller = "Boards", action = "PostCreate" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PostCreateQuote",
                            "boards/postcreate/{id}/{quote}",
                            new { controller = "Boards", action = "PostCreate" },
                            new { id = @"\d+", quote = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicEdit",
                            "boards/topicedit/{id}",
                            new { controller = "Boards", action = "TopicEdit" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicDelete",
                            "boards/topicdelete/{id}",
                            new { controller = "Boards", action = "TopicDelete" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicCreate",
                            "boards/topiccreate/{id}",
                            new { controller = "Boards", action = "TopicCreate" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicMove",
                            "boards/topicmove/{id}",
                            new { controller = "Boards", action = "TopicMove" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicWatch",
                            "boards/topicwatch/{id}",
                            new { controller = "Boards", action = "TopicWatch" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicSlug",
                            "boards/topic/{id}/{slug}",
                            new { controller = "Boards", action = "Topic", slug = UrlParameter.Optional },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("TopicSlugPaged",
                            "boards/topic/{id}/{slug}/page/{page}",
                            new { controller = "Boards", action = "Topic", slug = UrlParameter.Optional, page = UrlParameter.Optional },
                            new { id = @"\d+", page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ForumWatch",
                            "boards/forumwatch/{id}",
                            new { controller = "Boards", action = "ForumWatch" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ForumRSS",
                            "boards/forumrss/{id}",
                            new { controller = "Boards", action = "ForumRSS" },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ForumSlug",
                            "boards/forum/{id}/{slug}",
                            new { controller = "Boards", action = "Forum", slug = UrlParameter.Optional },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ForumSlugPaged",
                            "boards/forum/{id}/{slug}/page/{page}",
                            new { controller = "Boards", action = "Forum", slug = UrlParameter.Optional, page = UrlParameter.Optional },
                            new { id = @"\d+", page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ForumGroupSlug",
                            "boards/forumgroup/{id}/{slug}",
                            new { controller = "Boards", action = "ForumGroup", slug = UrlParameter.Optional },
                            new { id = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("Search",
                            "boards/search",
                            new { controller = "Boards", action = "Search" },
                            new[] { "Asu.Web.Controllers" });

            //private messages
            routes.MapLocalizedRoute("PrivateMessages",
                            "privatemessages/{tab}",
                            new { controller = "PrivateMessages", action = "Index", tab = UrlParameter.Optional },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PrivateMessagesPaged",
                            "privatemessages/{tab}/page/{page}",
                            new { controller = "PrivateMessages", action = "Index", tab = UrlParameter.Optional },
                            new { page = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PrivateMessagesInbox",
                            "inboxupdate",
                            new { controller = "PrivateMessages", action = "InboxUpdate" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("PrivateMessagesSent",
                            "sentupdate",
                            new { controller = "PrivateMessages", action = "SentUpdate" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("SendPM",
                            "sendpm/{toCustomerId}",
                            new { controller = "PrivateMessages", action = "SendPM" },
                            new { toCustomerId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("SendPMReply",
                            "sendpm/{toCustomerId}/{replyToMessageId}",
                            new { controller = "PrivateMessages", action = "SendPM" },
                            new { toCustomerId = @"\d+", replyToMessageId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("ViewPM",
                            "viewpm/{privateMessageId}",
                            new { controller = "PrivateMessages", action = "ViewPM" },
                            new { privateMessageId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapLocalizedRoute("DeletePM",
                            "deletepm/{privateMessageId}",
                            new { controller = "PrivateMessages", action = "DeletePM" },
                            new { privateMessageId = @"\d+" },
                            new[] { "Asu.Web.Controllers" });

            //activate newsletters
            routes.MapLocalizedRoute("NewsletterActivation",
                            "newsletter/subscriptionactivation/{token}/{active}",
                            new { controller = "Newsletter", action = "SubscriptionActivation" },
                            new { token = new GuidConstraint(false) },
                            new[] { "Asu.Web.Controllers" });

            //robots.txt
            routes.MapRoute("robots.txt",
                            "robots.txt",
                            new { controller = "Common", action = "RobotsTextFile" },
                            new[] { "Asu.Web.Controllers" });

            //sitemap (XML)
            routes.MapLocalizedRoute("sitemap.xml",
                            "sitemap.xml",
                            new { controller = "Common", action = "SitemapXml" },
                            new[] { "Asu.Web.Controllers" });

            //store closed
            routes.MapLocalizedRoute("StoreClosed",
                            "storeclosed",
                            new { controller = "Common", action = "StoreClosed" },
                            new[] { "Asu.Web.Controllers" });

            //install
            routes.MapRoute("Installation",
                            "install",
                            new { controller = "Install", action = "Index" },
                            new[] { "Asu.Web.Controllers" });
            
            //page not found
            routes.MapLocalizedRoute("PageNotFound",
                            "page-not-found",
                            new { controller = "Common", action = "PageNotFound" },
                            new[] { "Asu.Web.Controllers" });


            

            #region WC

            // WC brands
            routes.MapLocalizedRoute("Brands",
                            "brands/",
                            new { controller = "Customization", action = "Brands" },
                            new[] { "Asu.Web.Controllers" });
            // WC Checkout By Amazon
            routes.MapLocalizedRoute("AmazonCheckout",
                            "Cba",
                            new { controller = "Customization", action = "Cba" },
                            new[] { "Asu.Web.Controllers" });
            // WC Checkout By Amazon posts
            routes.MapLocalizedRoute("AmazonCheckoutPost",
                            "UpdateShippingOptions",
                            new { controller = "Customization", action = "UpdateShippingOptions" },
                            new[] { "Asu.Web.Controllers" });
            // WC Login with Amazon
            routes.MapLocalizedRoute("LoginWithAmazon",
                            "lwa",
                            new { controller = "Customization", action = "LoginWithAmazon" },
                            new[] { "Asu.Web.Controllers" });
            // WC Amazon Complete page
            routes.MapLocalizedRoute("AmazonComplete",
                            "CbaComplete",
                            new { controller = "Customization", action = "AmazonComplete" },
                            new[] { "Asu.Web.Controllers" });
            // WC Amazon Lwa Popup
            routes.MapLocalizedRoute("LwaPopup",
                            "LwaPopup",
                            new { controller = "Customization", action = "LwaPopup" },
                            new[] { "Asu.Web.Controllers" });
            // WC Check Order page
            routes.MapLocalizedRoute("CheckOrder",
                            "checkorder",
                            new { controller = "Customization", action = "CheckOrder" },
                            new[] { "Asu.Web.Controllers" });
            // WC Order Info page for any channel
            routes.MapLocalizedRoute("OrderInfo",
                            "orderinfo/{crmOrderId}/{hash}",
                            new { controller = "Customization", action = "OrderInfo", crmOrderId = UrlParameter.Optional, hash = UrlParameter.Optional },
                            new[] { "Asu.Web.Controllers" });
            // WC brands autocomplete
            routes.MapLocalizedRoute("BrandsAutocomplete",
                            "autocomplete/brand",
                            new { controller = "Customization", action = "GetBrands" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ClubLoyaltyProgram",
                          "members-club",
                          new { controller = "Customization", action = "ClubLoyaltyProgram" },
                          new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("AcknowledgeEbayMarketplaceAccountDeletionNotification",
                        "ack-ebay-marketplace-account-deletion-notification",
                        new { controller = "Customization", action = "AcknowledgeEbayMarketplaceAccountDeletionNotification" },
                        new[] { "Asu.Web.Controllers" });

            routes.Add("VehicleSeo", new VehicleSeoRoute());

            routes.Add("ProductGroupSeo", new ProductGroupSeoRoute());

            routes.MapLocalizedRoute("ProductGroupLloydPostback",
                            "lloyd/postback",
                            new { controller = "ProductGroup", action = "LloydMatsPostback" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ProductGroupLloydConfigurator",
                            "lloyd/configurator",
                            new { controller = "ProductGroup", action = "LloydConfigurator" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ProductGrouCoverkingPostback",
                       "coverking/postback",
                       new { controller = "ProductGroup", action = "CoverkingPostback" },
                       new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ReturnRequestHelper",
                       "return/helper",
                       new { controller = "ReturnRequest", action = "Helper" },
                       new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ReturnShipment",
                       "return/addtracking/{rmaId}",
                       new { controller = "ReturnRequest", action = "AddTracking" },
                       new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("CreateSalesQuote",
                "quote/create",
                new { controller = "SalesQuote", action = "Create" },
                new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("OpenSalesQuote",
              "quote/restore",
              new { controller = "SalesQuote", action = "RestoreQuote" },
              new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("RestoreSalesQuote",
             "quote/restore/{id}/{key}",
             new { controller = "SalesQuote", action = "Restore" },
             new[] { "Asu.Web.Controllers" });


            routes.MapLocalizedRoute("GetTireSpecificationValues",
                "vehicle/getspecification",
                new { controller = "Vehicle", action = "GetTireSpecificationValues" },
                new[] { "Asu.Web.Controllers" });

            #endregion
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
