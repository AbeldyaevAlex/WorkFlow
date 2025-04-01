/* "~/Scripts/site/vehicle.filter.js",
            "~/Scripts/neysvehicle/js/neysvehicle.js",
            "~/Scripts/site/product-search.js",
            //"~/Scripts/site/public.search.filter.js",
            //"~/Scripts/site/public.vehicles.js",
            //"~/Scripts/site/public.wc.checkout.js",
            //"~/Scripts/site/public.common.js", */


/* INIT MOBILE FILTER MENU */
var filterSectionCollapsed = true,
    filterCategoryCollapsed = true,
    filterManufacturerCollapsed = true,
    filterPriceCollapsed = true,
    filterSortCollapsed = true;

$(document).ready(function () {
    var filtersSelector = "html, body, .master-wrapper-main .side-2, #searchFilter";
    var mainContentOverlay = $(".main-content-overlay");
    var filterOpenedClassName = "filter-opened";

    $(".aside-close-btn").on("click", function () {
        $(filtersSelector).removeClass(filterOpenedClassName);
        mainContentOverlay.hide();
    });

    $(".mobile-filter-open-btn").on("click", function () {
        $(filtersSelector).addClass(filterOpenedClassName);
        mainContentOverlay.show();
        $("html, body").scrollTop(0);
        $("#searchFilter").show();

        $("div.filterName + div, div.filterName + ul").each(function () {
            $(this).addClass("collapsed");
        });

        $("div.filterName").each(function () {
            $(this).addClass("plus");
        });
    });

    if (window.mobilecheck()) {
        $(".brand-search-infilter-block").remove();
    }

    //$(document).on("change", "input[name='sorter']:radio", function () {
    //    setSortOption($("input[name='sorter']:checked").val());
    //});

    $(document).on("click", "div.filterName", function (e) {
        var that = $(this);
        e.stopImmediatePropagation(); // THM live issue fix
        var block = $(this).next("ul, div");
        if (window.mobilecheck()) {
            $(".brand-search-infilter-block").remove();
            $("div.filterName + div, div.filterName + ul").each(function () {
                if (this !== block[0]) {
                    $(this).addClass("collapsed");
                    that.removeClass("minus");
                    $(this).prev().addClass("plus");
                }
            });
        }

        if (block.hasClass("collapsed")) {
            block.removeClass("collapsed");
            that.removeClass("plus");
            that.addClass("minus");
        } else {
            block.addClass("collapsed");
            that.removeClass("minus");
            that.addClass("plus");
        }

        var filter = $("#sub-category-list");
        if (filter.length > 0) {
            filterSectionCollapsed = filter.hasClass("collapsed");
        }

        filter = $("#filterCategory");
        if (filter.length > 0) {
            filterCategoryCollapsed = filter.hasClass("collapsed");
        }

        filter = $("#filterManufacturer");
        if (filter.length > 0) {
            filterManufacturerCollapsed = filter.hasClass("collapsed");
        }

        filter = $("#filterPriceInput");
        if (filter.length > 0) {
            filterPriceCollapsed = filter.hasClass("collapsed");
        }

        filter = $("#sort-by");
        if (filter.length > 0) {
            filterSortCollapsed = filter.hasClass("collapsed");
        }
    });
});
/* END MOBILE FILTER MENU */




//public.common.js

//+
function displayPopupNotification(message, messagetype, modal) {
    var container;
    if (messagetype === 'success') {
        container = $('#dialog-notifications-success');
    }
    else if (messagetype === 'error') {
        container = $('#dialog-notifications-error');
    }
    else {
        container = $('#dialog-notifications-success');
    }

    //we do not encode displayed message
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p>' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p>' + message[i] + '</p>';
        }
    }
    container.html(htmlcode);
    var isModal = (modal ? true : false);
    container.dialog({ modal: isModal });
};

