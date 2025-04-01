(function ($, document, window) {

APL.home = APL.home || {};
APL.productGroup = {
    productOptionSelector: ".product-group-option"
};

    var defaults = {
        //configuratorDialog: $("#group-configurator-dialog"),
        configuratorBtns: $(".configurator-button"),
        vehicleFilter: $("#vehiclesFilter"),
        gallery: $("#neysgallery"),
        entityId: $("#product-group-id").val(),
        entityType: $("#vehicle-filter-entity-type").val(),
        fitmentHeader: $(".pf-header"),
        vehicleFilterContainer: $(".vehicle-filter-block"),
        priceElement: $(".product-price").children("span"),
        descriptionElement: $(".full-description"),
        specificationsElement: $(".product-group-specifications"),
        inventoryElement: $(".product-group-specifications"),
        addToCartUrl: "/addproducttocart/details/",
        addToCartContainer: $(".add-to-cart-container"),
        cartUrl: "/cart",
        productId: $("#selected-productId"),
        qtyInputSelector: "input.qty-input",
        addToCartLinkSelector: "#btn-add-to-cart",
        addToCartBlock: $(".add-to-cart-button-block"),
        productOptionsBlock: $(".product-options"),
        viewSpecificationsBtnSelector: ".view-product-specifications",
        productSpecificationsSelector: ".product-specs-box",
        warningModalSelector: "#warning-modal",
        warningModalBtnSelector: ".warning-modal-btn",
        form: $("#product-group-form"),
        selectVehicleModal: $("#select-vehicle-wnd"),
        selectVehicleBtn: $(".set-vehicle-btn"),
        propositions: $("ul.guarantee-banner-inner > li.link"),
        partNumber: $(".manufacturer-part-number .value"),
        inventoryContainer: $(".inventory-container"),
        freeShippingBlock: $(".free-shipping"),
        paypalButtonSelector: ".paypal-button",
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SubmitButton",
        $applePayBtn: $("#applePayButton")
    };

    function validate() {
        var options = $(APL.productGroup.productOptionSelector);

        return options.length > 0 && options.val() !== "";
    }

    function printErrorMessage(error) {
        APL.messageBar.show(error, "error", 0);
    }

    function appendPartNumber(productId, partNumber) {
        defaults.partNumber.attr("id", "mpn-" + productId);
        defaults.partNumber.text(partNumber);
        $(".manufacturer-part-number").show();
    }

    function initPayPal() {
        $(defaults.paypalButtonSelector).on("click", function () {
            var paymentAllowed = validate();
            if (!paymentAllowed) {
                printErrorMessage("Please ensure you selected your vehicle and product options.");
                return false;
            }

            var addToCartUrl = defaults.addToCartUrl + defaults.productId.val() + "/" + $(defaults.qtyInputSelector).val();
            APL.ajaxcart.add(addToCartUrl, defaults.paypalUrl, defaults.form);
            return false;
        });
    }

    function initApplePay() {
        APL.productGroup.applePay = {
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
                    ];

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

                // Start the session to display the Apple Pay sheet.
                session.begin();
            },
            setupApplePay: function () {
            },
            showButton: function () {
                var button = defaults.$applePayBtn;
                button.show();

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

    if (defaults.selectVehicleModal.length > 0) {
        defaults.selectVehicleWindow = defaults.selectVehicleModal.neysModal({
            trigger: defaults.selectVehicleBtn
        });

        defaults.selectVehicleBtn.on("click", function () {
            if (!defaults.selectVehicleModal.is(":visible")) {
                defaults.selectVehicleModal.show();
            }
        });
    }

    function initModalButton() {
        $(defaults.warningModalBtnSelector).on("click", function () {
            alert("Clicking this button will redirect you to tires search page that fits your vehicle. Coming soon...");
            window.location.href = "/17-tire-and-wheel";
        });
    }

    function initQtyInput() {
        if ($(defaults.qtyInputSelector).length > 0) {
            $(defaults.qtyInputSelector).neysQtyInput();
        }
    }

    function showModal(show = false) {
        var modals = $(defaults.warningModalSelector);
        initModalButton();
        if (modals.length > 0 && show) {
            var modal = modals.neysModal();
            $(".close").on("click", function () {
                modals.hide();
                APL.loader.hide();
            });

            modals.show();
            modal.show();
        }   
    } 

    function initAddToCart() {
        $(defaults.addToCartLinkSelector).on("click", function () {
            var isValid = validate();
            if (!isValid) {
                printErrorMessage("Please ensure you selected your vehicle and product options.");
                return false;
            }

            //var productOptions = $(APL.productGroup.productOptionSelector);
            //if (productOptions.length > 0 && productOptions.filter(function () { return $(this).val() === ''; }).length > 0) {
            //    APL.messageBar.show("Please ensure you selected your vehicle and product options.", "error", 0);
            //    return false;
            //}

            var addToCartUrl = defaults.addToCartUrl + defaults.productId.val() + "/" + $(defaults.qtyInputSelector).val();
            APL.ajaxcart.add(addToCartUrl, defaults.cartUrl, defaults.form);
            return false;
        });
    }

    function initScrollDown() {
        $(defaults.viewSpecificationsBtnSelector).on("click", function () {
            $([document.documentElement, document.body]).animate({
                scrollTop: $(defaults.productSpecificationsSelector).offset().top
            }, 1000);
        });
    }

var initVehicleFilter = function () {
    APL.veh.filter = defaults.vehicleFilter.neysVehicle({
        clear: "#vClear",
        entityId: defaults.entityId,
        entityType: defaults.entityType,
        denyInitFromCookie: false,
        hideClear: true,
        hideUniversal: true,
        disableSubmodel: false,
        disableSetVehicle: false,
        ready: function () {
            $(defaults.vehicleFilter).show();
        },
        changed: function () {
            var filter = APL.veh.filter;
            if (!filter.isSet()) {
                return; 
            }

            var vehicle = {
                year: filter.year,
                makeId: filter.make,
                modelId: filter.model,
                submodelId: filter.submodel
            };

            //defaults.fitmentHeader.children("div").text(filter.getFullName());
            //defaults.vehicleFilterContainer.hide();
            //defaults.fitmentHeader.show();
            //APL.productGroup.getGroupVariants(defaults.entityId, vehicle);
            //APL.productGroup.initProductOptions();
            //initScrollDown();
            window.location.reload();
        },
        loading: function () {
            APL.loader.show();
        },
        done: function () {
            APL.loader.hide();
        }
    });
};
 
    APL.productGroup.init = function () {
        this.Id = defaults.entityId;

        if (APL.currentStore.vehicleSupported) {
            initVehicleFilter();
            APL.productGroup.initProductOptions();
        }

        if ($.fn.neysGallery) {
            defaults.gallery.neysGallery({
                onReady: function () {
                    defaults.gallery.show();
                }
            });
        }

        initAddToCart();
        initPayPal();
        showModal($(APL.productGroup.productOptionSelector + ' option').length === 1);
        initQtyInput();
        defaults.propositions.tooltip();
    };

    APL.productGroup.initProductOptions = function () {
        var productOptions = $(this.productOptionSelector);

        if (productOptions && productOptions.length > 0) {
            var that = this;

            $.each(productOptions, function () {
                $(this).selectmenu({
                    change: function (event, ui) {
                        if (ui.item.value !== '') {
                            APL.loader.show();
                            that.update(ui.item.value);
                        } else {
                            $(".add-to-cart-button-block").remove();
                        }
                    }
                });
            });
        }
    };

    APL.productGroup.update = function (productId) {
        $.ajax({
            url: "/group/update",
            type: "POST",
            data: { productId },
            cache: false,
            context: this,
            success: function (result) {
                defaults.priceElement.text(result.Price);
                defaults.descriptionElement.html(result.Description);
                defaults.specificationsElement.html(result.SpecificationAttributes);
                defaults.addToCartContainer.html(result.AddToCart);
                $(defaults.productId).val(result.ProductId);
                initQtyInput();
                initAddToCart();
                initPayPal();
                //initApplePay();
                initScrollDown();
                appendPartNumber(result.ProductId, result.PartNumber);
                defaults.inventoryContainer.html(result.Inventory);
                if (result.IsFreeShipping) {
                    defaults.freeShippingBlock.show();
                }   

                var gallery = $("#neysgallery");
                gallery.replaceWith(result.MediaGallery);

                if ($.fn.neysGallery) {
                    gallery.neysGallery({
                        onReady: function () {
                            gallery.show();
                        }
                    });
                }

                if (window.history.pushState) {
                    var newUrl = window.location.protocol + "//" + window.location.host + window.location.pathname + '?variant=' + defaults.productId.val();
                    window.history.replaceState(document.documentElement.innerHTML, document.title, newUrl);
                }
                
                $(".view-product-specifications").show();
            },
            complete: function () {
                APL.loader.hide();
            }
        });
    };

    APL.productGroup.getGroupVariants = function (groupId, vehicle) {
        $.ajax({
            url: "/group/variants",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: "POST",
            data: $.param({
                "groupId": groupId,
                "vehicleModel.Year": vehicle.year,
                "vehicleModel.MakeId": vehicle.makeId,
                "vehicleModel.ModelId": vehicle.modelId,
                "vehicleModel.SubmodelId": vehicle.submodelId
            }),
            cache: false,
            context: this,
            success: function (data) {
                defaults.productOptionsBlock.replaceWith(data.ProductOptions);
                defaults.productOptionsBlock.show();
                $(defaults.warningModalSelector).replaceWith(data.WarningModal);
                APL.productGroup.initProductOptions();
                showModal($(APL.productGroup.productOptionSelector).length === 0);
            },
            complete: function () {
                APL.loader.hide();
            }
        });
    };  


var $tabs = $("#details-tabs");

var resize = function (width) {
    if (width <= 480) {
        if ($tabs.data("uiTabs")) {
            $tabs.tabs("destroy");
        }

        if (!$tabs.data("uiAccordion")) {
            $tabs.accordion({
                header: ".accordion-header",
                collapsible: true,
                heightStyle: "content",
                create: function (e, ui) {
                    $(".tab-headers").hide();
                    $(".accordion-header").show();
                    $(this).show();
                }
            });
        }
    }

    if (width > 480) {
        if ($tabs.data("uiAccordion")) {
            $tabs.accordion("destroy");
        }

        if (!$tabs.data("uiTabs")) {
            $tabs.tabs({
                create: function (e, ui) {
                    $(".accordion-header").hide();
                    $(".tab-headers").show();
                    $(this).show();
                }
            });
        }
    }
};


var windowResize = function () {
    resize.call(this, $(this).width());
};

var orientationChange = function () {
    resize.call(this, $(this).width());
};

$(window).on("resize", windowResize);
$(window).on("orientationchange", orientationChange);
resize($(window).width());

$(document).ready(function () {
    APL.init();
    APL.productGroup.init();
});

})(jQuery, document, window);