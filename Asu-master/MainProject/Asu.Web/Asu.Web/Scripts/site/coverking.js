(function ($, document, window) {
    APL.group.init = function () {
        if (typeof APL.group.data === "undefined") {
            return;
        }

        var data = APL.group.data;
        var url = "https://iframe.coverking.com/?dl=" + data.dealer + "&prod=" + data.prodCategory + "&mcat=" + data.material + "&fullwidth=yes&SessionID=" + data.sid;

        $(APL.group.trigger).on("click", function (e) {
            e.preventDefault();

            if (APL.isMobile()) {
                APL.openTab(url);
                return;
            }

            APL.group.modal.show();
            var iframe = $(APL.group.configurator).children("iframe")[0];
            iframe.src = url;
        });
    };

    $(document).ready(function () {
        APL.init();
        APL.group.init();
    });
})(jQuery, document, window);