//+
var barNotificationTimeout;
function displayBarNotification(message, messagetype, timeout) {
    clearTimeout(barNotificationTimeout);

    //types: success, error
    var cssclass = 'success';
    if (messagetype == 'success') {
        cssclass = 'success';
    }
    else if (messagetype == 'error') {
        cssclass = 'error';
    }
    //remove previous CSS classes and notifications
    $('#bar-notification')
        .removeClass('success')
        .removeClass('error');
    $('#bar-notification .content').remove();

    //we do not encode displayed message

    //add new notifications
    var htmlcode = '';
    if ((typeof message) == 'string') {
        htmlcode = '<p class="content">' + message + '</p>';
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p class="content">' + message[i] + '</p>';
        }
    }
    $('#bar-notification').append(htmlcode)
        .addClass(cssclass)
        .fadeIn('slow')
        .mouseenter(function () {
            clearTimeout(barNotificationTimeout);
        });

    $('#bar-notification .close').unbind('click').click(function () {
        $('#bar-notification').fadeOut('slow');
    });

    //timeout (if set)
    if (timeout > 0) {
        barNotificationTimeout = setTimeout(function () {
            $('#bar-notification').fadeOut('slow');
        }, timeout);
    }
};

function htmlEncode(value) {
    return $('<div/>').text(value).html();
};

function htmlDecode(value) {
    return $('<div/>').html(value).text();
};

/* WC. START Custom Scripts */
//Custom checkbox click event handler
var checkBox =
{
    'click': function (e, label) {
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
             e.cancelBubble = true;
        }

        var checkBox = $(label).children('input[type="checkbox"]');
        if (checkBox.length > 0) {
            if ($(checkBox).prop("checked")) {
                $(label).addClass("checked");
            } else {
                $(label).removeClass("checked");
            }
        }
    }
};

var radioButton =
{
    'click': function (e, label) {
        if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
        var radioButton = $(label).children('input[type="radio"]');
        if (radioButton.length > 0) {
            var name = radioButton.attr('name');
            if (name != '') {
                var groupRadioButtons = $('input[type="radio"][name="' + name + '"]');
                groupRadioButtons.each(function () {
                    $(this).hide();
                    $(this).parent('label.radio').removeClass('checked');
                });
            }
            if ($(radioButton).prop('checked')) {
                $(label).addClass('checked');
            } else {
                $(label).removeClass('checked');
            }
        }
    }
};

function initRadioButtons() {
    $('input[type="radio"]:not([class="no-style"])').addClass("radio");
    $('input.radio').filter(function () { return $(this).parent().is(":not(label.radio)"); }).wrap("<label class='radio' onclick='radioButton.click(event, this)'></label>");
    $("label.radio").each(function () {
        var radioButton = $(this).children('input[type="radio"]');
        $(radioButton).hide();
        if ($(radioButton).prop('checked') || $(radioButton).attr('checked')) { $(this).addClass('checked'); }
        else { $(this).removeClass('checked'); }
    });
};

$(document).ready(function () {
    overrideControls();
    initDiscountCoupon();
    //$("img.lazy").lazyload({ effect: "fadeIn" });
    //$("img.mlazy").lazyload({ effect: "fadeIn", event: "showed" });
});

function overrideControls() {
    //initCheckBoxes();
    initRadioButtons();
    if (isMobile) {
        initMobileFilterMenu();
        $(".aside-filters-block").show();
    }
};

//+
function CloseCouponPopup() {
    $('#discount-coupon-popup').hide();
    $('#discountPopUp').hide();
};

//+
function initDiscountCoupon() {
    // Discount Coupon logic
    $('#coupon-popup-close-button').click(function () { CloseCouponPopup(); });

    //if (window.mobilecheck() != true && window.getCookie('AP_WelcomeCouponSignUp') === '' && location.protocol === 'http:') {
    //    setTimeout(function () { showPopUp(); }, 5000);
    //}
};
//+
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
};

