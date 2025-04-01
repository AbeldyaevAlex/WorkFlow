(function ($, window) {
    APL.group = APL.group || {};
    if (!APL.undef(APL.group.gallery) && $.fn.neysGallery) {
        var $gallery = $(APL.group.gallery);
        $gallery.neysGallery({
            onReady: function() {
                $(APL.group.gallery).show();
            }
        });
    }

    if (!APL.undef(APL.group.configurator) && $.fn.neysModal) {
        APL.group.modal = $(APL.group.configurator).neysModal({
            behaviour: "iframe-content",
            onReady: function() {
                $(APL.group.configurator).show();
            }
        });
    }

    var $tabs = $("#details-tabs");

    var resize = function (width) {
        if (width <= 480) {
            if ($tabs.data("uiTabs")) {
                $tabs.tabs("destroy");
            }

            if (!$tabs.data("uiAccordion")) {
                $tabs.accordion({
                    header: ".accordion-header",
                    collapsible: true,
                    heightStyle: "content",
                    create: function (e, ui) {
                        $(".tab-headers").hide();
                        $(".accordion-header").show();
                        $(this).show();
                    }
                });
            }
        }

        if (width > 480) {
            if ($tabs.data("uiAccordion")) {
                $tabs.accordion("destroy");
            }

            if (!$tabs.data("uiTabs")) {
                $tabs.tabs({
                    create: function (e, ui) {
                        $(".accordion-header").hide();
                        $(".tab-headers").show();
                        $(this).show();
                    }
                });
            }
        }
    };

    var windowResize = function () {
        resize.call(this, $(this).width());
    };

    var orientationChange = function () {
        resize.call(this, $(this).width());
    };

    $("div.rateit, span.rateit").rateit();
    $(window).on("resize", windowResize);
    $(window).on("orientationchange", orientationChange);
    resize($(window).width());
})(jQuery, window);