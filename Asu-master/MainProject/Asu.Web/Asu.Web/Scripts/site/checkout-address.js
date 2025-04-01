(function ($, document, window) {
    APL.checkout = APL.checkout || {};
    var checkout = APL.checkout;

    var defaults = {
        $billingSameAsShipping: $(".billing-same-as-shipping"),
        $billingAddressBox: $(".billing-address"),
        $newReturningCustomer: $("input[name='new-customer']"),
        $customerForm: $("#customer-info"),
        $form: $("#shopping-cart-form"),
        $loginForm: $("#login-box"),
        submitBtns: $(".continue-checkout-btn"), 
        continueBtns: $(".continue-shopping-btn"),
        choiceSeparator: $(".choice-separator"),
        loginForm: $("#login-form"),
        $summary: $(".order-summary-container"),
        $addressWrapper: $(".wrapper_checkout"),
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SetExpressCheckout?isCredit=false&cancelUrl=%2Fcart&returnUrl=%2Fcheckout%2Fpaypal",
        paypalBtn: $("#paypal-btn")
    };

    $.extend(defaults, APL.addressSelectors);

    var initSubmitBtn = function (inputs) {
        if (!APL.nullOrUndef(inputs)) {
            inputs.each(function () {
                $(this).keypress(function (e) {
                    if (e.which === 13) {
                        e.preventDefault();
                        checkout.login.submit();
                    }
                });
            });
        }
    };

    function initPayPal() {
        var settings = defaults;
        window.paypalCheckoutReady = function () {
            settings.paypalBtn.on("DOMNodeInserted", function (e) {
                var $el = $(e.target).find("button.paypal-button");
                if ($el) {
                    $el.html("");
                    $el[0].style = "width: 100%;height:100%;border:none;max-width:none !important;";
                }
            });

            window.paypal.checkout.setup("DEGKRMMH8T6JY", {
                environment: "production",
                buttons: [
                    {
                        container: [document.getElementById("paypal-btn")],
                        type: "checkout",
                        color: "gold",
                        size: "medium",
                        shape: "rect"
                    }
                ],
                click: function (event) {
                    APL.loader.show();
                    event.preventDefault();
                    window.paypal.checkout.reset();
                    window.paypal.checkout.initXO();

                    var url = settings.paypalUrl; 
                    var action = $.ajax({ type: "POST", url: url, dataType: "json", contentType: "application/json" });
                    action.done(function (data) {
                        if (data.success === 1) {
                            window.paypal.checkout.startFlow(data.token);
                            APL.loader.hide();
                            return;
                        }

                        var error = typeof data.errorMessage === "undefined" ? "An error occurred setting up your cart for PayPal" : data.errorMessage;
                        APL.messageBar.show(error, "error", 0); 
                        APL.loader.hide();
                    });

                    action.fail(function (data) {
                        window.paypal.checkout.closeFlow();
                        var error = typeof data.errorMessage === "undefined" ? "An error occurred setting up your cart for PayPal" : data.errorMessage;
                        APL.messageBar.show(error, "error", 0);
                        APL.loader.hide();
                    });
                }
            });
        };

        var payPalScript = document.createElement("script");
        payPalScript.setAttribute("type", "text/javascript");
        payPalScript.setAttribute("src", "https://www.paypalobjects.com/api/checkout.js");
        payPalScript.setAttribute("async", "");
        document.body.appendChild(payPalScript);
    }

    var recalculateWidth = function () {
        var width = Math.round($(".master-wrapper-main").width() * 0.34) - 15;
        defaults.$summary.find(".order-summary-wrapper").css("width", width + "px");
    };

    var resize = function (width) {
        var style = "fixed_sidebar";
        if (width <= 800) {
            defaults.$summary.removeClass(style);
            defaults.$summary.find(".order-summary-wrapper").css("width", "100%");
        }
        else {
            if (defaults.$summary.hasClass(style)) {
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
        var summaryHandler = APL.onVisibilityChange(defaults.$addressWrapper, function (visible, topVisible, bottomVisible, inViewPoint) {
            var $summary = defaults.$summary;

            var style = "fixed_sidebar";
            if ($(window).width() <= 800) {
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

        $(window).on("DOMContentLoaded load resize scroll", summaryHandler);
    };

    function initValidation() {
        defaults.$customerForm.validate({ ignore: ".autocomplete" });
        $(defaults.shipping.firstName).rules("add", { required: true, maxlength: 50 });
        $(defaults.shipping.lastName).rules("add", { minlength: 1, maxlength: 50, required: true });
        $(defaults.shipping.address1).rules("add", { minlength: 1, maxlength: 50, required: true });
        $(defaults.shipping.city).rules("add", { minlength: 1, maxlength: 50, required: true });
        $(defaults.shipping.zip).rules("add", { minlength: 1, maxlength: 10, required: true });
        $(defaults.shipping.phone).rules("add", { required: true, minlength: 14, maxlength: 14 });
        $(defaults.shipping.email).rules("add", { required: true, email: true });
        $(defaults.shipping.country).rules("remove");
        $(defaults.shipping.state).rules("remove");

        $(defaults.shipping.phone).mask(checkout.address.phoneMask);
        $(defaults.billing.phone).mask(checkout.address.phoneMask);
        $(defaults.shipping.phone).on("change", function () {
            $(this).valid();
        });

        $(defaults.billing.phone).on("change", function () {
            $(this).valid();
        });

        $(".autocomplete").each(function() {
            $(this).on("change", function() {
                $(this).removeClass("valid");
            });
        });
    }

    function changeBillingRules(addressOption) {
        if (addressOption === "True") {
            $.each(defaults.billing, function (index, value) {
                $(value).rules("remove");
            });
        }
        else {
            $(defaults.billing.firstName).rules("add", { required: true, maxlength: 50 });
            $(defaults.billing.lastName).rules("add", { minlength: 1, maxlength: 50, required: true });
            $(defaults.billing.address1).rules("add", { minlength: 1, maxlength: 50, required: true });
            $(defaults.billing.city).rules("add", { minlength: 1, maxlength: 50, required: true });
            $(defaults.billing.zip).rules("add", { minlength: 1, maxlength: 10, required: true });
            $(defaults.billing.phone).rules("add", { required: true, minlength: 14, maxlength: 14 });
            $(defaults.billing.email).rules("add", { required: true, email: true });
            $(defaults.billing.country).rules("remove");
            $(defaults.billing.state).rules("remove");
        }
    }

    defaults.submitBtns.each(function() {
        $(this).on("click", function() {
            defaults.$customerForm.submit();
        });
    });

    defaults.$billingSameAsShipping.on("click", function () {
        var defs = defaults;
        var option = $(this).val();

        if (option === "True") {
            defs.$billingAddressBox.hide();
        }
        else {
            defs.$billingAddressBox.show();
        }

        changeBillingRules(option);
    });

    defaults.$newReturningCustomer.on("change", function () {
        var defs = defaults;
        var option = $(this).val();

        if (option === "True") {
            defs.$customerForm.show();
            defs.$loginForm.hide();
            defaults.submitBtns.show();
            defaults.continueBtns.show();
            defaults.choiceSeparator.show();
        }
        else {
            defs.$customerForm.hide();
            defs.$loginForm.show();
            defaults.submitBtns.hide();
            defaults.continueBtns.hide();
            defaults.choiceSeparator.hide();
        }
    });

    checkout.login = {
        submitBtn: $("#ap-login"),
        errorPlace: defaults.loginForm.find(".message-error"),
        form: defaults.loginForm,
        url: "/checkout/login",
        init: function() {
            var that = this;
            that.submitBtn.on("click", function() {
                that.submit.call(that);
            });
            initSubmitBtn(that.form.find("input"));
            
        },
        submit: function () {
            var that = this;
            APL.loader.show();

            $.ajax({
                cache: false,
                type: "POST",
                url: that.url,
                data: that.form.serialize(),
                success: function (data) {
                    if (data.success === true) {
                        window.location.reload();
                    }
                    else {
                        that.errorPlace.find("p").text(data.message);
                        that.errorPlace.show();
                        APL.loader.hide();
                    }
                },
                error: function (jqXhr) {
                    var status = jqXhr.status;
                    var msg;

                    if (status === 0) {
                        msg = "Connection failed. Verify network and try later";
                    }
                    else if (status === 404) {
                        msg = "Requested page not found.";
                    }
                    else {
                        msg = "Error occured while requesting page. Try again.";
                    }

                    that.errorPlace.find("p").text(msg);
                    that.errorPlace.show();
                    APL.loader.hide();
                }
            });
        }
    }

    checkout.address = {
        phoneMask: "(999)?-999-9999",
        $countries: $(".available-countries"),
        $states: $(".available-states"),
        init: function () {
            var self = this;
            var s = defaults;

            self.$countries.each(function () {
                $(this).on("change", function (e) {
                    APL.loader.show();
                    var selectedCountryId = $(this).val();
                    var state = $(this).parents("li").prev().find(".available-states");

                    var payload = {
                        countryId: selectedCountryId,
                        addSelectStateItem: true
                    };

                    if (e && e.detail) {
                        payload.defaultStateCode = e.detail.data;
                    }

                    $.ajax({
                        cache: false,
                        type: "GET",
                        url: "/country/getstatesbycountryid",
                        data: payload,
                        success: function (data) {
                            state.find("option").remove();
                            if (data.length > 1) {
                                $.each(data, function (i, obj) {
                                    var removeItem = 0;
                                    if (i !== removeItem) {
                                        state.append($("<option></option>").attr({ value: obj.id, data_code: obj.code }).text(obj.name));
                                        if (obj.isSelected) {
                                            state.val(obj.id);
                                        }
                                    }
                                });

                                state.parents(".states-box").removeClass("hide");
                            } else {
                                state.parents(".states-box").addClass("hide");
                            }

                            APL.loader.hide();
                        },
                        error: function () {
                            // TODO: handle errors

                            APL.loader.hide();
                        }
                    });
                });
            });
        }
    };

    checkout.discountBox = {
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

    checkout.giftCardBox = {
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

    checkout.init = function () {
        checkout.address.init();
        checkout.login.init();
        checkout.discountBox.init();
        checkout.giftCardBox.init();
        initValidation();
        initPayPal();
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

    

    $(document).ready(function () {
        APL.init();
        checkout.init();
        

        initSummary();

        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);
        resize($(window).width());
        APL.loader.hide();
    });
})(jQuery, document, window);
