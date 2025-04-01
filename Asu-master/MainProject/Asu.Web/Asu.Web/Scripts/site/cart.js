(function ($, document, window) {
    APL.cart = APL.cart || {};
    var cart = APL.cart;

    var defaults = {
        $removeBtns: $(".remove-item-button"),
        $form: $("#shopping-cart-form"),
        $quantityInputs: $(".quantity-dd"),
        $toolBarRating: $("#toolbar-stars"),
        //$keepShoppingBtn: $(".keep_shopping"),
        $paypalBtn: $("#paypal-btn"),
        //$paypalCreditBtn: $("#paypal-credit-btn"),
        $amazonBtn: $("#amazon-btn"),
        $adminPrices: $(".admin-price"),
        $productsAlsoPurchased: $("#productsAlsoPurchased"),
        $checkoutBtn: $("#mobile-checkout-btn"),
        formSelector: "#shopping-cart-form",
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SetExpressCheckout?isCredit=false&cancelUrl=%2Fcart&returnUrl=%2Fcheckout%2Fpaypal",
        paypalCreditUrl: "/Plugins/PaymentPayPalExpressCheckout/SetExpressCheckout?isCredit=true&cancelUrl=%2Fcart&returnUrl=%2Fcheckout%2Fpaypal",
        //paypalCreditId: "paypal-credit-btn",
        addToCartSelector: ".product-box-add-to-cart-button",
        $applePayButton: $("#applePayButton")
    };

    function initRating() {
        $('div.rateit').rateit();
    }

    function initPayPal() {
        var s = defaults;
        window.paypalCheckoutReady = function () {
            s.$paypalBtn.on("DOMNodeInserted", function (e) {
                var $el = $(e.target).find("button.paypal-button");
                if ($el) {
                    $el.html("");
                    $el[0].style = "width: 100%;height:100%;border:none;max-width:none !important;";
                }
            });

            //s.$paypalCreditBtn.on("DOMNodeInserted", function (e) {
            //    var $el = $(e.target).find("button.paypal-button");
            //    if ($el) {
            //        $el.html("");
            //        $el[0].style = "width: 100%;height:100%;border:none;max-width:none !important;";
            //    }
            //});

            window.paypal.checkout.setup("DEGKRMMH8T6JY", {
                environment: "production",
                buttons: [
                    {
                        container: [document.getElementById("paypal-btn")], /*document.getElementById("paypal-credit-btn")],*/
                        type: "checkout",
                        color: "gold",
                        size: "medium",
                        shape: "rect"
                    }
                ],
                click: function (event) {
                    event.preventDefault();
                    window.paypal.checkout.reset();
                    window.paypal.checkout.initXO();

                    var url = s.paypalUrl; //event.target.id === s.paypalCreditId || event.target.parentElement.id === s.paypalCreditId ? s.paypalCreditUrl : s.paypalUrl;
                    var action = $.ajax({ type: "POST", url: url, dataType: "json", contentType: "application/json" });
                    action.done(function (data) {
                        if (data.success === 1) {
                            window.paypal.checkout.startFlow(data.token);
                            return;
                        }

                        var error = typeof data.errorMessage === "undefined" ? "An error occurred setting up your cart for PayPal" : data.errorMessage;
                        APL.messageBar.show(error, "error", 0);
                    });

                    action.fail(function (data) {
                        window.paypal.checkout.closeFlow();
                        var error = typeof data.errorMessage === "undefined" ? "An error occurred setting up your cart for PayPal" : data.errorMessage;
                        APL.messageBar.show(error, "error", 0);
                    });
                }
            });
        };

        var payPalScript = document.createElement("script");
        payPalScript.setAttribute("type", "text/javascript");
        payPalScript.setAttribute("src", "https://www.paypalobjects.com/api/checkout.js");
        payPalScript.setAttribute("async", "");
        document.body.appendChild(payPalScript);
    };

    //function initAmazon() {
    //    defaults.$amazonBtn.on("click", function () {
    //        APL.amazon.login();
    //        return false;
    //    });
    //};

    function initApplePay() {
        // Get the merchant identifier from the page meta tags.
        if (cart.applePay.supportedByDevice()) {
            cart.applePay.showButton();
        } 
    };

    function initButton($button, name, value, $form, $input) {
        if (APL.nullOrUndef($button) || APL.nullOrUndef(name) || APL.nullOrUndef($form) || APL.nullOrUndef(value)) {
            return;
        }

        function apply() {
            $("<input>").attr({ type: "hidden", name: name, value: value }).appendTo($form);
            $form.submit();
        };

        $button.on("click", function () {
            apply();
        });

        if (!APL.nullOrUndef($input)) {
            $input.keypress(function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    apply();
                }
            });
        }
    };

    function initProductsAlsoPurchased() {
        var s = defaults;
        if (s.$productsAlsoPurchased.length > 0 && window.dataLayer[0].shoppingCart && window.dataLayer[0].shoppingCart.items && window.dataLayer[0].shoppingCart.items.length > 0) {
            //var shoppingCartProductIds = [];
            //for (var j = 0; j < window.dataLayer[0].shoppingCart.items.length; j++) {
            //    shoppingCartProductIds.push(window.dataLayer[0].shoppingCart.items[j].productId);
            //}
            //if (shoppingCartProductIds.length > 0) {
                //$.ajax({
                //    url: "/Product/GetProductsAlsoPurchased",
                //    data: { productId: shoppingCartProductIds[0], productsCount: 5, shoppingCartProductIds: shoppingCartProductIds },
                //    type: "POST",
                //    success: function (data) {
                        //s.$productsAlsoPurchased.html(data);
                        s.$productsAlsoPurchased.find("img.lazy").lazyload({ effect: "fadeIn" });

                        $(s.addToCartSelector).each(function () {
                            $(this).on("click", function () {
                                APL.ajaxcart.add("/addproducttocart/details/" + $(this).data("productid") + "/1", "/cart", s.$form);
                                return false;
                            });
                        });
                //    }
                //});
            //}
        }
    }

    cart.discountBox = {
        $discount: $(".discount"),
        $discountBtn: $(".discount-btn"),
        $discountApplyBtn: $("#apply-discount-coupon-code"),
        $discountRemoveBtn: $(".remove-discount-coupon-btn"),
        $discountInput: $("#discount-coupon-code"),
        init: function () {
            var self = this;
            var s = defaults;

            initButton(this.$discountApplyBtn, "applydiscountcouponcode", "Apply coupon", s.$form, this.$discountInput);
            initButton(this.$discountRemoveBtn, "removediscountcouponcode", "Remove coupon", s.$form, null);

            this.$discount.neysModal({
                trigger: self.$discountBtn,
                onReady: function () {
                    self.$discount.show();
                },
                onOpened: function () {
                    self.$discountInput.focus();
                }
            });
        }
    };

    cart.giftCardBox = {
        $giftCard: $(".gift-card"),
        $giftCardBtn: $(".gift-card-btn"),
        $giftCardApplyBtn: $(".apply-giftcard-code"),
        $giftCardRemoveBtn: $(".remove-giftcard-btn"),
        $giftCardInput: $("#giftcard-coupon-code"),
        init: function () {
            var self = this;
            var s = defaults;
            initButton(this.$giftCardApplyBtn, "applygiftcardcouponcode", "Apply gift card", s.$form, this.$giftCardInput);

            self.$giftCardRemoveBtn.on("click", function () {
                $("<input>").attr({ type: "hidden", name: "removegiftcard-" + $(this).data("id"), value: "Edit shopping cart" }).appendTo(s.$form);
                APL.loader.show();
                s.$form.submit();
            });

            this.$giftCard.neysModal({
                trigger: self.$giftCardBtn,
                onReady: function () {
                    self.$giftCard.show();
                },
                onOpened: function () {
                    self.$giftCardInput.focus();
                }
            });
        }
    };

    cart.estimateShippingBox = {
        $estimateShippingBtn: $(".estimate-shipping-button"),
        $estimateShippingSection: $("#estimate-shipping-section"),
        $countries: $(".country-input"),
        $states: $(".state-input"),
        $shippingMethodBtns: $(".shipping-method-btn"),
        $estimateShippingLink: $(".estimate-shipping-link"),
        $zipCodeInput: $("#ZipPostalCode"),
        $adminShipping: $("#admin-shipping"),
        init: function () {
            var self = this;
            var s = defaults;

            function setAdminShipping(shipping) {
                APL.loader.show();
                $.ajax({
                    url: "/shoppingcart/setadminshipping",
                    data: { shipping: shipping },
                    type: "POST",
                    success: function () { window.location.reload(); },
                    error: function () { APL.loader.hide();}
                });
            }

            function estimate() {
                if (self.$zipCodeInput.val().trim().length === 0) {
                    return;
                }

                $("<input>").attr({ type: "hidden", name: "estimateshipping", value: "Estimate shipping" }).appendTo(s.$form);
                APL.loader.show();
                s.$form.submit();
            };

            self.$adminShipping.on("change", function () {
                $(this).focusout();
                setAdminShipping(self.$adminShipping.val());
            });

            self.$adminShipping.keypress(function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    $(this).trigger("change");
                }
            });

            self.$estimateShippingSection.accordion({
                header: self.$estimateShippingSection.find(".title"),
                active: false,
                collapsible: true,
                heightStyle: "content",
                activate: function (event, ui) {
                    if ($(this).data("estimated") === true) {
                        return;
                    }

                    setTimeout(function () {
                        self.$zipCodeInput.focus();
                    }, 1);
                }
            });

            self.$shippingMethodBtns.each(function () {
                $(this).on("click", function () {
                    $("<input>").attr({ type: "hidden", name: "estimateshipping", value: "Estimate shipping" }).appendTo(s.$form);
                    APL.loader.show();
                    s.$form.submit();
                });
            });

            if (self.$estimateShippingSection.data("estimated") === true) {
                if (self.$estimateShippingSection.data("uiAccordion")) {
                    self.$estimateShippingSection.accordion("option", "active", 0);
                }
            }

            if (self.$estimateShippingLink.length > 0) {
                self.$estimateShippingLink.on("click", function () {
                    if (self.$estimateShippingSection.data("uiAccordion")) {
                        self.$estimateShippingSection.accordion("option", "active", 0);

                        setTimeout(function () {
                            self.$zipCodeInput.focus();
                        }, 1);
                    }

                    //$("html, body").animate({ scrollTop: defaults.$reviews.offset().top - defaults.$toolBar.height() }, 1000);
                });
            }

            self.$zipCodeInput.keypress(function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    estimate();
                }
            });

            self.$countries.on("change", function () {
                var selectedCountryId = $(this).val();
                if (selectedCountryId === "1") {
                    self.$states.parent().hide();
                    self.$states.find("option").remove();

                    setTimeout(function () {
                        self.$zipCodeInput.focus();
                    }, 1);

                    return;
                }

                $.ajax({
                    cache: false,
                    type: "GET",
                    url: "/country/getstatesbycountryid",
                    data: { countryId: selectedCountryId, addSelectStateItem: true },
                    success: function (data) {
                        self.$states.find("option").remove();
                        if (data.length > 1) {
                            $.each(data, function (i, obj) {
                                self.$states.append($("<option></option>").attr({ value: obj.id, data_code: obj.code }).text(obj.name));
                            });
                            self.$states.parent().show();
                        } else {
                            self.$states.parent().hide();
                            setTimeout(function () {
                                self.$zipCodeInput.focus();
                            }, 1);
                        }
                    }
                });
            });

            self.$states.on("change", function () {
                setTimeout(function () {
                    self.$zipCodeInput.focus();
                }, 1);
            });

            self.$estimateShippingBtn.on("click", function () {
                estimate();
            });

            self.$estimateShippingSection.show();
        }
    };

    cart.applePay = {

        // Function to handle payment when the Apple Pay button is clicked/pressed.
        beginPayment: function (e) {
            e.preventDefault();
            APL.loader.show();

            function updateSubtotals(lineItems) {
                var total = lineItems.total;
                var subtotal = lineItems.subtotal;
                var discount = lineItems.discount;
                var shipping = lineItems.shipping;
                var tax = lineItems.tax;
                var newLineItems = [
                    { type: 'final', label: 'Subtotal', amount: subtotal.toFixed(2) },
                    { type: 'final', label: 'Shipping', amount: shipping.toFixed(2) },
                    { type: 'final', label: 'Tax', amount: tax.toFixed(2) }
                ]

                if (discount && discount > 0) {
                    newLineItems.push({ type: 'final', label: 'Discount', amount: discount.toFixed(2) });
                }

                applePaymentRequest.lineItems = newLineItems;
                applePaymentRequest.total = { type: 'final', label: 'Total', amount: total.toFixed(2) };
            }

            function getApplePayPaymentRequest() {
                $.ajax({
                    url: "/applepay/request",
                    method: "GET",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (response) {
                        $(".apple-pay-container").html(response);
                    },
                    error: function (xhr, status, error) {
                        APL.messageBar.show("Error while getting pament request data. Please refresh the page and try again or contact support.", "error", 0);
                    }
                });
            }

            getApplePayPaymentRequest();
            var session = new ApplePaySession(1, applePaymentRequest);

            // Setup handler for validation the merchant session.
            session.onvalidatemerchant = function (event) {
                // Create the payload.
                var data = {
                    validationUrl: event.validationURL
                };

                $.ajax({
                    url: "/applepay/validate",
                    method: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(data),
                    success: function (merchantSession) {
                        // Complete validation by passing the merchant session to the Apple Pay session.
                        session.completeMerchantValidation(merchantSession);
                    },
                    error: function (xhr, status, error) {
                        APL.messageBar.show("Error while getting merchant payment request data. Please refresh the page and try again or contact support.", "error", 0);
                    }
                });
            };

            // Setup handler for shipping method selection.
            session.onshippingmethodselected = function (event) {
                var shippingMethodSystemName = event.shippingMethod.identifier;

                $.ajax({
                    url: "/applepay/shipping/update",
                    data: { shippingMethod: shippingMethodSystemName },
                    type: "GET",
                    success: function (response) {
                        if (response.success) {
                            var shippingAmount = response.shipping;
                            var subtotalAmount = response.subtotal;
                            var discountAmount = response.discount;
                            var taxAmount = response.tax;
                            var totalAmount = response.total;
                            updateSubtotals({
                                shipping: shippingAmount,
                                tax: taxAmount,
                                subtotal: subtotalAmount,
                                discount: discountAmount,
                                total: totalAmount,
                            });

                            var update = {
                                newTotal: applePaymentRequest.total,
                                newLineItems: applePaymentRequest.lineItems
                            };

                            session.completeShippingMethodSelection(update);
                        } else {
                            APL.messageBar.show("Error while updating shipping data. " + response.message + " Please refresh the page and try again or contact support.", "error", 0);
                        }
                    },
                    error: function (request, status, error) {
                        APL.messageBar.show("Error while updating shipping data. Please refresh the page and try again or contact support.", "error", 0);
                    }
                });
            };

            session.oncancel = function (event) {
                APL.loader.hide();
            };

            session.onshippingcontactselected = function (event) {
                var contact = event.shippingContact;

                $.ajax({
                    url: "/applepay/shipping/update",
                    data: { countryId: 1, stateCode: contact.administrativeArea, city: contact.locality, zip: contact.postalCode },
                    type: "GET",
                    success: function (response) {
                        if (response.success) {
                            applePaymentRequest.shippingMethods = response.shippingMethods;
                            var shippingAmount = Number(applePaymentRequest.shippingMethods[0].amount);
                            var subtotalAmount = response.subtotal;
                            var discountAmount = response.discount;
                            var taxAmount = response.tax;
                            var totalAmount = response.total;
                            updateSubtotals({
                                shipping: shippingAmount,
                                tax: taxAmount,
                                subtotal: subtotalAmount,
                                discount: discountAmount,
                                total: totalAmount,
                            });

                            var update = {
                                status: ApplePaySession.STATUS_SUCCESS,
                                newLineItems: applePaymentRequest.lineItems,
                                newShippingMethods: applePaymentRequest.shippingMethods,
                                newTotal: applePaymentRequest.total
                            };

                            session.completeShippingContactSelection(update);
                        } else {
                            APL.messageBar.show("Error while updating shipping contact. " + response.message + " Please refresh the page and try again or contact support.", "error", 0);
                        }
                    },
                    error: function (request, status, error) {
                        APL.messageBar.show("Error while updating shipping contact. Please refresh the page and try again or contact support.", "error", 0);
                    }
                });
            };

            // Setup handler to receive the token when payment is authorized.
            session.onpaymentauthorized = function (event) {
                // Get the contact details for use, for example to
                // use to create an account for the user.
                var billingContact = event.payment.billingContact;
                var shippingContact = event.payment.shippingContact;
                billingContact.emailAddress = shippingContact.emailAddress;
                billingContact.phoneNumber = shippingContact.phoneNumber;

                // Get the payment data for use to capture funds from
                // the encrypted Apple Pay token in your server.
                var paymentData = event.payment.token.paymentData;

                var authorizationResult = {}

                // Apply the details from the Apple Pay sheet to the page.
                var updateContact = function (contact, type, typeCode) {
                    var prefix = type + "_NewAddress";
                    var data = {};
                    data[prefix + ".FirstName"] = contact.givenName;
                    data[prefix + ".LastName"] = contact.familyName;
                    data[prefix + ".CountryId"] = 1; // TODO: replace contact.country
                    data[prefix + ".StateProvinceShortName"] = contact.administrativeArea;
                    data[prefix + ".StateProvinceId"] = 0;
                    data[prefix + ".City"] = contact.locality;
                    data[prefix + ".Address1"] = contact.addressLines[0];
                    data[prefix + ".Address2"] = contact.addressLines[1];
                    data[prefix + ".ZipPostalCode"] = contact.postalCode;
                    data[prefix + ".PhoneNumber"] = contact.phoneNumber;
                    data[prefix + ".Email"] = contact.emailAddress;
                    data[prefix + ".TypeCode"] = typeCode;
                    data[prefix + ".Id"] = 0;
                    data[prefix + ".Company"] = "";

                    function appendErrors(authorizationResult, addressType, errors) {
                        authorizationResult.status = addressType === "billing"
                            ? ApplePaySession.STATUS_INVALID_BILLING_POSTAL_ADDRESS
                            : ApplePaySession.STATUS_INVALID_SHIPPING_POSTAL_ADDRESS;

                        authorizationResult.errors = errors;
                    }

                    $.ajax({
                        url: "/checkout/create-address-" + type,
                        data: $.param(data),
                        type: "POST",
                        async: false,
                        headers: { "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8" },
                        success: function (response) {
                            if (response.success) {
                            } else {
                                appendErrors(authorizationResult, type, response.errors);
                                session.completePayment(authorizationResult)
                            }
                        },
                        error: function (request, status, error) {
                            appendErrors(authorizationResult, type, [request.responseText]);
                            session.completePayment(authorizationResult)
                        }
                    });
                };

                //var cardName = event.payment.token.paymentMethod.displayName;
                updateContact(billingContact, "billing", 1);
                updateContact(shippingContact, "shipping", 2);

                // Do something with the payment to capture funds and
                // then dismiss the Apple Pay sheet for the session with
                // the relevant status code for the payment's authorization.

                $.ajax({
                    type: "POST",
                    url: "/checkout/applepay",
                    dataType: "text",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ data: JSON.stringify(paymentData) }),
                    async: false,
                    success: function (response) {
                       
                        var paymentResult = JSON.parse(response);
                        if (paymentResult.error === 0 && paymentResult.redirect) {
                            authorizationResult = {
                                status: ApplePaySession.STATUS_SUCCESS,
                                errors: []
                            };

                            session.completePayment(authorizationResult);
                            window.location.href = paymentResult.redirect;
                        } else {
                            authorizationResult = {
                                status: ApplePaySession.STATUS_FAILURE,
                                errors: ["Error"]
                            };

                            APL.loader.hide();
                            APL.messageBar.show(paymentResult.message, "error", 0);
                            session.completePayment(authorizationResult);
                        }
                    },
                    error: function (request, status, error) {
                        authorizationResult = {
                            status: ApplePaySession.STATUS_FAILURE,
                            errors: [error]
                        };

                        APL.loader.hide();
                        APL.messageBar.show(error, "error", 0);
                        session.completePayment(authorizationResult);
                    }
                });
            };

            // Start the session to display the Apple Pay sheet.
            session.begin();
        },
        setupApplePay: function () {
        },
        showButton: function () {
            var button = $("#applePayButton");
            $(document).on("click", "#applePayButton", cart.applePay.beginPayment);
            button.show()
        },
        showSetupButton: function () {
            
        },
        hideSetupButton: function () {
            
        },
        showError: function (text) {
           
        },
        showSuccess: function () {
         
        },
        supportedByDevice: function () {
            return "ApplePaySession" in window;
        },
        supportsSetup: function () {
            return "openPaymentSetup" in ApplePaySession;
        },
        getPageLanguage: function () {
            return $("html").attr("lang") || "en";
        },
        getMerchantIdentifier: function () {
            return $("meta[name='apple-pay-merchant-id']").attr("content");
        }
    }

    cart.init = function () {
        initPayPal();
        //initAmazon();
        initApplePay();
        initRating();
        cart.discountBox.init();
        cart.giftCardBox.init();
        cart.estimateShippingBox.init(); 

        var self = this;
        var s = defaults;

        s.$removeBtns.each(function () {
            $(this).on("click", function () {
                $("<input>").attr({ type: "hidden", name: "removefromcart", value: $(this).data("item") }).appendTo(s.$form);
                $("<input>").attr({ type: "hidden", name: "updatecart", value: "Update shopping cart" }).appendTo(s.$form);
                s.$form.submit();
            });
        });

        s.$checkoutBtn.on("click", function () {
            $("<input>").attr({ type: "hidden", name: "checkout", value: "Checkout" }).appendTo(s.$form);
            s.$form.submit();
        });

        //function keepShopping() {
        //    $("<input>").attr({ type: "hidden", name: "continueshopping", value: "Keep shopping" }).appendTo(s.$form);
        //    s.$form.submit();
        //}

        function updateQty() {
            $("<input>").attr({ type: "hidden", name: "updatecart", value: "Edit shopping cart" }).appendTo(s.$form);
            s.$form.submit();
        }

        function updateAdminPrice() {
            $("<input>").attr({ type: "hidden", name: "updatecart", value: "Edit shopping cart" }).appendTo(s.$form);
            s.$form.submit();
        }

        s.$quantityInputs.on("change", function () {
            updateQty();
        });

        s.$adminPrices.on("change", function () {
            updateAdminPrice();
        });

        s.$adminPrices.keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                updateAdminPrice();
            }
        });

        //s.$keepShoppingBtn.on("click", function () {
        //    keepShopping();
        //});

        s.$form.on("submit", function () {
            APL.loader.show();
        });

        initProductsAlsoPurchased();
        addWarnings(APL.cart.warningMessage, APL.cart.successMessage);

        function addWarnings(warningMessage, successMessage) {
            if (typeof warningMessage !== "undefined" && warningMessage !== '') {
                APL.messageBar.show(warningMessage, "error", 0);
            }
            else if (typeof successMessage !== "undefined" && successMessage !== '') {
                APL.messageBar.show(successMessage, "success", 0);
            }
        }
    };

    $(document).ready(function () {
        APL.init();
        cart.init();
        APL.loader.hide();

        $(".owl-carousel").owlCarousel({
          autoplay: true,
          autoplayTimeout: 3000,
          autoplayHoverPause: true,
          loop:true,
          margin:10,
          nav: true,
          dots: false,
          responsive:{
              0:{
                  items:1
              },
              600:{
                  items:3
              },
              1000:{
                  items:5
              }
          }
        });
    });

    $(window).bind("pageshow", function (event) {
        if (event.originalEvent.persisted) {
            window.location.reload();
        }
    });

})(jQuery, document, window);


