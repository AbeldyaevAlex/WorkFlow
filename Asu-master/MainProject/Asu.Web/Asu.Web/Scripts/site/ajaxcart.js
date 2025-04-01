(function ($, window) {
    APL.ajaxcart = APL.ajaxcart || {};

    var loader = APL.loader;
    var messageBar = APL.messageBar;

    var success = function(response) {
        APL.setCookie("AddedToCart_Product", response.productid, 0.04);
        
        if (response.message) {
            if (response.success === true) {
                messageBar.show(response.message, "success", 3500);
            }
            else {
                messageBar.show(response.message, "error", 0);
                if (!APL.undef(APL.amazon)
                    && !APL.undef(APL.amazon.authWindow)
                    && APL.amazon.authWindow !== null
                    && !APL.amazon.authWindow.closed) {
                    APL.amazon.authWindow.close();
                }

                loader.hide();
                return false;
            }
        }

        if (response.redirect) {
            window.location.href = response.redirect;
            return true;
        }

        loader.hide();
        return false;
    };

    var failed = function () {
        loader.hide();
        messageBar.show("Failed to add the product to the cart. Please refresh the page and try one more time.", "error", 0);
    };

    APL.ajaxcart.add = function (addUrl, redirectUrl, formSelector) {
        loader.show();
        var settings = {
            type: "POST",
            url: addUrl,
            success: function(response) {
                if (!APL.undef(redirectUrl) && redirectUrl !== null) {
                    response.redirect = redirectUrl;
                }

                success(response);
            },
            complete: this.resetLoadWaiting,
            error: failed
        };

        if (!APL.undef(formSelector)) {
            settings.data = $(formSelector).serialize();
        }

        $.ajax(settings);
    };
})(jQuery, window);