//+
function CloseSignUpPopup() {
    $('#discount-sign-up-notification').hide();
    $('#discountPopUp').hide();
    $.ajax({
        cache: true,
        type: 'POST',
        url: '/Customization/RootDiscountPopup',
        data: '',
        dataType: 'html',
        success: function () {
            $('#discount-sign-up-notification').hide();
            $('#discountPopUp').hide();
        }
    });
};
//+
function showPopUp() {
    if ($("#vehiclesFilterPopup").is(":visible")) { return; }
    $('#discount-sign-up-notification').show();
    $('#discountPopUp').show();
    try {
        if (typeof window.dataLayer !== "undefined") {
            window.dataLayer.push({
                'event': 'promotionView',
                'ecommerce': {
                    'promoView': {
                        'promotions': [
                            {
                                'id': '20offshipping',
                                'name': '20% off Shipping'
                            }]
                    }
                }
            });
        }
    } catch (e) {
    }

    $('#discount-sign-up-button').click(function () {
        var email = $('#inputEmail').val();
        if (!validateEmail(email))
            return;
        $.ajax({
            cache: true,
            type: 'POST',
            url: '/Customization/RootDiscountPopup',
            data: { inputEmail: email },
            success: function (data) {
                try {
                    if (typeof window.dataLayer !== "undefined") {
                        window.dataLayer.push({
                            'event': 'promotionClick',
                            'ecommerce': {
                                'promoClick': {
                                    'promotions': [
                                        {
                                            'id': '20offshipping',
                                            'name': '20% off Shipping',
                                        }
                                    ]
                                }
                            }
                        });
                    }
                } catch (ex) {
                }

                $('#discount-sign-up-notification').hide();
                $('#discountPopUp').hide();
                if (data == 'True') {
                    $('#discount-coupon-popup').show();
                    $('#discountPopUp').show();
                    displayBarNotification('Shipping coupon code has been applied to your shopping cart', 'success', 3500);
                }
            }
        });
    });

    $('#discount-popup-close-button').click(function () { CloseSignUpPopup(); });
};
//+

function PopupCenterScreen(url, title, w, h) {
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'resizable=no,location=1,menubar=no,toolbar=no,scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

    if (window.focus) {
        newWindow.focus();
    }

    return newWindow;
};

/* End Amazon */

