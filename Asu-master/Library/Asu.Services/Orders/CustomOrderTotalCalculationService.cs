
using Asu.Core;
using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Discounts;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Shipping;
using Asu.Core.Domain.Tax;
using Asu.Services.Catalog;
using Asu.Services.Common;
using Asu.Services.Discounts;
using Asu.Services.Payments;
using Asu.Services.Shipping;
using Asu.Services.Tax;
using System;
using System.Collections.Generic;
namespace Asu.Services.Orders
{
    using System.Linq;

    public partial class CustomOrderTotalCalculationService : OrderTotalCalculationService
    {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly ITaxService _taxService;
        private readonly IShippingService _shippingService;
        private readonly IPaymentService _paymentService;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly IDiscountService _discountService;
        private readonly IGiftCardService _giftCardService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IOrderReportService _orderReportService;
        private readonly TaxSettings _taxSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly CatalogSettings _catalogSettings;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="priceCalculationService">Price calculation service</param>
        /// <param name="taxService">Tax service</param>
        /// <param name="shippingService">Shipping service</param>
        /// <param name="paymentService">Payment service</param>
        /// <param name="checkoutAttributeParser">Checkout attribute parser</param>
        /// <param name="discountService">Discount service</param>
        /// <param name="giftCardService">Gift card service</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="taxSettings">Tax settings</param>
        /// <param name="rewardPointsSettings">Reward points settings</param>
        /// <param name="shippingSettings">Shipping settings</param>
        /// <param name="shoppingCartSettings">Shopping cart settings</param>
        /// <param name="catalogSettings">Catalog settings</param>
        public CustomOrderTotalCalculationService(IWorkContext workContext,
            IStoreContext storeContext,
            IPriceCalculationService priceCalculationService,
            ITaxService taxService,
            IShippingService shippingService,
            IPaymentService paymentService,
            ICheckoutAttributeParser checkoutAttributeParser,
            IDiscountService discountService,
            IGiftCardService giftCardService,
            IGenericAttributeService genericAttributeService,
            IOrderReportService orderReportService,
            TaxSettings taxSettings,
            RewardPointsSettings rewardPointsSettings,
            ShippingSettings shippingSettings,
            ShoppingCartSettings shoppingCartSettings,
            CatalogSettings catalogSettings) : base(workContext,
            storeContext,
            priceCalculationService,
            taxService,
            shippingService,
            paymentService,
            checkoutAttributeParser,
            discountService,
            giftCardService,
            genericAttributeService,
            orderReportService,
            taxSettings,
            rewardPointsSettings,
            shippingSettings,
            shoppingCartSettings,
            catalogSettings)
        {
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._priceCalculationService = priceCalculationService;
            this._taxService = taxService;
            this._shippingService = shippingService;
            this._paymentService = paymentService;
            this._checkoutAttributeParser = checkoutAttributeParser;
            this._discountService = discountService;
            this._giftCardService = giftCardService;
            this._genericAttributeService = genericAttributeService;
            this._orderReportService = orderReportService;
            this._taxSettings = taxSettings;
            this._rewardPointsSettings = rewardPointsSettings;
            this._shippingSettings = shippingSettings;
            this._shoppingCartSettings = shoppingCartSettings;
            this._catalogSettings = catalogSettings;
        }

        #endregion

        /// <summary>
        /// Adjust shipping rate (free shipping, additional charges, discounts)
        /// </summary>
        /// <param name="shippingRate">Shipping rate to adjust</param>
        /// <param name="cart">Cart</param>
        /// <param name="appliedDiscount">Applied discount</param>
        /// <returns>Adjusted shipping rate</returns>
        public override decimal AdjustShippingRate(decimal shippingRate,
            IList<ShoppingCartItem> cart, out Discount appliedDiscount)
        {
            //appliedDiscount = null;

            //free shipping
            /*if (IsFreeShipping(cart))
                return decimal.Zero;*/

            //additional shipping charges
            /*decimal additionalShippingCharge = GetShoppingCartAdditionalShippingCharge(cart);
            var adjustedRate = shippingRate + additionalShippingCharge;*/
            var adjustedRate = shippingRate;

            //discount
            var customer = cart.GetCustomer();
            decimal discountAmount = GetShippingDiscount(customer, adjustedRate, out appliedDiscount);
            adjustedRate = adjustedRate - discountAmount;

            if (adjustedRate < decimal.Zero)
                adjustedRate = decimal.Zero;

            if (_shoppingCartSettings.RoundPricesDuringCalculation)
                adjustedRate = Math.Round(adjustedRate, 2);

            return adjustedRate;
        }

