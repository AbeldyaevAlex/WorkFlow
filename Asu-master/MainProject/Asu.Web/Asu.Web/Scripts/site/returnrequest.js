(function ($) {
    APL.returnrequest = APL.returnrequest || {};

    var defaults = {
        $qty: $("#return-request-form").find("input.dd-return-qty"),
        $form: $("#return-request-form"),
        $btn: $("#submit-return-request"),
        $reasons: $("select.return-reason"),
        validExtensions: [".jpg", ".jpeg", ".bmp", ".gif", ".png"],
        disableValidationClass: "novalidation"
    };

    var validatorOptions;

    var initValidation = function() {
        var $form = this.s.$form;
        validatorOptions = {
            ignore: "." + this.s.disableValidationClass,
            settings: {
                errorClass:"input-validation-error",
                errorElement:"span"
            },
            errorPlacement: function(error, element) {
                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent().siblings().last());
                }
                else if (element.is("select")) {
                    error.insertAfter(element.next("span.ui-selectmenu-button"));
                }
                else if (element.hasClass("qty-input")) {
                    error.data("val-number", "");
                }
                else {
                    error.insertAfter(element);
                }
            }
        };

        if (!APL.undef($form.data("validator"))) {
            $.extend($form.validate().settings, validatorOptions);
        } else {
            $form.validate(validatorOptions);
        }

        $.validator.setDefaults({
            ignore: "." + this.s.disableValidationClass
        });

        $.validator.addMethod("qtyselected", function (value, element, params) {
            return $(params).filter(function() { return this.value > 0; }).length > 0;
        }, "Please select quantity for each item you want to return");

        $.validator.addMethod("reason", function (value, element, params) {
            return $("#" + params).length === 0 || $("#" + params).val() === 0 || value > 0;
        }, "Please select a reason");

        $.validator.addMethod("comment", function (value, element, params) {
            return $("#" + params).length === 0 || $("#" + params).val() === 0 || value.length > 0;
        }, "Please enter your comment");

        $.validator.addMethod("qtytoship", function (value, element, params) {
            return $(params).filter(function () { return this.value > 0; }).length > 0;
        }, "Please select quantity for each item you shipped");

        $.validator.addMethod("tracking", function (value) {
           return value.length > 0;
        }, "Please type a tracking number");

        $.validator.addMethod("carrier", function (value) {
            return value > 0 || value !== null;
        }, "Please select a carrier");

        $.validator.unobtrusive.adapters.addSingleVal("qtyselected", "selector");
        $.validator.unobtrusive.adapters.addSingleVal("reason", "itemqty");
        $.validator.unobtrusive.adapters.addSingleVal("comment", "itemqty");
        $.validator.unobtrusive.adapters.addSingleVal("qtytoship", "selector");
        $.validator.unobtrusive.adapters.addSingleVal("tracking", "selector");
        $.validator.unobtrusive.adapters.addSingleVal("carrier", "selector");
    };

    APL.returnrequest = {
        init: function (options) {
            var self = this;
            this.s = $.extend({}, defaults, options);
            this.s.$reasons.selectmenu({
                change: function () {
                    self.s.$form.validate().element(this);
                    try {
                        self.initiationGroup.changeVisibility();
                    } catch (e) {

                    } 
                    
                }
            });

            function refreshBtn() {
                var $btn = self.s.$btn;
                var empty = self.s.$qty.filter(function () {
                    return this.value > 0;
                });

                if (empty && empty.length > 0) {
                    $btn.attr("disabled", false);
                } else {
                    $btn.attr("disabled", true);
                }
            };

            this.s.$qty.each(function () {
                $(this).on("change",
                    function () {
                        var thubOrderId = $(this).attr("data-item");
                        var returnItem = $("#" + thubOrderId);
                        if ($(this).val() > 0) {
                            returnItem.find("select, textarea").removeClass(self.s.disableValidationClass);
                            returnItem.show();
                        } else {
                            returnItem.find("select, textarea").addClass(self.s.disableValidationClass);
                            returnItem.hide();

                            var $reasonSelectMenu = returnItem.find("select.return-reason");
                            $reasonSelectMenu[0].selectedIndex = 0;
                            $reasonSelectMenu.selectmenu("refresh");
                        }

                        var refund = "$" + (parseFloat($(this).val()) * parseFloat($(this).attr("data-price"))).toFixed(2);
                        $(this).closest("td").next().html(refund);
                        returnItem.find(".quantity-x").html($(this).val() + "x");

                        var item = $("#" + thubOrderId + "-eligible-qty");
                        item.text(parseInt($("#e-qty" + thubOrderId).val()) - parseInt($(this).val()) + "x");
                        returnItem.find(".return-items-refund").html(refund);
                        refreshBtn();

                        try {
                            self.initiationGroup.changeVisibility();
                        } catch (e) {
                            
                        } 
                    });
            });

            this.s.$qty.neysQtyInput();

            this.s.$btn.on("click",
                function () {
                    if (self.s.$form.valid()) {
                        $(this).attr("disabled", true);
                        self.s.$form.submit();
                        return true;
                    }
                    return false;
                });

            initValidation.call(this);
        },
        validateFile: function (input) {
            var validExtensions = this.s.validExtensions;
            if (input.type === "file") {
                var fileName = input.value;
                if (fileName.length > 0) {
                    var valid = false;
                    for (var j = 0; j < validExtensions.length; j++) {
                        var extension = validExtensions[j];
                        if (fileName.substr(fileName.length - extension.length, extension.length).toLowerCase() ===
                            extension.toLowerCase()) {
                            valid = true;
                            break;
                        }
                    }

                    if (!valid) {
                        alert("Sorry, " +
                            fileName +
                            " is invalid, allowed picture extensions are: " +
                            validExtensions.join(", "));
                        input.value = "";
                        return false;
                    }

                    if (input.files[0].size > 5242880) {
                        alert("Sorry, maximum size of a picture is 5MB");
                        input.value = "";
                        return false;
                    }
                }
            }

            var filename = $(input).val().replace(/C:\\fakepath\\/i, '');
            $("span[for=" + $(input).attr("id") + "]").text(filename);

            return true;
        },
        initiationGroup: {
            groupId: null,
            changeVisibility: function () {
                var that = this;
                var anyQtySelected = defaults.$qty.filter(function () {
                    return $(this).val() > 0;
                });

                var anyReasonSelected = defaults.$reasons.filter(function () {
                    return $(this).val() > 0;
                });

                if (anyQtySelected.length <= 1) {
                    this.groupId = null;
                } else {
                    if (anyReasonSelected.length === 0) {
                        this.groupId = null;
                    } else {
                        if (this.groupId == null) {
                            this.groupId = anyReasonSelected.find("option:selected").first().data("group-id");
                        }
                    }
                }

                refresh();

                function refresh() {
                    var disableOptions;
                    if (that.groupId == null) {
                        var options = defaults.$reasons.find(":not(option[value='0'])"); 
                        refreshOptions(options, true);

                        defaults.$reasons.each(function () {
                            $(this).selectmenu("refresh");
                        });

                        return;

                    } else if (that.groupId === 0) {
                        disableOptions = defaults.$reasons.find("optgroup[data-group-id='" + 1 + "'], " + "option[data-group-id='" + 1 + "']");
                    } else {
                        disableOptions = defaults.$reasons.find("optgroup[data-group-id='" + 0 + "'], " + "option[data-group-id='" + 0 + "']");
                    }

                    refreshOptions(disableOptions, false);

                    defaults.$reasons.each(function() {
                        $(this).selectmenu("refresh");
                    });

                    function refreshOptions(options, enable) {
                        options.each(function () {
                            $(this).attr("disabled", !enable);
                        });
                    }
                }
            }
        },
        helper: {
            $dialog: $("#helper"),
            $loader: $(".box-loader"),
            $helpForm: $("#helper-form"),
            $btnHelp: $("#open-help"),
            $submit: $("#helper-submit"),
            $errors: $("#helper-errors"),
            $success: $("#helper-success"),
            $cancel: $("#helper-cancel"),
            $close: $("#helper-close"),
            $retry: $("#retry"),
            $email: $("#helper-email"),
            $phone: $("#helper-phone"),
            $comment: $("#customer-comment"),
            init: function () {
                var self = this;
                var $dialog = this.$dialog;
                if (APL.undef($dialog) || !$.fn.neysModal) {
                    return;
                }

                $dialog.neysModal({
                    trigger: self.$btnHelp
                });

                self.$btnHelp.click(function () {
                    $dialog.show();
                    self.$success.hide();
                    self.$helpForm.show();
                    return false;
                });

                function close() {
                    $dialog.data("neysModal").hide();
                };

                self.$comment.rules("add", {
                    required: true,
                    minlength: 2,
                    messages: {
                        required: "Comment is a required field",
                        minlength: jQuery.validator.format("Please, at least {0} characters are necessary")
                    }
                });

                self.$email.rules("add", {
                    required: true,
                    email: true,
                    messages: {
                        required: "Email is a required field",
                        email: "Email must be in the valid format"
                    }
                });

                self.$phone.rules("add", {
                    required: true,
                    digits: true,
                    minlength: 2,
                    maxlength: 20,
                    messages: {
                        required: "Phone is a required field",
                        minlength: jQuery.validator.format("Please, at least {0} digits are necessary"),
                        maxlength: jQuery.validator.format("Please, maximum number of digits are {0}")
                    }
                });

                var options = {
                    errorPlacement: function (error, element) {
                        error.insertAfter(element);
                    }
                };

                if (!APL.undef(self.$helpForm.data("validator"))) {
                    $.extend(self.$helpForm.validate().settings, options);
                } else {
                    self.$helpForm.validate(options);
                }

                self.$cancel.click(close);
                self.$close.click(close);

                function send() {
                    if (self.$helpForm.valid()) {
                        var $loader = self.$loader;
                        $loader.show();

                        $.ajax({
                            type: "POST",
                            url: "/return/helper",
                            data: self.$helpForm.serialize(),
                            success: function () {
                                $loader.hide();
                                self.$helpForm.hide();
                                self.$success.show();
                            },
                            failure: function () {
                                $loader.hide();
                                self.$errors.show();
                                self.$submit.hide();
                            },
                            error: function () {
                                $loader.hide();
                                self.$errors.show();
                                self.$submit.hide();
                            }
                        });
                    }
                }

                self.$submit.click(send);
                self.$retry.click(send);
            }
        },
        newShipment: {
            $dialog: $("#add-tracking-modal"),
            $content: $("#add-tracking-content"),
            $loader: $("#add-tracking-loader"),
            $btnAdd: $(".add-tracking"),
            $errors: $("#add-tracking-errors"),
            $success: $("#add-tracking-success"),
            $errorText: $("#add-tracking-error-text"),
            $close: $("#add-tracking-close"),
            carrier: "#add-tracking-carrier",
            tracking: "#add-tracking-number",
            submit: "#submit-tracking",
            trackingInfo: "#tracking-info",
            form: "#new-tracking-form",
            qty: "input.dd-ship-qty",
            init: function () {
                var self = this;
                var $dialog = this.$dialog;
                var $content = this.$content;
                var $loader = this.$loader;
                if (APL.undef($dialog) || !$.fn.neysModal) {
                    return;
                }

                function closeModal() {
                    $dialog.data("neysModal").hide();
                    self.$content.html("");
                    self.$errors.hide();
                    self.$success.hide();
                };

                function saveTracking() {
                    if ($(self.form).valid()) {
                        self.$loader.show();
                        $.ajax({
                            type: "POST",
                            url: "/return/savetracking",
                            data: $(self.form).serialize(),
                            cache: false,
                            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                            dataType: "html",
                            success: successFunc,
                            failure: failureFunc,
                            error: errorFunc
                        });
                    }

                    function successFunc(data) {
                        self.$loader.hide();
                        self.$errors.hide();
                        self.$content.hide();
                        self.$success.show();
                        appendShipment(data);
                    }

                    function failureFunc() {
                        self.$loader.hide();
                        self.$errors.show();
                    }

                    function errorFunc(jqXhr, textStatus, errorThrown) {
                        self.$loader.hide();
                        if (errorThrown.indexOf("tracking") > 0 || errorThrown.indexOf("quantity")) {
                            self.$errorText.text(errorThrown);
                        }
                       
                        self.$errors.show();
                    }
                }

                function appendShipment(shipment) {
                    var lastEvent = $("div.content").children("div.item.last_item");
                    lastEvent.removeClass("last_item");
                    lastEvent.after(shipment);
                }

                function initValidation($form) {
                    var validatorOptions = {
                        settings: {
                            errorClass: "input-validation-error",
                            errorElement: "span"
                        },
                        errorPlacement: function (error, element) {
                            if (element.is("select")) {
                                element.next("span.ui-selectmenu-button").addClass(this.settings.errorClass);
                            }
                            else
                            {
                                element.addClass(this.settings.errorClass);
                            }
                        },
                        success: function (error, element) {
                            if ($(element).is("select")) {
                                $(element).next("span.ui-selectmenu-button").removeClass(this.settings.errorClass);
                            }
                            else
                            {
                                $(element).removeClass(this.settings.errorClass);
                            }
                            
                        }
                    };

                    if (!APL.undef($form.data("validator"))) {
                        $.extend($form.validate().settings, validatorOptions);
                    } else {
                        var validator = $form.validate(validatorOptions);
                        validator.settings.errorClass = validatorOptions.settings.errorClass;
                        validator.settings.errorElement = validatorOptions.settings.errorElement;
                    }
                }

                function open() {
                    var $loader = self.$loader;
                    $loader.show();

                    var rmaId = $(this).data("rma");

                    $.ajax({
                        type: "POST",
                        url: "/return/addtracking/" + rmaId,
                        dataType: "html",
                        cache: false, 
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                        success: successFunc,
                        failure: function () {
                            $loader.hide();
                            self.$errors.show();
                        },
                        error: function () {
                            $loader.hide();
                            self.$errors.show();
                        }
                    });

                    function successFunc(data) {
                        $loader.hide();
                        $content.html(data);
                        $content.show();
                        var $form = $(self.form);
                        initValidation($form);
                        var qtyInputs = $form.find(self.qty);
                        qtyInputs.neysQtyInput();
                        if (qtyInputs.filter(function () { return this.value > 0; }).length > 0) {
                            $(self.submit).prop("disabled", false);
                            $(self.trackingInfo).show();
                        }
                        qtyInputs.each(function () {
                            $(this).on("change", function () {
                                var trackingInfo = $(self.trackingInfo);
                                if ($(this).val() > 0) {
                                    trackingInfo.show();
                                    $(self.submit).prop("disabled", false);
                                }
                                else {
                                    trackingInfo.hide();
                                    $(self.submit).prop("disabled", true);
                                }

                                var itemId = $(this).attr("data-item");
                                var item = $("#" + itemId + "-available-qty");
                                item.text(parseInt($(this).attr("data-max")) - parseInt($(this).val()) + "x");
                            });
                        });

                        var $menus = $form.find("select");
                        $menus.selectmenu().data("uiSelectmenu").menuWrap.css("z-index", "10001");
                        $menus.children('option[disabled]').prop("selected", true);
                        $menus.selectmenu("refresh");
                        $(self.submit).on("click", saveTracking);

                        $menus.selectmenu({
                            change: function() {
                                $form.validate().element(this);
                            }
                        });
                    }

                    return false;
                };

                $dialog.neysModal({
                    trigger: self.$btnAdd,
                    onClosed: function () {
                        close();
                    }
                });

                self.$btnAdd.each(function () {
                    $(this).on("click", function () {
                        $dialog.show();
                        $loader.show();
                        return false;
                    });
                });

                self.$btnAdd.on("click", open);
                self.$close.on("click", closeModal);
            }
        }
    }

    $(document).ready(function () {
        APL.init();
        APL.returnrequest.init();
        APL.returnrequest.helper.init();
        APL.returnrequest.newShipment.init();
    });
})(jQuery);

