(function ($, document, window) {
    APL.checkout = APL.checkout || {};
    var checkout = APL.checkout;

    var defaults = {
        $paymentWrapper: $(".wrapper_paiment_page"),
        $summary: $(".order-summary-container"),
        $paymentInfoErrorMessage: $(".payment-info-error-message"),
        $paypalLabel: $("#paypal-label"),
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SetExpressCheckout?isCredit=false&cancelUrl=%2Fcheckout%2Fpayment",
        paymentInfoCcErrorMessage: ".payment-info-cc-error-message",
        errorText: "An error occured when request. Please try again",
        submitBtn: $(".confirm-btn"),
        form: $("#confirm-order-form"),
        submitBtnBlock: $(".confirm-button-wrap"),
        phoneInput: $("#phoneNumberInput"),
        phoneMask: "(999)?-999-9999"
    }

    var initSumbmitButton = function () {
        var phoneInput = defaults.phoneInput;
        phoneInput.mask(defaults.phoneMask);
        defaults.submitBtn.each(function () {
            $(this).on("click", function () {
                defaults.form.validate();
                phoneInput.rules("add", { required: true, minlength: 14, maxlength: 14 });
                if (defaults.form.valid()) {
                    APL.loader.show();
                    defaults.form.submit();
                }
            });
        });
    }

    var recalculateWidth = function() {
        var width = Math.round($(".master-wrapper-main").width() * 0.29) - 15;
        defaults.$summary.find(".order-summary-wrapper").css("width", width + "px");
    };

    var resize = function (width) {
        var style = "fixed_sidebar";
        if (width <= 1000) {
            defaults.submitBtnBlock.hide();
            defaults.$summary.removeClass(style);
            defaults.$summary.find(".order-summary-wrapper").css("width", "100%");
        }
        else {
            if (defaults.$summary.hasClass(style)){
                recalculateWidth();
            }
        }
    };

    var windowResize = function () {
        resize.call(this, $(this).width());
    };

    var orientationChange = function () {
        resize.call(this, $(this).width());
    };

    var initSummary = function () {
        $(".back_to_payment_button").on("click", function (e) {
            $("html, body").animate({ scrollTop: $("#payment-details-block").offset().top }, 500);
            e.preventDefault();
        });

        var confirmButtonHandler = APL.onVisibilityChange(defaults.form, function (visible, topVisible, bottomVisible, inViewPoint) {
            var submit = defaults.submitBtnBlock;

            if ($(window).width() <= 1000) {
                return;
            }

            if (visible) {
                submit.hide();
            } else {
                submit.show();
            }
        });

        var summaryHandler = APL.onVisibilityChange(defaults.$paymentWrapper, function (visible, topVisible, bottomVisible, inViewPoint) {
            var $summary = defaults.$summary;

            var style = "fixed_sidebar";
            if ($(window).width() <= 1000) {
                return;
            }

            if (topVisible) {
                $summary.removeClass(style);
                $summary.find(".order-summary-wrapper").css("width", "100%");
            } else {
                defaults.$summary.addClass(style);
                recalculateWidth();
            }
        });

        $(window).on("DOMContentLoaded load resize scroll", confirmButtonHandler);
        $(window).on("DOMContentLoaded load resize scroll", summaryHandler);
    };

    checkout.shippingMethod = {
        setMethodUrl: "/simplecheckout/SetShippingMethod",
        options: $("input[name='shippingoption']"),
        set: function (value) {
            var that = this;

            APL.loader.show();
            $.ajax({
                cache: false,
                url: that.setMethodUrl,
                data: { shippingoption: value },
                type: "POST",
                success: function (response) {
                    if (response == null) {
                        defaults.$paymentInfoErrorMessage.html(defaults.errorText);
                        return;
                    }
                    
                    switch (response.error) {
                        case 0:
                            defaults.$summary.html(response.orderSummary);
                            break;
                        case 1:
                            if (!APL.nullOrUndef(response.message) && response.message !== "") {
                                defaults.$paymentInfoErrorMessage.html(response.message);
                            }

                            if (!APL.nullOrUndef(response.redirect) && response.redirect !== "") {
                                APL.redirect(response.redirect);
                                return;
                            }
                            break;
                    }

                    APL.loader.hide();
                },
                complete: function () {
                    try {

                    }
                    catch (ex) {
                        APL.loader.hide();
                    }
                },
                error: function () {
                    defaults.$paymentInfoErrorMessage.html(defaults.errorText);
                    window.location.reload();
                }
            });
        },
        init: function () {
            var that = this;

            $.each(that.options, function () {
                $(this).on("click", function () { that.set($(this).val()) });
            });
        }
    };

    $(document).ready(function () {
        APL.init();
        checkout.shippingMethod.init();
        initSumbmitButton();
        initSummary();

        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);
        resize($(window).width());
    });

})(jQuery, document, window);
