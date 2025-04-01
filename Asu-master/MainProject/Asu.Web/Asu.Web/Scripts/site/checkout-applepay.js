// Copyright (c) Just Eat, 2016. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

autoplicity = {
    applePay: {
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

            applePaymentRequest.shippingMethods = applePaymentRequest.shippingMethods.filter(function (e) {
                return typeof e !== "undefined";
            });

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
                            //appendErrors(authorizationResult, type, response.errors);
                        }
                    },
                    error: function (request, status, error) {
                        //appendErrors(authorizationResult, type, [request.responseText]);
                    }
                });
            };

            session.oncancel = function (event) {
                window.location.reload();
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
                            //appendErrors(authorizationResult, type, response.errors);
                        }
                    },
                    error: function (request, status, error) {
                        //appendErrors(authorizationResult, type, [request.responseText]);
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
                                console.log("onpaymentauthorized handler says: " + type + " address created successfully"); // TODO: remove log
                            } else {
                                appendErrors(authorizationResult, type, response.errors);
                            }
                        },
                        error: function (request, status, error) {
                            appendErrors(authorizationResult, type, [request.responseText]);
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
            button.show();
            $(document).on("click", "#applePayButton", autoplicity.applePay.beginPayment);
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
};

(function () {
    if (autoplicity.applePay.supportedByDevice()) {
        autoplicity.applePay.showButton();
    } else {
        var applePayLabel = $("#applepay-label");
        if (applePayLabel) {
            applePayLabel.parents("li").remove();
        }
    }
})();
