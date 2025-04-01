var APL = APL || {};
(function ($, document, window) {
    var mobileMenuInit = function () {
        if (!APL.mobileMenu.ready) {
            APL.mobileMenu.init();
        }
    };

    var resize = function () {
        var width = $(this).width();
        if (width <= 680) {
            mobileMenuInit();
        }
    };

    var updateSlider = function () {
        var width = $(this).width();
        (width > 768) ? APL.headerslider.start() : APL.headerslider.stop();
    };

    var windowResize = function () {
        //updateSlider();
        resize.call(this);
    };

    var orientationChange = function () {
        //updateSlider();
        resize.call(this);
    };

    APL.isDocLoaded = function () {
        return document.readyState === "complete";
    };

    APL.undef = function (obj) {
        return typeof obj === "undefined";
    };

    APL.nullOrUndef = function (obj) {
        return typeof obj === "undefined" || obj === null;
    };

    APL.nullOrEmpty = function (obj) {
        return obj === null || obj === "";
    };

    APL.isElementInViewpoint = function (el) {
        if (typeof jQuery === "function" && el instanceof jQuery) {
            el = el[0];
        }

        if (typeof el === "undefined" || el === null) {
            return false;
        }

        var rect = el.getBoundingClientRect();
        // if element is fully in viewpoint
        return (rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );
    };

    APL.isElementVisible = function (el) {
        if (typeof jQuery === "function" && el instanceof jQuery) {
            el = el[0];
        }

        if (typeof el === "undefined" || el === null) {
            return false;
        }

        var rect = el.getBoundingClientRect();
        // if element is fully visible
        return (rect.top >= 0 || rect.bottom >= 0);
    };

    APL.isElementTopVisible = function (el) {
        if (typeof jQuery === "function" && el instanceof jQuery) {
            el = el[0];
        }

        if (typeof el === "undefined" || el === null) {
            return false;
        }

        var rect = el.getBoundingClientRect();
        // if the top of element is visible
        return (rect.top >= 0 );
    };

    APL.isElementBottomVisible = function (el) {
        if (typeof jQuery === "function" && el instanceof jQuery) {
            el = el[0];
        }

        if (typeof el === "undefined" || el === null) {
            return false;
        }

        var rect = el.getBoundingClientRect();
        // if the bottom of element is visible
        return (rect.bottom >= 0);
    };

    APL.onVisibilityChange = function(el, callback) {
        var oldVisible;
        var oldTopVisible;
        var oldBottomVisible;
        var oldViewPoint;
        return function () {
            if (APL.nullOrUndef(el)) {
                return false;
            }

            var visible = APL.isElementVisible(el);
            var topVisible = APL.isElementTopVisible(el);
            var bottomVisible = APL.isElementBottomVisible(el);
            var inViewPoint = APL.isElementInViewpoint(el);

            if (visible !== oldVisible || oldTopVisible !== topVisible || oldBottomVisible !== bottomVisible || oldViewPoint !== inViewPoint) {
                oldVisible = visible;
                oldTopVisible = topVisible;
                oldBottomVisible = bottomVisible;
                oldViewPoint = inViewPoint;
                if (typeof callback == "function") {
                    callback(visible, topVisible, bottomVisible, inViewPoint);
                }
            }
        }
    };

    /*APL.initCheckboxes = function () {
        $("input[type='checkbox']").each(function () {
            var id = $(this).uniqueId().attr("id");
            if ($("label[for='" + id + "']").length > 0) {
                return;
            }

            var $label = $('<label for="' + id + '" class="checkbox"/>');
            $(this).wrap($label);
            $('<i class="mask_checkbox"/>').insertAfter($(this));
        });
    };*/

    APL.redirect = function (url) {
        window.location.href = url;
    };

    APL.openTab = function (url) {
        // Create link in memory
        var a = window.document.createElement("a");
        a.target = "_blank";
        a.href = url;

        // Dispatch fake click
        var e = window.document.createEvent("MouseEvents");
        e.initMouseEvent("click", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
        a.dispatchEvent(e);
    };

    APL.getCookie = function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === " ") {
                c = c.substring(1);
            }

            if (c.indexOf(name) !== -1) {
                return c.substring(name.length, c.length);
            }
        }

        return "";
    };

    APL.setCookie = function(name, value, days) {
        var date = new Date(), expires = "";

        if (days) {
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = ";expires=" + date.toUTCString();
        }

        document.cookie = name + "=" + value + expires + ";path=/";
    };

    APL.isMobile = function () {
        var mobile = false;
        (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) mobile = true })(navigator.userAgent || navigator.vendor || window.opera);
        return mobile;
    };

    APL.validateEmail = function validateEmail(email) {
        return /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
    };

    APL.openWindow = function (url, title, w, h, scroll) {
        var l = (screen.width - w) / 2;
        var t = (screen.height - h) / 2;

        var wnd = "resizable=0,height=" + h + ",width=" + w + ",top=" + t + ",left=" + l + "w";
        if (scroll) {
            wnd += ",scrollbars=1";
        }

        var newWindow = window.open(url, "_blank", wnd);
        if (window.focus && newWindow !== null) {
            newWindow.focus();
        }

        return newWindow;
    };

    APL.grayWrapper = {
        style: "master-wrapper-content-darkbg",
        $wrapper: $(".master-wrapper-page"),
        show: function () {
            this.$wrapper.addClass(self.style);
        },
        hide: function () {
            this.$wrapper.removeClass(self.style);
        }
    };

    APL.messageBar = {
        timeout: 0,
        $bar: $("#bar-notification"),
        $close: $("#bar-notification .close"),
        content: "#bar-notification > .content",
        show: function (message, type, timeout) {
            this.timeout = timeout;
            var self = this; 
            clearTimeout(this.timeout);

            this.$bar.removeClass("success").removeClass("error");
            $(this.content).remove();

            var html = "";
            if (typeof message == "string") {
                html = '<p class="content">' + message + "</p>";
            } else {
                for (var i = 0; i < message.length; i++) {
                    html = html + '<p class="content">' + message[i] + "</p>";
                }
            }

            this.$bar.append(html).addClass(type).fadeIn("slow").mouseenter(function () {
                clearTimeout(self.timeout);
            });

            this.$close.unbind("click").click(function () {
                self.$bar.fadeOut("slow");
            });

            if (this.timeout > 0) {
                this.timeout = setTimeout(function () { self.$bar.fadeOut("slow"); }, this.timeout);
            }
        }
    };

    APL.loader = {
        $loader: $("#loadScreen"),
        show: function () {
            this.$loader.show();
        },
        hide: function () {
            this.$loader.hide();
        },
        isVisible: function() {
            return this.$loader.is(":visible");
        }
    };

    APL.coupon = {
        cookie: "AP_WelcomeCouponSignUp",
        url: "/Customization/RootDiscountPopup",
        $popup:  $("#discountPopUp"),
        $form:   $("#discount-sign-up-notification"),
        $signup: $("#discount-sign-up-button"),
        $coupon: $("#discount-coupon-popup"),
        $close:  $("#discount-popup-close-button, #coupon-popup-close-button"),
        $email: $("#inputEmail"),
        $overlay: $("#discountPopUp").find(".modalBackground"),
        init: function () {
            var self = this;
            this.$close.on("click", { self: this }, this.hide);
            this.$overlay.on("click", { self: this }, this.hide);
            var href = window.location.href;
            //if (!APL.isMobile() && APL.nullOrEmpty(APL.getCookie(this.cookie)) && href.indexOf("/cart") === -1 && href.indexOf("checkout") === -1 && href.indexOf("/return") === -1) {
            //    setTimeout(function () { self.show(); }, 5000);
            //}
        },
        show: function () {
            var self = this;
            if (!APL.undef(APL.veh) && !APL.undef(APL.veh.popup) && APL.veh.popup.visible) {
                return;
            }

            this.$form.show();
            this.$popup.show();
            
            if (!APL.undef(window.dataLayer)) {
                window.dataLayer.push({
                    "event": "promotionView",
                    "ecommerce": {"promoView": {"promotions": [{"id": "20offshipping","name": "20% off Shipping"}]}}
                });
            }

            this.$signup.click(function () {
                var email = self.$email.val();
                if (!APL.validateEmail(email)) {
                    return;
                }

                $.ajax({
                    type: "POST", url: self.url, data: { inputEmail: email },
                    success: function (data) {
                        self.$form.hide();
                        if (data === "True") {
                            self.$coupon.show();
                            APL.messageBar.show("Shipping coupon code has been applied to your shopping cart", "success", 3500);
                        } else {
                            self.$popup.hide();
                        }

                        if (!APL.undef(window.dataLayer)) {
                            window.dataLayer.push({
                                "event": "promotionClick",
                                "ecommerce": { "promoClick": { "promotions": [{ "id": "20offshipping", "name": "20% off Shipping" }] } }
                            });
                        }
                    }
                });
            });
        },
        hide: function (e) {
            var self = e.data.self;
            self.$popup.hide();
            self.$form.hide();
            self.$coupon.hide();
            if (APL.nullOrEmpty(APL.getCookie(self.cookie))) {
                $.ajax({ type: "POST", url: self.url, dataType: "html"});
            }
        }
    };

    APL.searchBox = {
        $desktop: $("#search-box-desktop"),
        $mobile: $("#search-box-mobile"),
        $icon: $(".searchblockicon"),
        $block: $("#search-box-block"),
        $input: $("#small-searchterms"),
        $btn: $("#searchSubmitButton"),
        $form: $("#topSearchForm"),
        default: "Search store",
        init: function () {
            var self = this;
            $(window).on("resize", { self: this }, function () {
                if (self.$input.is(":focus")) {
                    return;
                }
                self.move();
            });
            $(window).trigger("resize");

            this.$mobile.hide();
            this.$block.show();
            
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

            this.$icon.click(function () {
                var target = this;
                self.$mobile.toggle();
                if (self.$mobile.is(":visible")) {
                    $(target).addClass("active");
                }
                else {
                    $(target).removeClass("active");
                }
            });

            self.$form.on("submit", function() {
                var text = self.$input.val();
                return !(APL.nullOrEmpty(text) || text === self.default);
            });

            this.$btn.click(function () {
                self.$form.submit();
            });
        },
        move: function () {
            if (window.innerWidth <= 768) {
                this.$block.appendTo(this.$mobile);
            } else {
                this.$block.prependTo(this.$desktop);
            }
        }
    };

    APL.lazy = function () {
        $("img.lazy").lazyload({ effect: "fadeIn" });
        $("img.mlazy").lazyload({ effect: "fadeIn", event: "showed" });
    };

    APL.mobileMenu = {
        trigger: "#apLeftMenu",
        panel: "#apLeftPanel",
        $btn: null,
        $close: $("#apLeftMenuClose"),
        $content: $("#mob-menu-button"),
        ready: false,
        init: function () {
            var self = this;
            this.ready = true;
            this.$btn = $(this.trigger);
            this.$close.click(function (e) {
                e.preventDefault();
                self.$btn.click();
            });

            this.$content.find("ul li ul").hide();
            this.$content.find("ul li span").click(function (e) {
                e.preventDefault();
                var submenu = $(this).next();
                submenu.toggle();
                submenu.find("img.mlazy").trigger("showed");
                if ($(this).next().is(":visible")) {
                    $(this).removeClass("expand");
                }
                else {
                    $(this).addClass("expand");
                }
            });

            $.jPanelMenu({
                menu: this.panel,
                trigger: this.trigger,
                keyboardShortcuts: false,
                clone: false,
                openPosition: "300px",
                afterOpen: function () {
                    APL.grayWrapper.show();
                    self.$menu.css("position", "fixed").css("overflow", "hidden");
                },
                beforeClose: function () {
                    APL.grayWrapper.hide();
                },
                afterClose: function () {
                    self.fix();
                    self.$menu.css("position", "relative").css("overflow", "auto");
                }
            }).on();

            this.$menu = $(".jPanelMenu-panel");
            this.fix();
        },
        fix: function() {
            // fix of jPanelMenu style that affects other page elements when menu closed
            this.$menu.css("transform", "none");
        }
    };

    APL.flyoutCart = function () {
        $("img.cart-lazy").lazyload({ effect: "fadeIn", event: "showed" });
        $(".header").on("mouseenter", "#topcartlink, #flyout-cart", function () {
            $("#flyout-cart").addClass("active");
            $("img.cart-lazy").trigger("showed");
        });

        $(".header").on("mouseleave", "#topcartlink, #flyout-cart", function () {
            $("#flyout-cart").removeClass("active");
        });

        $("#btn-mini-cart").on("click", function() {
            window.location.href = "/cart";
        });

        /*$("#btn-mini-checkout").on("click", function () {
            window.location.href = "/checkout";
        });*/
    };

    APL.mobileHeaderLinks = function () {
        var $menu = $(".mob-header-links-menu");
        $("#mob-header-links-button").on("click", function (e) {
            e.stopImmediatePropagation();
            if ($menu.hasClass("show")) {
                $menu.hide();
                $menu.removeClass("show");
            } else {
                $menu.addClass("show");
                $menu.show();
            }
        });
    };

    APL.newsletter = {
        url: "/subscribenewsletter",
        $header: $(".footermail"),
        $block: $("#newsletter-subscribe-block"),
        $email: $("#newsletter-email"),
        $btn: $("#newsletter-subscribe-button"),
        $progress: $("#subscribe-loading-progress"),
        $result: $("#newsletter-result-block"),
        init: function () {
            this.$btn.on("click", { self: this }, this.subscribe);
        },
        subscribe: function (e) {
            var self = e.data.self;
            var email = self.$email.val();
            if (!APL.validateEmail(email)) {
                return false;
            }

            self.$progress.show();
            $.ajax({ type: "POST", url: self.url, data: { "email": email }, success: function (data) {
                    self.$progress.hide();
                    self.$result.html(data.Result);
                    if (data.Success) {
                        self.$header.hide();
                        self.$block.hide();
                        self.$result.show();
                    }
                    else {
                        self.$result.fadeIn("slow").delay(2000).fadeOut("slow");
                    }
                },
                error: function () {
                    self.$progress.hide();
                }
            });

            return false;
        }
    };

    function customInitFreshChat() {
        var customerGuid = dataLayer[0].customerGuid;
        $(window.fcWidget).on("widget:loaded", function (resp) {
            window.fcWidget.setExternalId(customerGuid);
            setVehicleFreshchat();
        });
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
                "Year": parts[0],
                "Make": parts[1],
                "Model": parts[2]
            };

            if (typeof parts[3] !== 'undefined') {
                props["Submodel"] = parts[3];
            }

            window.fcWidget.user.setProperties(props);
        }
    }

    //APL.headerslider = $("#headerbanner").neysSlider({
    //    maxOpacity: 1,
    //    minOpacity: 0.04,
    //    duration: 1200,
    //    imageUrls: window.headerBanners,
    //    interval: 3000
    //});

    //$("#headerbanner").click(function () {
    //    window.location.replace("/blackfriday-cybermonday");
    //});

    //updateSlider();

    APL.init = function () {
        $(window).on("resize", windowResize);
        $(window).on("orientationchange", orientationChange);

        //if (APL.currentStore.vehicleSupported) {
        //    customInitFreshChat();
        //}
        
        //this.initCheckboxes();
        this.searchBox.init();
        this.lazy();
        this.flyoutCart();
        this.mobileHeaderLinks();
        this.coupon.init();
        this.newsletter.init();
        
        if (document.createEventObject) {
            document.fireEvent("APL.ready");
        } else {
            var evt = document.createEvent("HTMLEvents");
            evt.initEvent("APL.ready", false, true);
            document.dispatchEvent(evt);
        }
    };


    $(document).ready(function () {
        if (APL.currentStore.vehicleSupported) {
            setTimeout(customInitFreshChat, 7000);
        }

        setTimeout(function () {
            $.ajax({
                type: "GET",
                url: "/Customization/BrandCaching",
                cache: true,
                success: function (data) {
                }
            });
        }, 5000);

    });
})(jQuery, document, window);
