namespace Asu.Web.App_Start
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Optimization;
    using Asu.Core;
    using Asu.Core.Domain.Customization;
    using Asu.Core.Infrastructure;
    using Asu.Framework.Themes;

    public class BundleConfig
    {
        #region scripts

        private static readonly List<string> CommonScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery-{version}.js",
            "~/Scripts/site/jquery/jquery.lazyload.js",
            "~/Scripts/site/jquery/jquery-ui.js",
            //"~/Scripts/site/jquery/jquery.jpanelmenu.js",
            "~/Scripts/neysslider/js/neysslider.js",
            "~/Scripts/ui-controls/search-bar/js/search-bar.js",
            "~/Scripts/site/main.js"
        };

        private static readonly List<string> HomeScripts = new List<string>
        {
            "~/Scripts/site/custom-select.js",
            "~/Scripts/slick.min.js",
            "~/Scripts/site/vehicle.filter.js",
            "~/Scripts/neysslider/js/neysslider.js",
            "~/Scripts/neysvehicle/js/neysvehicle.js",
            "~/Scripts/tireconfigurator/js/tire-configurator.js",
            "~/Scripts/site/home.js",
            "~/Scripts/jquery.rateit.min.js",
            "~/Scripts/site/jquery/jquery.rateit.js"
        };

        private static readonly List<string> ProductScripts = new List<string>
        {
            "~/Scripts/jquery-migrate-1.2.1.min.js",
            "~/Scripts/site/jquery/jquery.rateit.js",
            "~/Scripts/popper.js",
            "~/Scripts/bootstrap.js",
            "~/Scripts/popper.js",
            "~/Scripts/jquery.sliderPro.min.js",
            "~/Scripts/site/vehicle.data.js",
            "~/Scripts/site/vehicle.filter.js",
            "~/Scripts/neysvehicle/js/neysvehicle.js",
            "~/Scripts/neysqtyinput/js/neysqtyinput.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/jquery.fancybox-1.3.4.pack.js",
            "~/Scripts/site/product.js",
            "~/Scripts/site/ajaxcart.js",
            "~/Scripts/jquery.photobox.js",
            "~/Scripts/slick.min.js"
            //"~/Scripts/site/countdown.js",
        };

        private static readonly List<string> BrandScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.rateit.js",
            "~/Scripts/site/vehicle.data.js",
            "~/Scripts/site/brand.js",
        };

        private static readonly List<string> ProductGroupScripts = new List<string>
        {
            "~/Scripts/neysgallery/js/neysgallery.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/site/jquery/jquery.rateit.js",
            "~/Scripts/site/vehicle.data.js",
            "~/Scripts/site/group.js",
        };

        private static readonly List<string> LloydScripts = new List<string>
        {
            "~/Scripts/site/lloyd.js"
        };

        private static readonly List<string> CoverkingScripts = new List<string>
        {
            "~/Scripts/site/coverking.js"
        };

        private static readonly List<string> ReturnScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/neysqtyinput/js/neysqtyinput.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/site/returnrequest.js"
        };

        private static readonly List<string> CheckOrderScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/site/checkorder.js"
        };

        private static readonly List<string> CartScripts = new List<string>
        {
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/site/jquery/jquery.rateit.js",
            "~/Scripts/site/ajaxcart.js",
            "~/Scripts/site/cart.js",
            "~/Scripts/site/jquery/jquery.rateit.js",
            "~/Scripts/site/sales-quote.js",
            "~/Scripts/owl.carousel.min.js",
            "~/Scripts/neysqtyinput/js/neysqtyinput.js",
        };

        #region Checkout

        private static readonly List<string> CheckoutAddressScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/jquery.maskedinput.js",
            "~/Scripts/site/checkout-address.js",
        };

        private static readonly List<string> CheckoutPaymentScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/jquery.maskedinput.js",
            "~/Scripts/site/checkout-payment.js",
            //"~/Scripts/site/checkout-applepay.js"
        };

        private static readonly List<string> CheckoutPayPalScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.maskedinput.js",
            "~/Scripts/site/checkout-paypal.js"
        };

        private static readonly List<string> CheckoutAmazonScripts = new List<string>
        {
            "~/Scripts/site/amazon.js",
            "~/Scripts/site/checkout-amazon.js"
        };

        private static readonly List<string> CheckoutApplePayScripts = new List<string>
        {
             "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/jquery.maskedinput.js",
            "~/Scripts/site/checkout-applepay.js"
        };

        private static readonly List<string> ProductSearchScripts = new List<string>
        {
            "~/Scripts/site/jquery/jquery.validate.js",
            "~/Scripts/site/jquery/jquery.validate.unobtrusive.js",
            "~/Scripts/bootstrap.js",
            "~/Scripts/popper.js",
            "~/Scripts/jquery.sliderPro.min.js",
            "~/Scripts/site/vehicle.data.js",
            "~/Scripts/site/vehicle.filter.js",
            "~/Scripts/jquery.jscroll.js",
            "~/Scripts/jquery.rateit.min.js",
            "~/Scripts/public.ajaxcart.js",
            "~/Scripts/neysvehicle/js/neysvehicle.js",
            "~/Scripts/neysmodal/js/neysmodal.js",
            "~/Scripts/neysslider/js/neysslider.js",
            "~/Scripts/site/product-search.js",
            "~/Scripts/site/product-search-refactored.js"
        };

        #endregion Checkout

        #endregion

        public static void RegisterBundles(BundleCollection bundles)
        {
            var themeName = EngineContext.Current.Resolve<IThemeContext>().WorkingThemeName;

            bundles.Add(new ScriptBundle("~/scripts/home") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(HomeScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/brand") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(BrandScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/product") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(ProductScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/lloyd") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(ProductGroupScripts).Concat(LloydScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/coverking") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(ProductGroupScripts).Concat(CoverkingScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/return") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(ReturnScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/checkorder") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckOrderScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/cart") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CartScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/checkout-address") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckoutAddressScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/checkout-payment") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckoutPaymentScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/checkout-paypal") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckoutPayPalScripts).ToArray()));
            //bundles.Add(new ScriptBundle("~/scripts/checkout-amazon") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckoutAmazonScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/product-search") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(ProductSearchScripts).ToArray()));
            bundles.Add(new ScriptBundle("~/scripts/checkout-applepay") { Orderer = new NonOrderingBundleOrderer() }.Include(CommonScripts.Concat(CheckoutApplePayScripts).ToArray()));

            #region home

            bundles.Add(new StyleBundle("~/styles/hhome") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/header.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/bhome") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/plugins/slick-slider.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/home.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/fhome") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/black-friday-banner.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/footer.css", new CssRewriteUrlTransform()));

            #endregion

            #region product

            bundles.Add(new StyleBundle("~/styles/hproduct") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/header.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/tooltip.min.css", new CssRewriteUrlTransform())
           );

            bundles.Add(new StyleBundle("~/styles/bproduct") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/plugins/slick-slider.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/product.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/product-svg.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/fproduct") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/rateit/rateit.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysqtyinput/css/neysqtyinput.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysgasllery/css/neysgallery.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/search_by_vehicle.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/plugins/photobox.css", new CssRewriteUrlTransform())
            );

            #endregion

            #region product search

            bundles.Add(new StyleBundle("~/styles/hproduct-search") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/tooltip.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/bproduct-search") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/jquery-ui-themes/smoothness/jquery-ui-1.10.3.custom.min.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/order_summary.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.min.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/product_details.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/products_grid_template.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/catalog.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/search_by_vehicle.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/others.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/mobile/tablets_landscape_netbooks.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/mobile/tablets_portrait_smartphones_landscape.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/mobile/smaller_mobile_devices.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/parts/mobile/filter_menu.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/mobile-search-filters.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/search.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/fproduct-search") { Orderer = new NonOrderingBundleOrderer() }
                  .Include($"~/Themes/{themeName}/Content/rateit.css", new CssRewriteUrlTransform())
                  .Include("~/Content/site/fontello/lock-embedded.css")
                  .Include("~/Content/css/product-svg.css")
                  .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                  .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                  .Include($"~/Content/css/footer.min.css", new CssRewriteUrlTransform())
                  .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform()));

            #endregion product search

            #region group

            bundles.Add(new StyleBundle("~/styles/hgroup") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/bgroup") { Orderer = new NonOrderingBundleOrderer() }
            .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
              .Include($"~/Themes/{themeName}/Content/css/group.css", new CssRewriteUrlTransform())
              .Include($"~/Content/css/product-svg.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/fgroup") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/site/rateit/rateit.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include("~/Scripts/neysgallery/css/neysgallery.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform()));

            #endregion

            #region return request

            bundles.Add(new StyleBundle("~/styles/hreturn") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.min.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/breturn") { Orderer = new NonOrderingBundleOrderer() }
               .Include($"~/Content/css/returnrequest.css", new CssRewriteUrlTransform())
           );

            bundles.Add(new StyleBundle("~/styles/freturn") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include("~/Scripts/neysqtyinput/css/neysqtyinput.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform())
            );

            #endregion

            #region shopping cart

            bundles.Add(new StyleBundle("~/styles/hcart") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/bcart") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/topic.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/cart.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/styles/fcart") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/rateit/rateit.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include($"~/Content/css/cart-svg.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysqtyinput/css/neysqtyinput.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/owl.carousel.min.css", new CssRewriteUrlTransform())
                .Include($"~/Themes/{themeName}/Content/css/owl.theme.default.min.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform()));

            #endregion

            #region checkout

            #region address

            bundles.Add(new StyleBundle("~/styles/hcheckout-address") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/bcheckout-address") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/checkout-header.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/checkout-address.css", new CssRewriteUrlTransform())
           );

            bundles.Add(new StyleBundle("~/styles/fcheckout-address") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include("~/Content/css/checkout-svg.css")
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform()));

            #endregion address

            #region success

            bundles.Add(new StyleBundle("~/styles/bcheckout-completed") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/checkout-success.css", new CssRewriteUrlTransform())
            );

            #endregion success

            #region payment

            bundles.Add(new StyleBundle("~/styles/hcheckout-payment") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/general.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/header.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new StyleBundle("~/styles/bcheckout-payment") { Orderer = new NonOrderingBundleOrderer() }
                .Include($"~/Content/css/checkout-header.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/checkout-payment.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/cart-svg.css"));

            bundles.Add(new StyleBundle("~/styles/fcheckout-payment") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Scripts/ui-controls/search-bar/css/search-bar.css", new CssRewriteUrlTransform())
                .Include($"~/Content/css/footer.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/fontello/lock-embedded.css")
                .Include($"~/Content/css/checkout-svg.css")
                .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/neysmodal/css/neysmodal.css", new CssRewriteUrlTransform()));

            #region amazon

            //bundles.Add(new StyleBundle("~/styles/hcheckout-amazon") { Orderer = new NonOrderingBundleOrderer() }
            //    .Include($"~/Themes/{themeName}/Content/css/header.css", new CssRewriteUrlTransform())
            //    .Include($"~/Themes/{themeName}/Content/css/checkout-header.css", new CssRewriteUrlTransform())
            //    .Include($"~/Themes/{themeName}/Content/css/checkout-payment.css", new CssRewriteUrlTransform())
            //    .Include($"~/Themes/{themeName}/Content/css/footer.css", new CssRewriteUrlTransform()));

            //bundles.Add(new StyleBundle("~/styles/fcheckout-amazon") { Orderer = new NonOrderingBundleOrderer() }
            //    .Include("~/Content/site/fontello/fonticons.css")
            //    .Include($"~/Themes/{themeName}/Content/css/footer-svg.css")
            //    .Include($"~/Themes/{themeName}/Content/css/checkout-svg.css")
            //    .Include("~/Content/site/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform())
            //    .Include("~/Content/site/jquery-ui/jquery-ui.theme.css", new CssRewriteUrlTransform()));

            #endregion amazon

            #endregion payment


            #endregion checkout

            #region topic

            bundles.Add(new StyleBundle("~/styles/btopic") { Orderer = new NonOrderingBundleOrderer() }
                .Include("~/Content/css/topic.css", new CssRewriteUrlTransform())
                .Include("~/Content/css/text-pages.css", new CssRewriteUrlTransform()));

            #endregion topic

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/SiteKendo.css"));


            //Kendo UI Js
            bundles.Add(new ScriptBundle("~/bundles/kendojs").Include(
                      "~/Scripts/kendoHelpersn.js",
                      "~/Scripts/kendo/2016.1.412/jszip.min.js",
                      "~/Scripts/kendo/2016.1.412/angular.min.js",
                      "~/Scripts/kendo/2016.1.412/kendo.web.min.js",
                      "~/Scripts/kendo/2016.1.412/kendo.angular.min.js",
                      "~/Scripts/kendo/2016.1.412/kendo.angular2.min.js",
                      "~/Scripts/kendo/2016.1.412/kendo.all.min.js",
                      "~/Scripts/kendo/2016.1.412/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo/2016.1.412/cultures/kendo.culture.ba-RU.min.js",
                      "~/Scripts/kendo/2016.1.412/messages/kendo.messages.ru-RU.min.js"));



            //Kendo UI CSS
            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
                      "~/Content/kendo/2016.1.412/kendo.common.min.css",
                      "~/Content/kendo/2016.1.412/kendo.mobile.all.min.css",
                      "~/Content/kendo/2016.1.412/kendo.dataviz.min.css",
                      "~/Content/kendo/2016.1.412/kendo.blueopal.min.css",
                      "~/Content/kendo/2016.1.412/kendo.dataviz.default.min.css"
                      ));


#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }

    public class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}