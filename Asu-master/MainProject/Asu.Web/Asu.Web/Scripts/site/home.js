(function ($, document, window) {
    APL.home = APL.home || {};

    var updateSlider = function () {
        var width = $(this).width();
        (width > 768) ? APL.home.slider.start() : APL.home.slider.stop();
    };

    var windowResize = function () {
        updateSlider();
    };

    var orientationChange = function () {
        updateSlider();
    };

    /*
        Replaced by affirm
     */
    //var initPayPalCreditBanner = function () {
    //    var payPalCreditBanner = '<script type="text/javascript" data-pp-pubid="27a6bd7a61" data-pp-placementtype="234x60">(function (d, t) {"use strict";var s = d.getElementsByTagName(t)[0], n = d.createElement(t);n.src = "//paypal.adtag.where.com/merchant.js";s.parentNode.insertBefore(n, s);}(document, "script"));</script>';
    //    $(".homepagegarantebanner2").html(payPalCreditBanner);
    //};

    APL.home.searchBox = {
        default: "Search by Part #",
        $input: $("#tKeyword"),
        $btn: $("#tSearchBtn"),
        $form: $("#tSearchForm"),
        init: function () {
            var self = this;
            this.$input.val(this.default);
            this.$input.focus(function () {
                if (this.value === self.default) {
                    this.value = "";
                }
            });

            this.$input.blur(function () {
                if (APL.nullOrEmpty(this.value)) {
                    this.value = self.default;
                }
            });

            this.$form.on("submit", function () {
                var text = self.$input.val();
                return !(APL.nullOrEmpty(text) || text === self.default);
            });

            this.$btn.click(function () {
                if (self.$input.val() === "") {
                    return;
                }

                self.$form.submit();
            });
        }
    };

    APL.home.searchBrand = {
        default: "Type brand name...",
        url: "autocomplete/brand?prefix",
        $input: $("#tBrandName"),
        init: function () {
            var self = this;
            this.$input.val(this.default);
            this.$input.focus(function () {
                if (this.value === self.default) {
                    this.value = "";
                }
            });

            this.$input.blur(function () {
                if (APL.nullOrEmpty(this.value)) {
                    this.value = self.default;
                }
            });

            this.$input.autocomplete({
                minLength: 2,
                source: function (request, response) {
                    if (request.term === self.default) {
                        return;
                    }

                    $.ajax({
                        url: self.url + self.$input.val(),
                        dataType: "json",
                        data: { prefix: request.term },
                        success: function (data) { response(data); }
                    });
                },
                open: function (e, ui) {
                    if (APL.isMobile()) {
                        $('.ui-autocomplete').off('menufocus hover mouseover mouseenter');
                    }
                },
                select: function (e, ui) {
                    e.preventDefault();
                    self.$input.val(ui.item.label);
                    window.location.href = ui.item.value;
                },
                focus: function (e, ui) {
                    e.preventDefault();
                    self.$input.val(ui.item.label);
                }
            });
        }
    };

    function setVehicleFreshchat() {

        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        var cookieName = "WC.Vehicle.Name.Cookie";
        var cookie = getCookie(cookieName);
        if (cookie !== "") {
            var parts = cookie.split("|");

            var props = {
                meta: {
                    "Year": parts[0],
                    "Make": parts[1],
                    "Model": parts[2]
                }
            };

            if (typeof parts[3] !== 'undefined') {
                props.meta["Submodel"] = parts[3];
            }

            window.fcWidget.user.update(props).then(function (response) {
                console.log(response.data);
            });
        }
    }

    APL.home.init = function () {
        $(window).on("resize", windowResize);

        $(window).on("orientationchange", orientationChange);

        APL.home.filter = "#vehiclesFilter";
        APL.home.loader = ".box-loader";

        if (APL.currentStore.vehicleSupported) {
            APL.veh.filter = $(APL.home.filter).neysVehicle({
                clear: "#vClear",
                ready: function() {
                    $(APL.home.filter).show();
                },
                changed: function () {
                    setVehicleFreshchat();
                },
                loading: function() {
                    $(APL.home.loader).show();
                },
                done: function() {
                    $(APL.home.loader).hide();
                }
            });

            $("#VehicleAccessories").tabs();
        } else {
            $(APL.home.loader).hide();
        }
        
        $("#homeTabs").tabs({
            create: function (e, ui) {
                $(this).show();
            }
        });

        $("#homeTabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
        $("#homeTabs").removeClass("ui-corner-top").addClass("ui-corner-left");

        APL.home.searchBox.init();
        APL.home.searchBrand.init();
        APL.home.slider = $("#homepagelagebanner").neysSlider({
            maxOpacity: 1,
            minOpacity: 0.04,
            duration: 1200,
            imageUrls: window.homeLogos,
            interval: 5000
        });

        $("#homepagelagebanner").click(function () {
            var styles = $("#homepagelagebanner").attr("style");
            if (styles.indexOf("westinrebatecoupon.jpg") >= 0) {
                var win = window.open("/Content/files/rebate.pdf", '_blank');
                if (win) {
                    //Browser has allowed it to be opened
                    win.focus();
                } else {
                    //Browser has blocked it
                    alert('Please allow popups for this website');
                }
                //window.location.replace("/Content/files/rebate.pdf");
            }
            else
            {
                window.location.replace("/workathome");
            }
            
        });
        
        updateSlider();

        //if (APL.isDocLoaded()) {
        //    initPayPalCreditBanner();
        //}
        //else {
        //    $(window).on("load", initPayPalCreditBanner);
        //}
    };

    //function initTireConfigurator() {
    //    APL.tireConfiguratorObject = $("#tTireConfigurator").tireConfigurator({
    //        ready: function () {
    //            //displayAjaxLoading(true);
    //        },
    //        changed: function () {
    //        },
    //        loading: function () {
    //            //displayAjaxLoading(true);
    //        },
    //        done: function () {
    //            //displayAjaxLoading();
    //        }
    //    });
    //}

    APL.home.tireConfigurator = {
        default: "Search by Part #",
        $sectionDd: $("#section"),
        $aspectDd: $("#aspect"),
        $rimDd: $("#rim"),
        $form: $("#tire-configurator-form"),
        $btn: $("#tFindTireBtn"),
        init: function () {
            var that = this;
            var options = {};
            //this.$form.on("submit", function () {
            //    var text = that.$input.val();
            //    return !(APL.nullOrEmpty(text) || text === that.default);
            //});

            that.$aspectDd.selectmenu({
                change: function () {
                    getRimValues(that.$sectionDd, that.$aspectDd, that.$rimDd);
                }
            });

            that.$rimDd.selectmenu(options);

            this.$sectionDd.selectmenu({
                create: function (event, ui) {
                    var context = this;
                    getSectionValues(context, that.$aspectDd, that.$rimDd);

                },
                change: function (event, ui) {
                    that.$rimDd.val(that.$rimDd.find("option:first-child").val());
                    that.$rimDd.selectmenu("refresh");
                    getAspectValues(that.$sectionDd, that.$aspectDd, that.$rimDd);
                }
            });

            this.$btn.click(function () {
                var section = that.$sectionDd.val();
                var aspect = that.$aspectDd.val();
                var rim = that.$rimDd.val();
                if (isNullOrEmpty(section) && isNullOrEmpty(aspect) && isNullOrEmpty(rim)) {
                    that.$sectionDd.selectmenu("open");
                    return;
                }

                if (!isNullOrEmpty(section) && isNullOrEmpty(aspect) && isNullOrEmpty(rim)) {
                    that.$aspectDd.selectmenu("open");
                    return;
                }

                if (!isNullOrEmpty(section) && !isNullOrEmpty(aspect) && isNullOrEmpty(rim)) {
                    that.$rimDd.selectmenu("open");
                    return;
                }

                that.$form.submit();
            });
        }
    };

    function isNullOrEmpty(value) {
        return value === "" || value === null;
    }

    function getSectionValues(context, aspectSelectMenu, rimSelectMenu) {
        
        $.ajax({
            url: "vehicle/getspecification",
            type: "POST",
            data: { target: "section" },
            success: function (result) {
                result.unshift({ Id: "0", Name: "Select Width" });
                var targetMenuId = "#" + $(context).attr("id");
                $.each(result, function (index, value) {
                    $(context).append($("<option/>", {
                        value: value.Id,
                        text: value.Name,
                        selected: index === 0
                    }));
                });

                aspectSelectMenu.selectmenu("enable");
                aspectSelectMenu.selectmenu("open");

                $(targetMenuId + " option:first-child").attr("disabled", true);
                aspectSelectMenu.selectmenu("disable");
                rimSelectMenu.selectmenu("disable");
                $(context).selectmenu("refresh");
            }
        });
    }

    function getAspectValues(sectionSelectMenu, aspectSelectMenu, rimSelectMenu) {
        rimSelectMenu.selectmenu("disable");
        var sectionDefaultValue = $(sectionSelectMenu).val();

        $.ajax({
            url: "vehicle/getspecification",
            type: "POST",
            data: { sectionValue: sectionDefaultValue, target: "aspect"  },
            success: function (result) {
                var targetMenuId = "#" + $(aspectSelectMenu).attr("id");
                $(targetMenuId + " option").remove();
                result.unshift({ Id: "0", Name: "Select Ratio" });

                $.each(result, function (index, value) {
                    $(aspectSelectMenu).append($("<option/>", {
                        value: value.Id,
                        text: value.Name
                    }));
                });

                aspectSelectMenu.selectmenu("enable");
                $(targetMenuId + " option:first-child").attr("disabled", true); 
                aspectSelectMenu.selectmenu("refresh");
                aspectSelectMenu.selectmenu("open");
            }
        });
    }

    function getRimValues(sectionSelectMenu, aspectSelectMenu, rimSelectMenu) {
        rimSelectMenu.selectmenu("disable");
        var sectionDefaultValue = $(sectionSelectMenu).val();
        var aspectDefaultValue = $(aspectSelectMenu).val();

        $.ajax({
            url: "vehicle/getspecification",
            type: "POST",
            data: { sectionValue: sectionDefaultValue, aspectValue: aspectDefaultValue, target: "rim"  },
            success: function (result) {
                var targetMenuId = "#" + $(rimSelectMenu).attr("id");
                $(targetMenuId + " option").remove();
                result.unshift({ Id: "0", Name: "Select Diameter" });

                $.each(result, function (index, value) {
                    $(rimSelectMenu).append($("<option/>", {
                        value: value.Id,
                        text: value.Name
                    }));
                });

                rimSelectMenu.selectmenu("enable");
                $(targetMenuId + " option:first-child").attr("disabled", true); 
                rimSelectMenu.selectmenu("refresh");
                rimSelectMenu.selectmenu("open");
            }
        });
    }

    $(document).ready(function () {
        APL.init();
        APL.home.init();
        APL.home.tireConfigurator.init();
        //$("#tTireConfigurator").on("click", function (e) {
        //    APL.home.tireConfigurator.init();
        //});
    });

})(jQuery, document, window);