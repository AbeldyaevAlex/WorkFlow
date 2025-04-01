(function ($, window) {
    var wnd = window;

    APL.group.init = function () {
        if (typeof APL.group.data === "undefined") {
            return;
        }

        var data = APL.group.data;
        var url = "/lloyd/configurator?brand=" + data.brand + "&line=" + data.line + "&sid=" + data.sid + "&year=" + APL.veh.data.year + "&make=" + APL.veh.data.make + "&model=" + APL.veh.data.model + "&mobile=" + APL.isMobile();
        $(APL.group.trigger).on("click", function (e) {
            e.preventDefault();

            if (APL.isMobile()) {
                APL.openTab(url);
                return;
            }

            APL.group.modal.show();
            var iframe = $(APL.group.configurator).children("iframe")[0];
            iframe.src = url;
            iframe.onload = function () {
                try {
                    if (this.contentWindow.location.href.indexOf("/lloyd/postback") !== -1) {
                        wnd.location = "/cart";
                    }
                } catch (e) {
                    console.error(e);
                }
            }
        });
    };

    $(document).ready(function () {
        APL.init();
        APL.group.init();
    });
    
})(jQuery, window);