        public override decimal AdjustShippingRate(decimal shippingRate, IList<ShoppingCartItem> cart, out decimal discountAmount, out Discount appliedDiscount)
        {
            //appliedDiscount = null;

            //free shipping
            /*if (IsFreeShipping(cart))
                return decimal.Zero;

            //additional shipping charges
            decimal additionalShippingCharge = GetShoppingCartAdditionalShippingCharge(cart);
            var adjustedRate = shippingRate + additionalShippingCharge;*/
            var adjustedRate = shippingRate;

            //discount
            var customer = cart.GetCustomer();
            discountAmount = GetShippingDiscount(customer, adjustedRate, out appliedDiscount);
            adjustedRate = adjustedRate - discountAmount;

            if (adjustedRate < decimal.Zero)
                adjustedRate = decimal.Zero;

            if (_shoppingCartSettings.RoundPricesDuringCalculation)
                adjustedRate = Math.Round(adjustedRate, 2);

            return adjustedRate;
        }

        public override decimal GetShoppingCartAdditionalShippingCharge(IList<ShoppingCartItem> cart)
        {
            /*bool isFreeShipping = IsFreeShipping(cart);
            if (isFreeShipping)
                return decimal.Zero;*/

            return cart.Where(sci => sci.IsShipEnabled && !this._shippingService.IsFreeShipping(sci.Product, this._storeContext.CurrentStore.Id)).Sum(sci => sci.AdditionalShippingCharge);
        }

        public override decimal? GetShoppingCartShippingTotal(IList<ShoppingCartItem> cart, bool includingTax,
            out decimal taxRate, out Discount appliedDiscount)
        {
            decimal? shippingTotal = null;
            decimal? shippingTotalTaxed = null;
            appliedDiscount = null;
            taxRate = decimal.Zero;

            var customer = cart.GetCustomer();

            /*bool isFreeShipping = IsFreeShipping(cart);
            if (isFreeShipping)
                return decimal.Zero;*/

            ShippingOption shippingOption = null;
            if (customer != null)
                shippingOption = customer.GetAttribute<ShippingOption>(SystemCustomerAttributeNames.SelectedShippingOption, _genericAttributeService, _storeContext.CurrentStore.Id);

            if (shippingOption != null)
            {
                //use last shipping option (get from cache)

                //adjust shipping rate
                shippingTotal = AdjustShippingRate(shippingOption.Rate, cart, out appliedDiscount);
            }
            else
            {
                //use fixed rate (if possible)
                Address shippingAddress = null;
                if (customer != null)
                    shippingAddress = customer.ShippingAddress;

                var shippingRateComputationMethods = _shippingService.LoadActiveShippingRateComputationMethods(_storeContext.CurrentStore.Id);
                if (shippingRateComputationMethods == null || shippingRateComputationMethods.Count == 0)
                    throw new NopException("Shipping rate computation method could not be loaded");

                if (shippingRateComputationMethods.Count == 1)
                {
                    var shippingRateComputationMethod = shippingRateComputationMethods[0];

                    var shippingOptionRequests = _shippingService.CreateShippingOptionRequests(cart, shippingAddress);
                    decimal? fixedRate = null;
                    foreach (var shippingOptionRequest in shippingOptionRequests)
                    {
                        //calculate fixed rates for each request-package
                        var fixedRateTmp = shippingRateComputationMethod.GetFixedRate(shippingOptionRequest);
                        if (fixedRateTmp.HasValue)
                        {
                            if (!fixedRate.HasValue)
                                fixedRate = decimal.Zero;

                            fixedRate += fixedRateTmp.Value;
                        }
                    }

                    if (fixedRate.HasValue)
                    {
                        //adjust shipping rate
                        shippingTotal = AdjustShippingRate(fixedRate.Value, cart, out appliedDiscount);
                    }
                }
            }

            if (shippingTotal.HasValue)
            {
                if (shippingTotal.Value < decimal.Zero)
                    shippingTotal = decimal.Zero;

                //round
                if (_shoppingCartSettings.RoundPricesDuringCalculation)
                    shippingTotal = Math.Round(shippingTotal.Value, 2);

                shippingTotalTaxed = _taxService.GetShippingPrice(shippingTotal.Value,
                    includingTax,
                    customer,
                    out taxRate);

                //round
                if (_shoppingCartSettings.RoundPricesDuringCalculation)
                    shippingTotalTaxed = Math.Round(shippingTotalTaxed.Value, 2);
            }

            return shippingTotalTaxed;
        }

