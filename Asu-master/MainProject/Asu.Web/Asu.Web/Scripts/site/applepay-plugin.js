autoplicity = {
    applePay: {
        // Function to handle payment when the Apple Pay button is clicked/pressed.
        beginPayment: function (e) {
            e.preventDefault();

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
                        session.completeMerchantValidation(merchantSession);
                    },
                    error: function (xhr, status, error) {
                    }
                });
            };

            session.onpaymentmethodselected = function (event) {
                session.completePaymentMethodSelection(applePaymentRequest.total, applePaymentRequest.lineItems);
            };

            // Setup handler for shipping method selection.
            session.onshippingmethodselected = function (event) {
                session.completeShippingMethodSelection(update);
            };

            session.oncancel = function (event) {};

            session.onshippingcontactselected = function (event) {
                session.completeShippingContactSelection(update);
            };


            // Setup handler to receive the token when payment is authorized.
            session.onpaymentauthorized = function (event) {
                session.completePayment(authorizationResult);
            };

            // Start the session to display the Apple Pay sheet.
            session.begin();
        },
        setupApplePay: function () {},
        showButton: function () {
            $(document).on("click", "#applePayButton", autoplicity.applePay.beginPayment);
        },
        showSetupButton: function () {},
        hideSetupButton: function () {},
        showError: function (text) {},
        showSuccess: function () {},
        supportedByDevice: function () {
            return "ApplePaySession" in window;
        },
        supportsSetup: function () {
            return "openPaymentSetup" in ApplePaySession;
        },
        getPageLanguage: function () {},
        getMerchantIdentifier: function () {}
    }
};

(function () {

    // Get the merchant identifier from the page meta tags.
    var merchantIdentifier = autoplicity.applePay.getMerchantIdentifier();

    if (!merchantIdentifier) {
        autoplicity.applePay.showError("No Apple Pay merchant certificate is configured.");
    }
    // Is ApplePaySession available in the browser?
    else if (autoplicity.applePay.supportedByDevice()) {
        // Determine whether to display the Apple Pay button. See this link for details
        // on the two different approaches: https://developer.apple.com/documentation/applepayjs/checking_if_apple_pay_is_available
        if (ApplePaySession.canMakePayments() === true) {
            autoplicity.applePay.showButton();
        } else {
            ApplePaySession.canMakePaymentsWithActiveCard(merchantIdentifier).then(function (canMakePayments) {
                if (canMakePayments === true) {
                    autoplicity.applePay.showButton();
                    document.write('canMakePaymentsWithActiveCard');
                } else {
                    if (autoplicity.applePay.supportsSetup()) {
                        autoplicity.applePay.showSetupButton(merchantIdentifier);
                        document.write('showSetupButton');
                    } else {
                        autoplicity.applePay.showError("Apple Pay cannot be used at this time. If using macOS you need to be paired with a device that supports at least TouchID.");
                    }
                }
            });
        }
    } else {
        autoplicity.applePay.showError("This device and/or browser does not support Apple Pay.");
    }
})();