$(document).ready(function () {

    if ($('.infinite-scroll').length > 0) {
        $('.infinite-scroll').jscroll({
            nextSelector: '#nextPageBlock',
            callback: onDownloadNextPage
        });
        onDownloadNewPage();
    }

    var currentPosY = $(window).scrollTop();  //your current y position on the page
    setTimeout(function () { $(window).scrollTop(currentPosY + 1); }, 1000);

    //start TopMenu.cshtml
    //+
    $("#small-searchterms").focus(function () {
        if (this.value === 'Search store') {
            this.value = '';
        }
    });

    //+
    $("#small-searchterms").blur(function () {
        if (this.value === '') {
            this.value = 'Search store';
        }
    });

    //+
    $("#searchSubmitButton").click(function () {
        $("#topSearchForm").submit();
    });
    //end init SearchBox.cshtml

    //start TopMenu.cshtml
    $('#mob-menu-button-a').toggle(function () {
        $('.mob-top-menu').addClass('show');
        $('.icon').addClass('transform1');
    }, function () {
        $('.mob-top-menu').removeClass('show');
        $('.icon').removeClass('transform1');
    });

    $(function ($) {
        $('.mob-top-menu .expand').click(function () {
            var parent = $(this).parent();
            if (parent.hasClass('active')) {
                $(".sublist:first", parent).hide(300);
                parent.removeClass('active');
            } else {
                $(".sublist:first", parent).show(300);
                parent.addClass('active');
            }
        });
    });

    //+
    //if (window.mobilecheck() === true) {
    //    var jPm = $.jPanelMenu({
    //        excludedPanelContent: "#857eb8360ae64ac78b189eae4d626969-b",
    //        menu: '#apLeftPanel',
    //        trigger: '#apLeftMenu',
    //        keyboardShortcuts: false,
    //        clone: false,
    //        openPosition: '300px',
    //        afterOn: function () {
    //            window.dataLayer.push({
    //                'event': 'jPanelMenuReady'
    //            });
    //        },
    //        afterOpen: function () { $(".master-wrapper-page").addClass("master-wrapper-content-darkbg"); },
    //        beforeClose: function () { $(".master-wrapper-page").removeClass("master-wrapper-content-darkbg"); }
    //    });
    //    jPm.on();
    //} else {
    //    window.dataLayer.push({
    //        'event': 'jPanelMenuReady'
    //    });
    //}
    //end init TopMenu.cshtml

    //start SearchPage.cshtml
    $('a', $('#homepagelagebanner')).click(function () {
        if ($('#homepagebannertitle1 > .icon').hasClass('transform2')) {
            $('.vehicles-filter').removeClass('show');
            $('#homepagebannertitle1 > .icon').removeClass('transform2');
        }
        else {
            $('.vehicles-filter').addClass('show');
            $('#homepagebannertitle1 > .icon').addClass('transform2');
        }
        return false;
    });

    initShowHideCatAndBrands();
    //end SearchPage.cshtml

    //+
    //start _FilterProductsList.cshtml
    $('div.rateit, span.rateit').rateit();
    //end _FilterProductsList.cshtml

    //start HeaderLinks.cshtml
    // +
    //$('#mob-header-links-button').toggle(function () { $('.mob-header-links-menu').addClass('show'); }, function () { $('.mob-header-links-menu').removeClass('show'); });

    // -
    $(".alertMessageNotification").each(function () {
        displayPopupNotification($(this).data('message'), $(this).data('type'), false);
    });

    //+
    moveMobileBox();
    window.onresize = moveMobileBox;
    $("#search-box-mobile").hide();
    $("#search-box-block").show();
    $(".searchblockicon").click(function () {
        $("#search-box-mobile").toggle();
        if ($('#search-box-mobile').is(':visible')) {
            $('.searchblockicon').addClass('active');
        }
        else {
            $('.searchblockicon').removeClass('active');
        }
    });

    //+
    $('.header').on('mouseenter', '#topcartlink', function () {
        $('#flyout-cart').addClass('active');
    });
    $('.header').on('mouseleave', '#topcartlink', function () {
        $('#flyout-cart').removeClass('active');
    });
    $('.header').on('mouseenter', '#flyout-cart', function () {
        $('#flyout-cart').addClass('active');
    });
    $('.header').on('mouseleave', '#flyout-cart', function () {
        $('#flyout-cart').removeClass('active');
    });
    //end HeaderLinks.cshtml

    //start Index.cshtml
    $("#VehicleAccessories").tabs({
        ajaxOptions: {
            error: function (xhr, status, index, anchor) {
                $(anchor.hash).html("I tried to load this, but couldn't. Try one of the other links?");
            },
        },
        beforeLoad: function (event, ui) {
            ui.panel.html('<img src="/Themes/Autoplicity/Content/images/loading-small.gif" />');
        }
    });
    //end Index.cshtml

    //start NewsletterBox.schtml
    // +
    //$('#newsletter-subscribe-button').click(function () {
    //    var email = $("#newsletter-email").val();
    //    var subscribeProgress = $("#subscribe-loading-progress");
    //    subscribeProgress.show();
    //    $.ajax({
    //        cache: false,
    //        type: "POST",
    //        url: "/subscribenewsletter",
    //        data: { "email": email },
    //        success: function (data) {
    //            subscribeProgress.hide();
    //            $("#newsletter-result-block").html(data.Result);
    //            if (data.Success) {
    //                $('#newsletter-subscribe-block').hide();
    //                $('#newsletter-result-block').show();
    //            }
    //            else {
    //                $('#newsletter-result-block').fadeIn("slow").delay(2000).fadeOut("slow");
    //            }
    //        },
    //        error: function (xhr, ajaxOptions, thrownError) {
    //            alert('Failed to subscribe.');
    //            subscribeProgress.hide();
    //        }
    //    });
    //    return false;
    //});
    //end NewsletterBox.schtml

    //start _Root.cshtml
    if (window.location.pathname.indexOf("/checkout/") != -1) {
        $('.header-menu').hide();
        $('.column').hide();
        $('.homepagegarantebannerfooter').hide();
        $('.mob-footer-menu-wrapper .phone').hide();
        $('.mob-footer-menu-wrapper ul').hide();
        $('.mob-searchboxblock').hide();
        $('.mob-rightheader').hide();
        $('.checkout_ratings_footer').show();
        $('.searchboxblock').hide();
        var headerLinks = $('.header-links').children('li');
        for (var i = 0; i < headerLinks.length - 1; i = i + 1) {
            $(headerLinks[i]).hide();
        }
        $('.header-links').addClass("header-checkout");
    }
    AjaxCart.init(false, '#cart-qty', '.header-links .wishlist-qty', '#flyout-cart');
    //end _Root.cshtml

    //start _Root.Head.cshtml
    //$("#apLeftMenuClose").click(function () {
    //    $("#apLeftMenu").click();
    //});
    //$("#mob-menu-button ul li ul").hide();
    //$("#mob-menu-button ul li span").click(function () {
    //    var submenu = $(this).next();
    //    submenu.toggle();
    //    submenu.find("img.mlazy").trigger("showed");
    //    if ($(this).next().is(':visible')) {
    //        $(this).removeClass('expand');
    //    }
    //    else {
    //        $(this).addClass('expand');
    //    }
    //});
    //end _Root.Head.cshtml


    if ($("li[class='arrow arrow2'] > a.active-step").length > 0) {
        $('a.inactive-step').addClass('step2');
    }

    if ($("li[class='arrow arrow3'] > a.active-step").length > 0) {
        $('a.inactive-step').addClass('step3');
    }


    //start Brands.cshtml
    if ($("#sbb_filter").length > 0) {
        var sPageURL = window.location.search.substring(1);
        if (sPageURL.indexOf('letter=') != -1) {
            $('.sbb_filter').addClass('hide');
        }
    }
    //end Brands.cshtml

    //start _SingleSymbolBrand.cshtml
    if ($("#manufacturer-list-page").length > 0) {
        if (window.mobilecheck()) {
            var arrBrandItems = $('.manufacturer-grid').children('.item-box').children('.manufacturer-item');
            for (var i = 0; i < arrBrandItems.length; i = i + 1) {
                if ($(arrBrandItems[i]).children('.picture').children('a').children('img')[0].src.indexOf(window.storeDefaultImageName + "_187.gif") != -1) {
                    $($(arrBrandItems[i]).children('.picture')[0]).hide();
                    $($(arrBrandItems[i]).children('h2')[0]).addClass('show');
                }
            }
        }
    }
    //end _SingleSymbolBrand.cshtml

    // hide phone if product price < $250
    if ($('.product-price').length === 1 && $("span[itemprop='price']").length === 1) {
        if ($("span[itemprop='price']").text().replace('$', '').trim() < 250) {
            $('span.headerphone').parent().hide();
            $("div[class='column follow-us'] > h3").hide();
        }
    }
});