        public override decimal? GetShoppingCartShippingTotal(IList<ShoppingCartItem> cart, bool includingTax, out decimal taxRate, out decimal discountAmount, out Discount appliedDiscount)
        {
            decimal? shippingTotal = null;
            decimal? shippingTotalTaxed = null;
            appliedDiscount = null;
            taxRate = decimal.Zero;
            discountAmount = decimal.Zero;

            var customer = cart.GetCustomer();

            /*bool isFreeShipping = IsFreeShipping(cart);
            if (isFreeShipping)
                return decimal.Zero;*/

            ShippingOption shippingOption = null;
            if (customer != null)
                shippingOption = customer.GetAttribute<ShippingOption>(SystemCustomerAttributeNames.SelectedShippingOption, _genericAttributeService, _storeContext.CurrentStore.Id);

            if (shippingOption != null)
            {
                //use last shipping option (get from cache)

                //adjust shipping rate
                shippingTotal = AdjustShippingRate(shippingOption.Rate, cart, out discountAmount, out appliedDiscount);
            }
            else
            {
                //use fixed rate (if possible)
                Address shippingAddress = null;
                if (customer != null)
                    shippingAddress = customer.ShippingAddress;

                var shippingRateComputationMethods = _shippingService.LoadActiveShippingRateComputationMethods(_storeContext.CurrentStore.Id);
                if (shippingRateComputationMethods == null || shippingRateComputationMethods.Count == 0)
                    throw new NopException("Shipping rate computation method could not be loaded");

                if (shippingRateComputationMethods.Count == 1)
                {
                    var shippingRateComputationMethod = shippingRateComputationMethods[0];

                    var shippingOptionRequests = _shippingService.CreateShippingOptionRequests(cart, shippingAddress);
                    decimal? fixedRate = null;
                    foreach (var shippingOptionRequest in shippingOptionRequests)
                    {
                        //calculate fixed rates for each request-package
                        var fixedRateTmp = shippingRateComputationMethod.GetFixedRate(shippingOptionRequest);
                        if (fixedRateTmp.HasValue)
                        {
                            if (!fixedRate.HasValue)
                                fixedRate = decimal.Zero;

                            fixedRate += fixedRateTmp.Value;
                        }
                    }

                    if (fixedRate.HasValue)
                    {
                        //adjust shipping rate
                        shippingTotal = AdjustShippingRate(fixedRate.Value, cart, out discountAmount, out appliedDiscount);
                    }
                }
            }

            if (shippingTotal.HasValue)
            {
                if (shippingTotal.Value < decimal.Zero)
                    shippingTotal = decimal.Zero;

                //round
                if (_shoppingCartSettings.RoundPricesDuringCalculation)
                    shippingTotal = Math.Round(shippingTotal.Value, 2);

                shippingTotalTaxed = _taxService.GetShippingPrice(shippingTotal.Value,
                    includingTax,
                    customer,
                    out taxRate);

                //round
                if (_shoppingCartSettings.RoundPricesDuringCalculation)
                    shippingTotalTaxed = Math.Round(shippingTotalTaxed.Value, 2);
            }

            return shippingTotalTaxed;
        }
    }
}
