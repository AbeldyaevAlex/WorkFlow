(function ($, document, window) {
    APL.checkout = APL.checkout || {};
    var checkout = APL.checkout;

    var defaults = {
        editBtns: $(".edit-address-btn"),
        $paymentWrapper: $(".wrapper_paiment_page"),
        $paymentForm: $("#checkout-payment-method-info-form"),
        paymentFormSelector: "#checkout-payment-method-info-form",
        $form: $("#shopping-cart-form"),
        checkoutButtonSelector: "#checkout-btn-proceed",
        $summary: $(".order-summary-container"),
        proceedCheckoutButtons: $(".proceed-checkout-button"),
        proceedCheckoutButtonMobile: $(".proceed-checkout-button-mobile"),
        $swapOrderNumber: $("#swapOrderNumber"),
        $paymentInfoErrorMessage: $(".payment-info-error-message"),
        $paymentMethods: $("[name='paymentmethod']"),
        $amazonLabel: $("#amazon-label"),
        $paypalBtn: $("#paymentmethod_PayPal"),
        $paypalLabel: $("#paypal-label"),
        paypalUrl: "/Plugins/PaymentPayPalExpressCheckout/SetExpressCheckout?isCredit=false&cancelUrl=%2Fcheckout%2Fpayment&returnUrl=%2Fcheckout%2Fpaypal",
        paymentInfoCcErrorMessage: ".payment-info-cc-error-message",
        errorText: "An error occured when request. Please try again",
        ccInputsSelector: ".cc-payment-form input",
        adminPaypalTransactionIdSelector: "#admin-paypal-transaction-id",
        selectedPaymentMethodName: $("label[disabled]").prev("[name='paymentmethod']").val(),
        continueBtns: $(".continue-shopping-btn").parent(".buttons")
    };

    $.extend(defaults, APL.addressTypeCode);

    var initSubmitBtn = function(inputs) {
        if (!APL.nullOrUndef(inputs)) {
            inputs.each(function () {
               $(this).keypress(function (e) {
                    if (e.which === 13) {
                        e.preventDefault();
                        checkout.payment.setPaymentInfoAndConfirm();
                    }
                });
            });
        }
    }

    var recalculateWidth = function() {
        var width = Math.round($(".master-wrapper-main").width() * 0.29) - 15;
        defaults.$summary.find(".order-summary-wrapper").css("width", width + "px");
    };

    var resize = function (width) {
        var style = "fixed_sidebar";
        if (width <= 1000) {
            defaults.proceedCheckoutButtons.hide();
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

        defaults.proceedCheckoutButtons.on('click', function () {
            checkout.payment.setPaymentInfoAndConfirm.call(checkout.payment);
        });

        defaults.proceedCheckoutButtonMobile.on('click', function () {
            checkout.payment.setPaymentInfoAndConfirm.call(checkout.payment);
        });
        
        defaults.continueBtns.show();

        var proceedCheckoutHandler = APL.onVisibilityChange(defaults.$paymentForm, function (visible, topVisible, bottomVisible, inViewPoint) {
            var proceedCheckoutButtons = defaults.proceedCheckoutButtons;

            if ($(window).width() <= 1000) {
                return;
            }

            if (visible) {
                proceedCheckoutButtons.hide();
            } else {
                proceedCheckoutButtons.show();
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

        $(window).on("DOMContentLoaded load resize scroll", proceedCheckoutHandler);
        $(window).on("DOMContentLoaded load resize scroll", summaryHandler);

    };
    
    function initPayPal() {
        var s = defaults;
        window.paypalCheckoutReady = function () {
            s.$paypalBtn.on("DOMNodeInserted", function (e) {
                var $el = $(e.target).find("button.paypal-button");
                if ($el) {
                    $el.hide();
                    $el.html("");
                    s.$paypalBtn.width(0).height(0);
                }
            });

            window.paypal.checkout.setup("DEGKRMMH8T6JY", {
                environment: "production",
                buttons: [
                    {
                        container: [s.$paypalBtn[0]],
                        type: "checkout",
                        color: "gold",
                        size: "medium",
                        shape: "rect"
                    }
                ],
                click: function () {
                    event.preventDefault();
                    window.paypal.checkout.reset();
                    window.paypal.checkout.initXO();

                    var url = s.paypalUrl;
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

            s.$paypalLabel.on("click", function () {
                var $btn = s.$paypalBtn.find("button.paypal-button");
                if ($btn) {
                    $btn.trigger("click");
                }
            });
        };

        var payPalScript = document.createElement("script");
        payPalScript.setAttribute("type", "text/javascript");
        payPalScript.setAttribute("src", "https://www.paypalobjects.com/api/checkout.js");
        payPalScript.setAttribute("async", "");
        document.body.appendChild(payPalScript);
    };

    function initAmazon() {
        defaults.$amazonLabel.on("click", function () {
            APL.amazon.login();
            return false;
        });
    };

    function initAdminPayPal() {
        var transactionIdInput = $(defaults.adminPaypalTransactionIdSelector);
        if (transactionIdInput.length > 0) {
            initSubmitBtn(transactionIdInput);
        }

        defaults.$paymentForm.validate();
        transactionIdInput.rules("add", { required: true });
    }

    checkout.paymentMethod = {
        setPaymentMethodUrl: "/checkout/set-payment-method",
        $labels: $(".list_radiobuttons").find(".method-list li label"),
        cvvInfoBtn: ".cvv-info-btn",
        cvvInfo: "#cvv2-info-container",
        initCvvInfo: function() {
            var that = this;
            $(this.cvvInfo).neysModal({
                onReady: function() {
                    $(that.cvvInfo).show();
                }
            });

            $(this.cvvInfoBtn).on("click",
                function() {
                    $(that.cvvInfo).data("neysModal").show();
                });
        },
        set: function (paymentMethodName) {
            var that = this;
            defaults.selectedPaymentMethodName = paymentMethodName;

            that.$labels.each(function () {
                $(this).attr("disabled", true);
            });

            APL.loader.show();

            $.ajax({
                cache: false,
                url: that.setPaymentMethodUrl,
                data: { paymentmethod: paymentMethodName },
                type: "post",
                success: function (response) {

                    that.$labels.each(function () {
                        var value = $(this).prev("[name='paymentmethod']").val() === paymentMethodName ? true : false;
                        $(this).attr("disabled", value);
                    });

                    //$("").attr("disabled", true);

                    if (response.error === 0) {
                        defaults.$paymentForm.html(response.html);
                        if (defaults.$paymentMethods.length > 0) {
                            if (paymentMethodName === "Payments.AuthorizeNet") {
                                that.initCvvInfo();
                                initSubmitBtn($(defaults.ccInputsSelector));
                            }
                            if (paymentMethodName === "Payments.AdminPaypal") {
                                initAdminPayPal();
                            }
                        }
                    } else {
                        if (typeof response.message !== "undefined") {
                            defaults.$paymentInfoErrorMessage.html(response.message);
                        }

                        if (typeof response.redirect !== "undefined") {
                            APL.redirect(response.redirect);
                        }
                    }
                },
                error: function () {
                    defaults.$paymentInfoErrorMessage.html(defaults.errorText);
                },
                complete: function () {
                    APL.loader.hide();
                }
            });
        },
        init: function() {
            var that = this;
            if (!("ApplePaySession" in window)) {
                var applePayLabel = $("#applepay-label");
                if (applePayLabel) {
                    applePayLabel.parents("li").remove();
                }
            } 

            defaults.$paymentMethods.each(function () {
                var $field = $(this);
                
                if ($(this).val() === "Payments.AuthorizeNet") {
                    that.initCvvInfo();
                    initSubmitBtn($(defaults.ccInputsSelector));
                }

                if ($(this).val() === "Payments.AdminPaypal") {
                    initAdminPayPal();
                }

                if ($(this).attr("id") === "paymentmethod_PayPal" || $(this).attr("id") === "paymentmethod_PayPalCredit") {
                    initPayPal();
                    return;
                }
                if ($(this).attr("id") === "paymentmethod_Amazon") {
                    initAmazon();
                    return;
                }

                var label = $field.next("label");
                label.on("click", function () { that.set($field.val()) });
            });
        }
    };

    checkout.payment = {
        setPaymentUrl: "/checkout/set-payment-info",
        changeProceedAvailability: function () {
            if ($("input[name='shippingoption']:radio").length > 0) {
                $(defaults.checkoutButtonSelector).removeAttr("disabled");
            }
            else {
                $(defaults.checkoutButtonSelector).attr("disabled", true);
            }
        },
        setPaymentInfoAndConfirm: function () {
            var that = this;
            APL.loader.show();
            $(defaults.checkoutButtonSelector).attr("disabled", true);
            if (defaults.$swapOrderNumber.length > 0) {
                if ($(defaults.paymentFormSelector + " input[name='SwapOrderNumber']").length > 0) {
                    $(defaults.paymentFormSelector + " input[name='SwapOrderNumber']").val(defaults.$swapOrderNumber.val());
                } else {
                    $("<input name='SwapOrderNumber' type='hidden' value='" + defaults.$swapOrderNumber.val() + "'/>").appendTo(defaults.$paymentForm);
                }
            }

            $(defaults.paymentInfoCcErrorMessage).html("");
            defaults.$paymentInfoErrorMessage.html("");
            if (!defaults.$paymentForm.valid()) {
                APL.loader.hide();
                $(defaults.checkoutButtonSelector).removeAttr("disabled");
                return;
            }
            
            $.ajax({
                cache: false,
                url: that.setPaymentUrl,
                data: defaults.$paymentForm.serialize(),
                type: "POST",
                success: function (response) {
                    switch (response.error) {
                        case 0:
                            APL.redirect(response.redirect);
                            return;
                        case 1:
                            if (!APL.nullOrUndef(response.message) && response.message !== "") {
                                defaults.$paymentInfoErrorMessage.html(response.message);
                            }

                            if (!APL.nullOrUndef(response.redirect) && response.redirect !== "") {
                                APL.redirect(response.redirect);
                                return;
                            }

                            that.changeProceedAvailability(); 
                            initSubmitBtn($(defaults.ccInputsSelector));
                            APL.loader.hide();
                            break;
                        case 2:
                            defaults.$paymentForm.html(response.html);
                            checkout.paymentMethod.initCvvInfo();
                            initSubmitBtn($(defaults.ccInputsSelector));
                            that.changeProceedAvailability();
                            APL.loader.hide();
                            break;
                        case 3:
                            if (!APL.nullOrUndef(response.message) && response.message !== "") {
                                defaults.$paymentInfoErrorMessage.html(response.message);
                            }

                            that.changeProceedAvailability();
                            initSubmitBtn($(defaults.ccInputsSelector));
                            APL.loader.hide();
                            break;
                    }
                },
                error: function (jqXhr, msg) {
                    defaults.$paymentInfoErrorMessage.html(defaults.errorText);
                    initSubmitBtn($(defaults.ccInputsSelector));
                    that.changeProceedAvailability(); 
                    APL.loader.hide();
                }
            });
        },
        init: function () {
            var that = this;
            defaults.$paymentForm.on("click", defaults.checkoutButtonSelector, function () {
                that.setPaymentInfoAndConfirm.call(that);
            });
            this.changeProceedAvailability();
        }
    };

    checkout.shippingMethod = {
        setMethodUrl: "/simplecheckout/SetShippingMethod",
        options: $("input[name='shippingoption']"),
        update: function updateApplePayShippingMethod() {
            if (typeof applePaymentRequest === "undefined") {
                return;
            }

            var shippingMethods = $(applePaymentRequest.shippingMethods);
            if (shippingMethods.length > 1) {
                var selectedShippingMethod = $("input[name='shippingoption']:checked");
                var selectedShippingMethodId = selectedShippingMethod.attr("id");
                var newShippingMethods = shippingMethods.filter(function (index) {
                    return this.identifier != selectedShippingMethodId;
                }).toArray();

                selectedShippingMethod = shippingMethods.filter(function (index) {
                    return this.identifier == selectedShippingMethodId;
                })[0];

                newShippingMethods.unshift(selectedShippingMethod);
                applePaymentRequest.shippingMethods = newShippingMethods;
            }
        },
        set: function (value, controlLoaderBehaviour) {
            var that = this;

            if (typeof controlLoaderBehaviour === "undefined") {
                APL.loader.show();
            } 
            
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


                    if (typeof controlLoaderBehaviour === "undefined") {
                        APL.loader.hide();
                    } 

                    that.update();
                },
                complete: function () {
                    try {
                        // TODO: to figure out
                        if (APL && APL.cart && APL.cart.affirmPromotional) {
                            APL.cart.affirmPromotional.update();
                            if (affirm && affirm.ui) {
                                affirm.ui.refresh();
                            }
                        }

                        // to refresh Affirm JS object
                        if (defaults.selectedPaymentMethodName === "Payments.Affirm") {
                            checkout.paymentMethod.set(defaults.selectedPaymentMethodName);
                        }
                    } catch (ex) {
                        if (typeof controlLoaderBehaviour === "undefined") {
                            APL.loader.hide();
                        }
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
            that.update();

            $.each(that.options, function () {
                $(this).on("click", function () { that.set($(this).val()) });
            });
        }
    };

    checkout.address = {
        phone: $(".phone"),
        phoneMask: "(999)?-999-9999",
        closeBtn: $(".close-btn"),
        billing: {
            type: defaults.billingCode,
            editBtns: $(".edit-billing-address-btn"),
            dialog: $("#edit-billing-address-wnd"),
            create: {
                block: $("#create-billing-address"),
                form: $("#create-billing-address-form"),
                cancelBtn: $("#create-billing-address-form").find(".cancel-button"),
                saveBtn: $("#create-billing-address-form").find(".submit-btn"),
                phone: $("#create-billing-address-form").find(),
                countries: $("#create-billing-address-form").find(".available-countries"),
                states: $("#create-billing-address-form").find(".available-states"),
                url: "/checkout/create-address-billing",
                inputs: APL.addressSelectors.billing
            },
            edit: {
                block: $("#edit-billing-address"),
                container: $(".edit-billing-address-container"),
                url: "/checkout/save-billing-address",
                saveBtn: $("#edit-billing-address").find(".save-btn"),
                cancelBtn: $("#edit-billing-address").find(".cancel-btn"),
                form: $("#edit-billing-address-form"),
                countriesSelector: ".available-countries",
                statesSelector: ".available-states",
                countries: {},
                states: {},
                mvcHtmlPrefix: "billing_Edit_",
                getRequestUrl: "/checkout/edit-address"
            },
            list: {
                block: $("#billing-address-list"),
                createBtn: $("#billing-address-list").find(".create-btn"),
                url: "/checkout/set-address",
                options: $("input[name='existing-billing-address']"),
                editBtns: $("#billing-address-list").find(".edit-button"),
                errors: $("#billing-address-list").find(".error-messages"),
                retryBtn: $("#billing-address-list").find(".error-messages .retry-btn"),
                selectedOptionSelector: "input[name='existing-billing-address']:checked"
            }
        },
        shipping: {
            type: defaults.shippingCode,
            editBtns: $(".edit-shipping-address-btn"),
            dialog: $("#edit-shipping-address-wnd"),
            create: {
                block: $("#create-shipping-address"),
                form: $("#create-shipping-address-form"),
                createBtn: $("#create-shipping-address-form").find(".create-btn"),
                cancelBtn: $("#create-shipping-address-form").find(".cancel-button"),
                saveBtn: $("#create-shipping-address-form").find(".submit-btn"),
                phone: $("#create-shipping-address-form").find(),
                countries: $("#create-shipping-address-form").find(".available-countries"),
                states: $("#create-shipping-address-form").find(".available-states"),
                url: "/checkout/create-address-shipping",
                inputs: APL.addressSelectors.shipping
            },
            edit: {
                block: $("#edit-shipping-address"),
                container: $(".edit-shipping-address-container"),
                url: "/checkout/save-shipping-address",
                saveBtn: $("#edit-shipping-address").find(".save-btn"),
                cancelBtn: $("#edit-shipping-address").find(".cancel-btn"),
                form: $("#edit-shipping-address-form"),
                countries: {},
                countriesSelector: ".available-countries",
                statesSelector: ".available-states",
                states: {},
                mvcHtmlPrefix: "shipping_Edit_",
                getRequestUrl: "/checkout/edit-address"
            },
            list: {
                block: $("#shipping-address-list"),
                createBtn: $("#shipping-address-list").find(".create-btn"),
                editBtns: $("#shipping-address-list").find(".edit-button"),
                url: "/checkout/set-address",
                options: $("input[name='existing-shipping-address']"),
                errors: $("#shipping-address-list").find(".error-messages"),
                retryBtn: $("#shipping-address-list").find(".error-messages .retry-btn"),
                selectedOptionSelector: "input[name='existing-shipping-address']:checked"
            }
        },
        init: function () {
            var that = this;
            var billing = that.billing;
            var shipping = that.shipping;

            getStates(billing.create);
            getStates(shipping.create);
            initValidation(billing.create);
            initValidation(shipping.create);

            that.phone.each(function () {
                $(this).mask(that.phoneMask);
                $(this).on("change", function () {
                    $(this).valid();
                });
            });

            billing.dialog.neysModal({
                onClosed: function () {
                    closeModal(billing);
                },
                customClass: "edit-address-wnd",
                display: "block"
            });

            shipping.dialog.neysModal({
                onClosed: function () {
                    closeModal(shipping);
                },
                customClass: "edit-address-wnd",
                display: "block"
            });

            billing.create.cancelBtn.on("click", function () {
                billing.create.block.hide();
                billing.list.block.show();
            });

            shipping.create.cancelBtn.on("click", function () {
                shipping.create.block.hide();
                shipping.list.block.show();
            });

            billing.list.createBtn.on("click", function () {
                var create = billing.create;
                billing.list.block.hide();
                create.block.show();
            });

            shipping.list.createBtn.on("click", function () {
                var create = shipping.create;
                shipping.list.block.hide();
                create.block.show();
            });

            billing.editBtns.each(function () {
                $(this).on("click", function () {
                    billing.dialog.data("neysModal").show();
                    billing.dialog.show();
                });
            });

            shipping.editBtns.each(function () {
                $(this).on("click", function () {
                    shipping.dialog.data("neysModal").show();
                    shipping.dialog.show();
                });
            });

            billing.create.saveBtn.on("click", function () {
                createAddress(billing.create, billing.type);
            });

            shipping.create.saveBtn.on("click", function () {
                createAddress(shipping.create, shipping.type);
            });

            billing.list.options.each(function () {
                $(this).on("click", function () {
                    applyAddress(billing.list, billing.type, $(this).val());
                });
            });

            shipping.list.options.each(function () {
                $(this).on("click", function () {
                    applyAddress(shipping.list, shipping.type, $(this).val());
                });
            });

            billing.list.editBtns.each(function () {
                $(this).on("click", function () {
                    editAddressGet(billing.edit, billing.type, $(this).data("address-id"), billing.list.block);
                });
            });

            shipping.list.editBtns.each(function () {
                $(this).on("click", function () {
                    editAddressGet(shipping.edit, shipping.type, $(this).data("address-id"), shipping.list.block);
                });
            });

            billing.edit.saveBtn.on("click", function () {
                editAddressPost(billing.edit, billing.type);
            });

            shipping.edit.saveBtn.on("click", function () {
                editAddressPost(shipping.edit, shipping.type);
            });

            billing.edit.cancelBtn.on("click", function () {
                closeModal(billing);
            });

            shipping.edit.cancelBtn.on("click", function () {
                closeModal(shipping);
            });

            shipping.list.retryBtn.on("click", function () {
                applyAddress(shipping.list, shipping.type);
            });

            billing.list.retryBtn.on("click", function () {
                applyAddress(billing.list, billing.type);
            });

            that.closeBtn.on("click", function () {
                billing.dialog.data("neysModal").hide();
                billing.dialog.hide();
                shipping.dialog.data("neysModal").hide();
                shipping.dialog.hide();
            });

            function closeModal(address) {
                var autocompletes = document.getElementsByName('autocomplete');
                for (var i = 0; i < autocompletes.length; i++) {
                    autocompletes[i].value = '';
                }

                address.edit.block.hide();
                address.create.block.hide();
                address.list.block.show();
            };

            function setSelectOptions(selectList, items) {
                var select = selectList[0];

                function removeOptions(select) {
                    while (select.options.length) {
                        select.remove(0);
                    }
                }

                removeOptions(select);

                for (var i = 0; i < items.length; i++) {
                    var removeItem = 0;
                    if (i !== removeItem) {
                        var item = items[i];
                        var option = document.createElement('option');
                        option.value = item.id;
                        option.text = item.name;
                        option.setAttribute('data_code', item.code);
                        option.selected = item.isSelected;

                        select.appendChild(option);
                    }
                }
            }

            function getStates(address) {
                address.countries.each(function () {
                    $(this).on("change", function (event) {
                        var selectedCountryId = $(this).val();
                        var states = address.states;
                        var stateCode = states.data("code");
                        if (event && event.detail) {
                            stateCode = event.detail.data;
                        }

                        APL.loader.show();
                        $.ajax({
                            cache: false,
                            type: "GET",
                            url: "/country/getstatesbycountryid",
                            data: { countryId: selectedCountryId, addSelectStateItem: true, defaultStateCode: stateCode },
                            success: function (data) {
                                states.find("option").remove();
                                if (data.length === 1) {
                                    address.states.parents(".states-box").addClass("hide");
                                } else if (data) {
                                    setSelectOptions(address.states, data);
                                    address.states.parents(".states-box").removeClass("hide");
                                }
                            },
                            error: function () {
                                // TODO: handle errors
                            },
                            complete: function() {
                                APL.loader.hide();
                            }
                        });
                    });
                });
            };

            function createAddress(address, type) {
                var form = address.form;

                if (form.valid()) {
                    APL.loader.show();
                    $.ajax({
                        cache: false,
                        type: "POST",
                        url: address.url,
                        data: address.form.serialize(),
                        success: function (data) {
                            if (data.success === true) {
                                window.location.reload();
                            }
                            else {
                                $(data.errors).each(function () {
                                    address.form.find("#" + (type === 1 ? "billing" : "shipping") + "_NewAddress_" + this.PropertyName).addClass("input-validation-error");
                                });
                                APL.loader.hide();
                            }
                        },
                        error: function (jqXhr, error, errorThrown) {
                            APL.loader.hide();
                            APL.messageBar.show(defaults.errorText, "error", 0);
                        }
                    });
                }
            };

            function applyAddress(address, type) {
                var id = $(address.selectedOptionSelector).val();
                var url = address.url + "/" + id + "?type=" + type;
                APL.loader.show();

                $.ajax({
                    cache: false,
                    type: "GET",
                    url: url,
                    success: function () {
                        window.location.reload();
                    },
                    error: function () {
                        address.errors.attr("display", "inline-block");
                        APL.loader.hide();
                    }
                });
            };

            function editAddressGet(address, type, id, toToggle) {
                var url = address.getRequestUrl + "/" + id + "?type=" + type;
                var form = address.form;
                APL.loader.show();

                $.ajax({
                    cache: false,
                    type: "GET",
                    url: url,
                    success: function (data) {
                        toToggle.hide();
                        var prefix = address.mvcHtmlPrefix;
                        address.inputs = {
                            firstName: form.find("#" + prefix + "FirstName"),
                            lastName: form.find("#" + prefix + "LastName"),
                            address1: form.find("#" + prefix + "Address1"),
                            address2: form.find("#" + prefix + "Address2"),
                            city: form.find("#" + prefix + "City"),
                            zip: form.find("#" + prefix + "ZipPostalCode"),
                            phone: form.find("#" + prefix + "PhoneNumber"),
                            email: form.find("#" + prefix + "Email"),
                            country: form.find("#" + prefix + "Country"),
                            state: form.find("#" + prefix + "State"),
                            company: form.find("#" + prefix + "Company"),
                            id: form.find("#" + prefix + "Id"),
                            typeCode: form.find("#" + prefix + "TypeCode")
                        };

                        function setSelectOptions(selectList, items) {
                            var select = selectList[0];

                            function removeOptions(select) {
                                while (select.options.length) {
                                    select.remove(0);
                                }
                            }

                            removeOptions(select);

                            for (var i = 0; i < items.length; i++) {
                                var removeItem = 0;
                                if (i !== removeItem) {
                                    var item = items[i];
                                    var option = document.createElement('option');
                                    option.value = item.Value;
                                    option.text = item.Text;
                                    option.setAttribute('data_code', item.HtmlAttributes.data_code);
                                    option.selected = item.Selected;

                                    select.appendChild(option);
                                }
                            }
                        }

                        address.countries = address.form.find(address.countriesSelector);
                        address.states = address.form.find(address.statesSelector);
                        address.countries.val(data.CountryId);
                        address.states.val(data.StateProvinceId);
                        getStates(address);
                        if (data.AvailableStates.length === 1) {
                            address.states.parents(".states-box").addClass("hide");
                        } else {
                            setSelectOptions(address.states, data.AvailableStates);
                            address.states.parents(".states-box").removeClass("hide");
                        }

                        address.inputs.firstName.val(data.FirstName);
                        address.inputs.lastName.val(data.LastName);
                        address.inputs.address1.val(data.Address1);
                        address.inputs.address2.val(data.Address2);
                        address.inputs.company.val(data.Company);
                        address.inputs.city.val(data.City);
                        address.inputs.zip.val(data.ZipPostalCode);
                        address.inputs.phone.val(data.PhoneNumber);
                        address.inputs.email.val(data.Email);
                        address.inputs.id.val(data.Id);
                        address.inputs.typeCode.val(data.TypeCode);

                        initValidation(address);

                        address.block.show();
                    },
                    error: function () {
                        APL.messageBar.show(defaults.errorText, "error", 0);
                    },
                    complete: function () {
                        APL.loader.hide();
                    }
                });
            };

            function editAddressPost(address) {
                var url = address.url;

                if (address.form.valid()) {
                    APL.loader.show();
                    $.ajax({
                        cache: false,
                        type: "POST",
                        data: address.form.serialize(),
                        url: address.url,
                        success: function (data) {
                            if (data.success === true) {
                                window.location.reload();
                            }
                            else if (data.success === false) {
                                if (data.errors != null) {
                                    $(data.errors).each(function () {
                                        address.form.find(address.mvcHtmlPrefix + this.PropertyName).addClass("input-validation-error");
                                        APL.loader.hide();
                                    });
                                } else if(data.message != null){
                                    APL.messageBar.show(data.message, "error", 0);
                                    APL.loader.hide();
                                }
                            }
                        },
                        error: function () {
                            APL.messageBar.show(defaults.errorText, "error", 0);
                            APL.loader.hide();
                        }
                    });
                }
            };

            function initValidation(address) {
                var inputs = address.inputs;

                address.form.validate();
                $(inputs.firstName).rules("add", { required: true, maxlength: 50 });
                $(inputs.lastName).rules("add", { minlength: 1, maxlength: 50, required: true });
                $(inputs.address1).rules("add", { minlength: 1, maxlength: 50, required: true });
                $(inputs.city).rules("add", { minlength: 1, maxlength: 50, required: true });
                $(inputs.zip).rules("add", { minlength: 1, maxlength: 10, required: true });
                $(inputs.phone).rules("add", { required: true, minlength: 14, maxlength: 14 });
                $(inputs.email).rules("add", { required: true, email: true });
                $(inputs.country).rules("remove");
                $(inputs.state).rules("remove");

                $(".phone").mask(that.phoneMask);
            };
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

$(document).ready(function () {
        APL.init();
        checkout.shippingMethod.init();
        checkout.paymentMethod.init();
        checkout.address.init();
        checkout.payment.init();
        checkout.discountBox.init();
        checkout.giftCardBox.init();
        initSummary();

        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);
        resize($(window).width());
});

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

})(jQuery, document, window);
