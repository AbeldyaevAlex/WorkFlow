(function ($, document, window) {
    APL.product = APL.product || {};
    var defaults = {
        $addToCartWrapper: $(".add-to-cart-button-block.with-qty"),
        $selectVehicleBtn: $("#select-vehicle-btn"),
        $selectVehicleWnd: $("#select-vehicle-wnd"),
        $addToCartBlock: $(".add-to-cart-button-block.with-qty"),
        $toolBar: $("#toolbar-fixed"),
        $toolBarRating: $("#toolbar-stars"),
        $toolBarBtn: $("#toolbar-btn"),
        $addToCartBtn: $("#add-to-cart-button-" + $("#ptsProductId").val()),
        $addToCartLnk: $(".add-to-cart-link"),
        $paypalBtn: $(".paypal-button"),
        $amazonBtn: $(".amazon-button"),
        $applePayBtn: $("#applePayButton"),
        $filter: $("#vehiclesFilter"),
        $checkByVehicle: $("#check-by-vehicle"),
        $tabs: $("#product-tabs"),
        $fitment: $("#fitment"),
        $reviews: $("#reviews"),
        $returns: $("#returns"),
        $fitmentAccordion: $("#fitment-accordion"),
        $scrollToFitment: $(".scroll-to-fitment"),
        $scrollToReviews: $(".scroll-to-reviews"),
        $scrollToReturns: $(".scroll-to-returns"),
        $rating: $("#rateit-product > div.rateit"),
        $fitmentBtn: $("#fitments-show-hide-btn"),
        $reviewsBtn: $("#reviews-show-hide-btn"),
        $competitorPrices: $("#competitorPrices"),
        $competitorsBtn: $("#competitors-link"),
        //$cashRebate: $("#cash-rebate"),
        $propositions: $("ul.garantbanner_inner>li.link"),
        fitmentHidden: true,
        reviewsHidden: true,
        productId: $("#ptsProductId").val(),
        formSelector: "#product-details-form",
        cartUrl: "/cart",
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SubmitButton",
        addToCartUrl: "/addproducttocart/details/" + $("#ptsProductId").val() + "/1",
        countdown: "countdown-time",
        checkoutAddresUrl: "/checkout/address"
    };

    var reinitAccordion = function () {
        defaults.$tabs.accordion({
            header: ".accordion-header",
            active: 1,
            collapsible: true,
            heightStyle: "content",
            create: function (e, ui) {
                $(".accordion-header").show();
                $(this).show();
            }
        });
    };

    var resize = function (width) {
        var $tabs = defaults.$tabs;
        if (width <= 480) {
            if (!$tabs.data("uiAccordion")) {
                reinitAccordion();
            }
        }

        if (width > 480) {
            if ($tabs.data("uiAccordion")) {
                $tabs.accordion("destroy");
            }
        }
    };

    var windowResize = function () {
        resize.call(this, $(this).width());
    };

    var orientationChange = function () {
        resize.call(this, $(this).width());
    };

    var initFloatToolbar = function () {
        if (APL.nullOrUndef(defaults.$addToCartBlock) || defaults.$addToCartBlock.length === 0) {
            return;
        }

        var handler = APL.onVisibilityChange(defaults.$addToCartBlock,
            function (visible, topVisible, bottomVisible, inViewPoint) {
                var toolbar = defaults.$toolBar;
                if (visible) {
                    toolbar.removeClass("active");
                } else {
                    toolbar.addClass("active");
                    if (!defaults.$toolBarRating.data("rateitInit")) {
                        defaults.$toolBarRating.rateit();
                    }
                }
            });

        $(window).on("DOMContentLoaded load resize scroll", handler);
    };

    function initApplePay() {
        APL.product.applePay = {
            // Function to handle payment when the Apple Pay button is clicked/pressed.
            beginPayment: function (e) {
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
                            APL.messageBar.show("Error while getting payment request data. Please refresh the page and try again or contact support.", "error", 0);
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
                                    status: ApplePaySession.STATUS_SUCCESS,
                                    newTotal: applePaymentRequest.total,
                                    newLineItems: applePaymentRequest.lineItems
                                };

                                session.completeShippingMethodSelection(update);
                            } else {
                                var update = {
                                    status: ApplePaySession.STATUS_FAILURE,
                                    newTotal: null,
                                    newLineItems: null
                                };

                                session.completeShippingMethodSelection(update);
                                APL.messageBar.show("Error while updating shipping data. " + response.message + " Please refresh the page and try again or contact support.", "error", 0);
                            }
                        },
                        error: function (request, status, error) {
                            var update = {
                                status: ApplePaySession.STATUS_FAILURE,
                                newTotal: null,
                                newLineItems: null
                            };

                            session.completeShippingMethodSelection(update);
                            APL.messageBar.show("Error while updating shipping data. Please refresh the page and try again or contact support.", "error", 0);
                        }
                    });
                };

                session.oncancel = function (event) {
                    window.location.replace(defaults.cartUrl);
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
                                var update = {
                                    status: ApplePaySession.STATUS_INVALID_SHIPPING_POSTAL_ADDRESS,
                                    newLineItems: applePaymentRequest.lineItems,
                                    newShippingMethods: applePaymentRequest.shippingMethods,
                                    newTotal: applePaymentRequest.total
                                };

                                session.completeShippingContactSelection(update);
                                APL.messageBar.show("Error while updating shipping contact. " + response.message + " Please refresh the page and try again or contact support.", "error", 0);
                            }
                        },
                        error: function (request, status, error) {
                            var update = {
                                status: ApplePaySession.STATUS_INVALID_SHIPPING_POSTAL_ADDRESS,
                                newLineItems: applePaymentRequest.lineItems,
                                newShippingMethods: applePaymentRequest.shippingMethods,
                                newTotal: applePaymentRequest.total
                            };

                            session.completeShippingContactSelection(update);
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
                            authorizationResult = {
                                status: ApplePaySession.STATUS_SUCCESS,
                                errors: []
                            };

                            session.completePayment(authorizationResult);
                            var paymentResult = JSON.parse(response);
                            if (paymentResult.error === 0 && paymentResult.redirect) {
                                window.location.href = paymentResult.redirect;
                            }
                        },
                        error: function (response) {
                            var paymentResult = JSON.parse(response);
                            authorizationResult = {
                                status: ApplePaySession.STATUS_FAILURE,
                                errors: [paymentResult.message]
                            };

                            session.completePayment(authorizationResult);
                        }
                    });
                };

                APL.loader.hide();
                // Start the session to display the Apple Pay sheet.
                session.begin();
            },
            setupApplePay: function () {
            },
            showButton: function () {
                var button = defaults.$applePayBtn;
                //button.show();

                button.on("click", function () {
                    APL.ajaxcart.add(defaults.addToCartUrl + "?forceredirection=false", null, defaults.formSelector);
                    APL.product.applePay.beginPayment();
                    return false;
                });
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
            },
            getMerchantIdentifier: function () {
                return $("meta[name='apple-pay-merchant-id']").attr("content");
            }
        }
    }

    var initVehicleFilter = function () {
        APL.veh.filter = defaults.$filter.neysVehicle({
            clear: "#vClear",
            productId: defaults.productId,
            denyInitFromCookie: true,
            hideClear: true,
            hideUniversal: true,
            disableSubmodel: true,
            disableSetVehicle: true,
            ready: function () {
                $(defaults.$filter).show();
            },
            changed: function () {
                if (!defaults.selectVehicleWndModal) {
                    return;
                }

                defaults.selectVehicleWndModal.$loader.show();
                var filter = APL.veh.filter;
                $.ajax({
                    url: "/Product/ProductFits",
                    type: "POST",
                    data: {
                        productId: defaults.productId,
                        yearId: filter.year,
                        makeId: filter.make,
                        modelId: filter.model
                    },
                    cache: false,
                    context: this,
                    success: function (result) {
                        // need code refactoring
                        $("#vehicle-specific").hide().parent().removeClass("fit").removeClass("not-fit").addClass("fits-data-block").addClass("specific");
                        $(".fits-info").html(result);
                        $("#product-fits-filter").hide();
                        $(".fits-info").show();
                        $("#product-fits-buttons").show();
                        defaults.$selectVehicleBtn.hide();
//                        defaults.$addToCartWrapper.hide();

                        $(".pf-edit").on("click", function () {
                            $("#product-fits-filter").show();
                            $(".pf-window.fits-info").hide();
                            $("#product-fits-buttons").hide();
                            defaults.selectVehicleWndModal.show();
                        });
                    },
                    complete: function() {
                        defaults.selectVehicleWndModal.$loader.hide();
                    }
                });
            },
            loading: function () {
                defaults.selectVehicleWndModal.$loader.show();
            },
            done: function () {
                defaults.selectVehicleWndModal.$loader.hide();
            }
        });

        $("#product-fits-buttons>.to-cart-btn").on("click", function() {
            APL.ajaxcart.add(defaults.addToCartUrl, defaults.cartUrl, defaults.formSelector);
            defaults.selectVehicleWndModal.hide();
        });

        $("#product-fits-buttons>.close-btn").on("click", function () {
            defaults.selectVehicleWndModal.hide();
        });
    };

    APL.product.init = function () {
        $("div.rateit").rateit();
        if ($("input.qty-input").length > 0){
            $("input.qty-input").neysQtyInput();
        }

        defaults.$addToCartBtn.on("click", function () {
            APL.ajaxcart.add(defaults.addToCartUrl, defaults.cartUrl, defaults.formSelector);
            return false;
        });

        defaults.$toolBarBtn.on("click", function () {
            APL.ajaxcart.add(defaults.addToCartUrl, defaults.checkoutAddresUrl, defaults.formSelector);
            return false;
        });

        defaults.$addToCartLnk.on("click", function () {
            APL.ajaxcart.add(defaults.addToCartUrl, defaults.checkoutAddresUrl, defaults.formSelector);
            return false;
        });

        //defaults.$amazonBtn.on("click", function () {
        //    APL.ajaxcart.add(defaults.addToCartUrl, null, defaults.formSelector);
        //    APL.amazon.login();
        //    return false;
        //});

        defaults.$paypalBtn.on("click", function () {
            APL.ajaxcart.add(defaults.addToCartUrl, defaults.paypalUrl, defaults.formSelector);
            return false;
        });

        defaults.$fitmentBtn.on("click", function () {
            if (defaults.fitmentHidden) {
                defaults.fitmentHidden = false;
                $(this).text("Hide");
                $(".fit-row.hidden").removeClass("hidden");
            }
            else {
                defaults.fitmentHidden = true;
                $(this).text("Show All Vehicle Fitments");
                $(".fit-row").slice(7).addClass("hidden");
            }
            return false;
        });

        defaults.$reviewsBtn.on("click", function () {
            if (defaults.reviewsHidden) {
                defaults.reviewsHidden = false;
                $(this).text("Hide");
                $(".product-review-item.hidden").removeClass("hidden");
                $(".rateit.review-rating").rateit();
            }
            else {
                defaults.reviewsHidden = true;
                $(this).text("Show All Reviews");
                $(".product-review-item").slice(4).addClass("hidden");
            }
            return false;
        });

        if (defaults.$selectVehicleWnd.length > 0) {
            defaults.selectVehicleWndModal = defaults.$selectVehicleWnd.neysModal({
                trigger: defaults.$selectVehicleBtn
            });

            defaults.$selectVehicleBtn.on("click", function () {
                if (!defaults.$selectVehicleWnd.is(":visible")) {
                    defaults.$selectVehicleWnd.show();
                }
            });

            if (APL.currentStore.vehicleSupported) {
                initVehicleFilter();
            }
           
        }

        if (defaults.$scrollToReviews.length > 0) {
            defaults.$scrollToReviews.on("click", function () {
                if (defaults.$tabs.data("uiAccordion")) {
                    defaults.$tabs.accordion("option", "active", 1);
                }

                $("html, body").animate({ scrollTop: defaults.$reviews.offset().top - defaults.$toolBar.height() }, 1000);
            });
        }

        if (defaults.$scrollToFitment.length > 0) {
            defaults.$scrollToFitment.on("click", function () {
                if (defaults.$tabs.data("uiAccordion")) {
                    defaults.$tabs.accordion("option", "active", 2);
                }

                $("html, body").animate({ scrollTop: defaults.$fitment.offset().top - defaults.$toolBar.height() }, 1000);
            });
        }

        if (defaults.$scrollToReturns.length > 0) {
            defaults.$scrollToReturns.on("click", function () {
                if (defaults.$tabs.data("uiAccordion")) {
                    defaults.$tabs.accordion("option", "active", 3);
                }

                $("html, body").animate({ scrollTop: defaults.$returns.offset().top - defaults.$toolBar.height() }, 1000);
            });
        }

        if (defaults.$fitmentAccordion.length > 0) {
            defaults.$fitmentAccordion.accordion({
                header: ".fit-row-title",
                active: false,
                collapsible: true,
                heightStyle: "content"
            });
        }

        if (defaults.$competitorPrices.length > 0) {
            if (APL.isMobile()) {
                defaults.$competitorPrices.neysModal({
                    trigger: defaults.$competitorsBtn,
                    onReady: function () {
                        defaults.$competitorPrices.show();
                    }
                });
            } else {
                defaults.$competitorsBtn.tooltip({
                    content: defaults.$competitorPrices.html()
                });
            }
        }

        //if (defaults.$cashRebate.length > 0) {
        //    defaults.$cashRebate.tooltip();
        //}

        defaults.$propositions.tooltip();

        initFloatToolbar();
        //initVoting();

        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);
        resize($(window).width());
    };

    $(document).ready(function () {

        // Initialization PhotoBox SliderPro
        $("#slider-pro-main").sliderPro({
            width: 670,
            height: 500,
            loop: false,
            arrows: true,
            buttons: false,
            thumbnailsPosition: 'right',
            thumbnailPointer: true,
            breakpoints: {
                800: {
                    thumbnailsPosition: 'bottom',
                    thumbnailWidth: 270
                }
            }
        });

        // Initialization PhotoBox Library
        $(".gallery-product").photobox();

        APL.init();
        APL.product.init();

        initApplePay();

        if (APL.product.applePay.supportedByDevice()) {
            APL.product.applePay.showButton();
        } else {
            defaults.$paypalBtn.css("width", "100%");
        }

        APL.loader.hide();
    });

    $(window).bind("pageshow", function (event) {
        if (event.originalEvent.persisted) {
            window.location.reload();
        }
    });

})(jQuery, document, window);