(function ($, document, window) {
    APL.amazon = APL.amazon || {}
    var amazonObject = APL.amazon;
    amazonObject.addressSelected = false;
    amazonObject.paymentSelected = false;

    var defaults = {
        widgetDesign: {
            designMode: "responsive"
        },
        $paymentWrapper: $(".wrapper_paiment_page"),
        $summary: $(".order-summary-container"),
        $selectedShippingMethod: $("#selectedShippingMethod"),
        scriptsUrl: $("#scriptsUrl").val(),
        sellerId: $("#sellerId").val(),
        clientId: $("#clientId").val(),
        accessToken: $("#accessToken").val(),
        orderReferenceId: $("#orderReferenceId").val(),
        addressBookWidgetBlock: "addressBookWidget",
        walletWidgetBlock: "walletWidget",
        $addressBookWidget: $("#addressBookWidget"),
        $walletWidget: $("#walletWidget"),
        submitBtns: ".confirm-btn",
        submitBtnBlock: ".confirm-button-wrap",
        form: $("#confirm-order-form"),
        $errorContainer: $("#error-container"),
        $amazonErrorContainer: $("#amazon-error-container"),
        $addressError: $("#address-error"),
        $paymentMethodError: $("#payment-method-error"),
        $shippingMethodError: $("#shipping-method-error"),
        shippingMethods: "input[name='shippingoption']",
        $shippingMethodBlock: $(".radiobuttons_list"),
        $prop65Warning: $(".prop65-warning")
    };

    var recalculateWidth = function () {
        var width = Math.round($(".master-wrapper-main").width() * 0.29) - 15;
        defaults.$summary.find(".order-summary-wrapper").css("width", width + "px");
    };

    var resize = function (width) {
        var style = "fixed_sidebar";
        if (width <= 1000) {
            $(defaults.$submitBtnBlock).hide();
            defaults.$summary.removeClass(style);
            defaults.$summary.find(".order-summary-wrapper").css("width", "100%");
        }
        else {
            if (defaults.$summary.hasClass(style)) {
                recalculateWidth();
            }
        }
    };

    var allowSubmit = function(value) {
        $(defaults.submitBtns).each(function() {
            $(this).attr("disabled", !value);
        });
    }

    var windowResize = function () {
        resize.call(this, $(this).width());
    };

    var orientationChange = function () {
        resize.call(this, $(this).width());
    };

    var initSumbmitButton = function () {
        $(defaults.submitBtns).each(function () {
            $(this).on("click", function () {
                defaults.form.submit();
            });
        });

        defaults.form.on("submit", function () {
            APL.loader.show();
        });
    }

    var initSummary = function () {
        var confirmButtonHandler = APL.onVisibilityChange(defaults.form, function (visible, topVisible, bottomVisible, inViewPoint) {
            var $submitBtnBlock = $(defaults.submitBtnBlock);

            if ($(window).width() <= 1000) {
                return;
            }

            if (visible) {
                $submitBtnBlock.hide();
            } else {
                $submitBtnBlock.show();
            }
        });

        var summaryHandler = APL.onVisibilityChange(defaults.$paymentWrapper, function (visible, topVisible, bottomVisible, inViewPoint) {
            var style = "fixed_sidebar";
            if ($(window).width() <= 1000) {
                return;
            }

            var $summary = defaults.$summary;

            if (topVisible) {
                $summary.removeClass(style);
                $summary.find(".order-summary-wrapper").css("width", "100%");
            } else {
                defaults.$summary.addClass(style);
                recalculateWidth();
            }
            
            
        });

        $(window).on("DOMContentLoaded load resize scroll", summaryHandler);
        $(window).on("DOMContentLoaded load resize scroll", confirmButtonHandler);
    };

    function initWidgets() {
        var s = defaults;
        var that = this;

        window.onAmazonLoginReady = function () {
            window.amazon.Login.setClientId(s.clientId);
            window.amazon.Login.setUseCookie(true);

            amazonObject.addressBook = new OffAmazonPayments.Widgets.AddressBook({
                sellerId: s.sellerId,
                amazonOrderReferenceId: s.orderReferenceId,
                onAddressSelect: function () {
                    APL.loader.show();
                    try {
                        clearAmazonErrors();
                        amazonObject.addressSelected = true;
                        defaults.$addressError.hide();
                        updateShippingOptions();
                    }
                    catch (e) {
                        allowSubmit(false);
                        endRequestHandler();
                    }
                },
                onError: function (error) {
                    addAmazonError(error);
                    allowSubmit(false);
                    endRequestHandler();
                },
                design: s.widgetDesign
            }).bind(defaults.addressBookWidgetBlock);

            amazonObject.wallet = new OffAmazonPayments.Widgets.Wallet({
                sellerId: s.sellerId,
                amazonOrderReferenceId: s.orderReferenceId,
                onPaymentSelect: function () {
                    amazonObject.paymentSelected = true;
                    defaults.$paymentMethodError.hide();
                    refreshWidgets();
                },
                onError: function (error) {
                    addAmazonError(error);
                    allowSubmit(false);
                    endRequestHandler();
                },
                design: s.widgetDesign
            }).bind(defaults.walletWidgetBlock);

            s.$errorContainer.show();
        };

        var amazonScript = document.createElement("script");
        amazonScript.setAttribute("type", "text/javascript");
        amazonScript.setAttribute("src", s.scriptsUrl);
        amazonScript.setAttribute("async", "");
        document.body.appendChild(amazonScript);
    };

    function updateShippingOptions() {
        APL.loader.show();
        clearAmazonErrors();
        var internalErrorMsg = "Internal error happened. Please try other address or refresh page. If it doesn't help, please contact us for assistance.";
        var selectedShippingMethod = $(defaults.shippingMethods + ":checked").data("value");
        defaults.$selectedShippingMethod.val(selectedShippingMethod);
        $.ajax({
            url: "/checkout/update-shipping-options", 
            data: { orderReferenceId: defaults.orderReferenceId, addressConsentToken: defaults.accessToken, selectedMethod: selectedShippingMethod != null ? selectedShippingMethod : "" }, 
            type: "POST",
            success: function (data) {
                var json = $.parseJSON(JSON.stringify(data));
                if (json == null) {
                    addShippingMethodError(internalErrorMsg);
                    defaults.$shippingMethodBlock.html("");
                    allowSubmit(false);
                    endRequestHandler();
                    return;
                }

                if (json.error != null) {
                    addShippingMethodError(json.error);
                    defaults.$shippingMethodBlock.html("");
                    defaults.$summary.html(json.orderTotals);
                    allowSubmit(false);
                    endRequestHandler();
                    return;
                }

                if (json.shippingMethods == null) {
                    addShippingMethodError("An error occured when requesting shipping methods. Make sure you selected the correct address.");
                    defaults.$shippingMethodBlock.html("");
                    defaults.$summary.html(json.orderTotals);
                    allowSubmit(false);
                    endRequestHandler();
                    return;
                }

                if (json.showProp65Warning) {
                    defaults.$prop65Warning.show();
                }
                else {
                    defaults.$prop65Warning.hide();
                }
                
                defaults.$shippingMethodBlock.html(json.shippingMethods);
                defaults.$summary.html(json.orderTotals);
                var shippingMethods = $(defaults.shippingMethods);
                defaults.$selectedShippingMethod.val($(defaults.shippingMethods + ":checked").data("value"));
                shippingMethods.each(function() {
                    $(this).on("click", function() {
                        updateShippingOptions.call($(this));
                    });
                });

                clearShippingMethodErrors();
                refreshWidgets();
                endRequestHandler();
            },
            error: function (jxhr, msg, err) {
                addShippingMethodError(internalErrorMsg);
                defaults.$shippingMethodBlock.html("");
                allowSubmit(false);
                endRequestHandler();
                return;
            }
        });
    }

    function refreshWidgets() {
        if (amazonObject.addressSelected === true && amazonObject.paymentSelected === true && defaults.$amazonErrorContainer.find("p span").length === 0
            && defaults.$shippingMethodError.find("p span").length === 0 && $(defaults.shippingMethods).length > 0) {
            allowSubmit(true);
            defaults.$errorContainer.hide();
        } else {
            allowSubmit(false);
            defaults.$errorContainer.show();
        }
    }

    function addAmazonError(error) {
        var errors = defaults.$amazonErrorContainer.find("p span").map(function (i, element) {
            return $(element).data("code");
        });

        var code = error.getErrorCode();
        if ($.grep(errors, function (value) { return value === code; }).length > 0) {
            return;
        }

        var msg = code === "BuyerSessionExpired" ? "Your session has expired. Please sign in with Amazon and continue checkout." : "Amazon Payment error: " + error.getErrorMessage();
        var newError = $("<p />").addClass("amazon-error").append($("<span />").attr("data-code", code).text(msg));
        defaults.$amazonErrorContainer.append(newError).show();
    }

    function clearAmazonErrors() { 
        defaults.$amazonErrorContainer.hide();
        defaults.$amazonErrorContainer.html("");
    }

    function endRequestHandler(sender, args) {
        try {
            if (addressSelected) {
                defaults.$addressError.hide();
            }
            if (paymentSelected) {
                defaults.$paymentMethodError.hide();
            }

            refreshWidgets();
        } catch (e) { }

        APL.loader.hide();
    }

    function addShippingMethodError(error) {
        var newError = $("<p />").append($("<span />").text(error));
        defaults.$shippingMethodError.append(newError).show();
        refreshWidgets();
    }

    function clearShippingMethodErrors() {
        defaults.$shippingMethodError.hide();
        defaults.$shippingMethodError.html("");
    }

    $(document).ready(function () {
        APL.init();
        initWidgets();
        initSumbmitButton();
        initSummary();

        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);
        resize($(window).width());
    });
})(jQuery, document, window);