function AP_IsRemove() {
    var listCartRows = $('.cart').children('tbody').children('tr');
    for (var i = 0; i < listCartRows.length; i = i + 1) {
        var cartRemoveCheckBox = $(listCartRows[i]).children('.remove-from-cart').children('label').children('input');
        if (cartRemoveCheckBox != null & cartRemoveCheckBox.length > 0) {
            if (cartRemoveCheckBox[0].checked) {
                return true;
            }
        }
    }
    return false;
};

function onPreventNonLatinSymbols(e) {
    var charcode = e.which;
    if (charcode > 127) {
        e.preventDefault();
        return;
    }
};

function checkAddressFormForErrors() {
    $("#overall-error-message").hide();
    setTimeout(function () {
        if ($("span[class$='-error']").length > 0) {
            $("#overall-error-message").show();
        }
    }, 500);
};

function moveMobileBox() {
    if (window.innerWidth <= 768) {
        $("#search-box-desktop > #search-box-block").appendTo("#search-box-mobile");
    } else {
        $("#search-box-mobile > #search-box-block").prependTo("#search-box-desktop");
    }
};

function initShowHideCatAndBrands() {
    $('.dlCategories').find('tr:gt(3)').css({ "display": "none" });
    $('.dlManufacturers').find('tr:gt(3)').css({ "display": "none" });
    $('#showHide').text('View all Categories & Manufacturers');
    $('#showHideVal').val('0');
}

function showHideCatAndBrands() {
    if ($('#showHideVal').val() == '0') {
        $('.dlCategories').find('tr').removeAttr("style");
        $('.dlManufacturers').find('tr').removeAttr("style");
        $('#showHide').text('Hide Categories & Manufacturers');
        $('#showHideVal').val('1');

    } else {
        $('.dlCategories').find('tr:gt(3)').css({ "display": "none" });
        $('.dlManufacturers').find('tr:gt(3)').css({ "display": "none" });
        $('#showHide').text('View all Categories & Manufacturers');
        $('#showHide').css("display", "block");
        $('#showHideVal').val('0');
    }
};


function closeVehicleFilterWindow() {
    closeVehicleFilterPopup();
    window.setCookie("AP_VehicleFilterPopup", "1", 1);
    return false;
};

//+
function check_small_search_form() {
    var searchTerms = $("#small-searchterms");
    if (searchTerms.val() == "" || searchTerms.val() == "@searchTooltip") {
        alert('@Html.Raw(HttpUtility.JavaScriptStringEncode(T("Search.EnterSearchTerms").Text))');
        searchTerms.focus();
        return false;
    }
    return true;
};

function showLoadingScreen() {
    $("#loadScreen").css("display", "block");
};

function hideLoadingScreen() {
    $("#loadScreen").css("display", "none");
};

function replaceIfMobile(link) {
    if (window.mobilecheck() == true)
        window.location.replace(link);
};


/* WC. Hack for jQueryUI tabs*/
(function ($) {
    $.fn.disableTab = function (tabIndex, hide) {
        // Get the array of disabled tabs, if any
        var disabledTabs = this.tabs("option", "disabled");

        if ($.isArray(disabledTabs)) {
            var pos = $.inArray(tabIndex, disabledTabs);

            if (pos < 0) {
                disabledTabs.push(tabIndex);
            }
        }
        else {
            disabledTabs = [tabIndex];
        }

        this.tabs("option", "disabled", disabledTabs);

        if (hide === true) {
            $(this).find('li:eq(' + tabIndex + ')').addClass('ui-state-hidden');
        }

        // Enable chaining
        return this;
    };

    $.fn.enableTab = function (tabIndex) {
        // Remove the ui-state-hidden class if it exists
        $(this).find('li:eq(' + tabIndex + ')').removeClass('ui-state-hidden');
        // Use the built-in enable function
        this.tabs("enable", tabIndex);
        // Enable chaining
        return this;
    };
})(jQuery);
/* WC. END Hack for jQueryUI tabs*/
/* WC. END Custom Scripts */

// WC. Custom slider
(function ($) {
    $.fn.fadeInSlider = function (options) {
        var timer;
        var maxOpacity;
        var imageUrls;
        var minOpacity;
        var step;
        var interval = 3000;
        var speed = 20;
        var element = this;
        var fadeIn;

        function init() {
            maxOpacity = options.maxOpacity;
            imageUrls = options.imageUrls;
            step = 0.02;
            interval = options.interval;
            speed = options.speed;
            minOpacity = options.minOpacity;
            fadeIn = true;
            $(element).css('background-image', 'url("' + imageUrls[0] + '")');
            run();
            for (var i = 0; i < imageUrls.length; i++) {
                var img = new Image();
                img.src = imageUrls[i];
            }
        };

        function run() {
            clearInterval(timer);
            timer = window.setInterval(fadeInFunc, speed);
        }

        function fadeInFunc() {
            var opacity = parseFloat($(element).css('opacity'));
            if (fadeIn) {
                if (opacity < maxOpacity) {
                    opacity += step;
                    $(element).css('opacity', opacity);
                }

                if (opacity >= maxOpacity) {
                    clearInterval(timer);
                    fadeIn = false;
                    timer = window.setInterval(run, interval);
                }
            }
            else {
                if (opacity > minOpacity) {
                    opacity -= step;
                    $(element).css('opacity', opacity);
                }

                if (opacity <= minOpacity) {
                    clearInterval(timer);
                    $(element).css('background-image', nextImage());
                    fadeIn = true;
                    timer = window.setInterval(fadeInFunc, speed);
                }
            }
        };

        function nextImage() {
            return 'url("' + imageUrls[nextIndex()] + '")';
        };

        function nextIndex() {
            var url = $(element).css('background-image');
            var start = url.indexOf('url(');
            if (start === -1) {
                return 0;
            }

            var end = url.indexOf(')', start);
            if (end === -1) {
                return 0;
            }

            url = url.substring(start, end + 1);
            url = url.replace(/^url\(["']?/, '').replace(/["']?\)$/, '');
            var index = imageUrls.indexOf(url);
            return index === imageUrls.length - 1 ? 0 : ++index;
        };

        init();
    };
})(jQuery);
