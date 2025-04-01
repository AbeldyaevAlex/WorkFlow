//(function ($, document, window) {
//    if (APL.currentStore.vehicleSupported) {
//        APL.veh.filter = $(APL.search.filter).neysVehicle({
//            clear: "#vClear",
//            ready: function () {
//                $(APL.search.filter).show();
//            },
//            changed: function () {
//                $(APL.search.loader).hide();
//                submitFilterForm();
//            },
//            loading: function () {
//                $(APL.search.loader).show();
//            },
//            done: function () {
//            }
//        });

//    } else {
//        $(APL.search.loader).hide();
//    }

//    $("#searchSubmitButton").click(function () {
//        $(APL.search.loader).hide();
//        $("#topSearchForm").submit();
//    });

//    var filtersSelector = "html, body, .master-wrapper-main .side-2, #searchFilter";
//    $(".aside-close-btn").on("click", function () {
//        $(filtersSelector).removeClass("filter-opened");
//        $(".main-content-overlay").hide();
//    });

//    var asideFilter = ".aside-filter-item-vehicle";
//    $(document).on("click", asideFilter, function () {
//        APL.veh.filter.clear();
//    });

//    asideFilter = ".aside-filter-item-category";
//    $(document).on("click", asideFilter, function () {
//        var id = $(this).data("id");
//        var checkBox = "#filterCategory input[type='checkbox'][value='" + id + "'" + "]";
//        var label = $(checkBox).closest("label");
//        $(label).trigger("click");
//    });

//    asideFilter = ".aside-filter-item-manufacturer";
//    $(document).on("click", asideFilter, function () {
//        var id = $(this).data("id");
//        var checkBox = "#filterManufacturer input[type='checkbox'][value='" + id + "'" + "]";
//        var label = $(checkBox).closest("label");
//        $(label).trigger("click");
//    });

//    var priceFilter = ".aside-filter-item-price";
//    $(document).on("click", priceFilter, function () {
//        $("#minPrice").val("");
//        $("#maxPrice").val("");
//        $("#filterMinPrice").val("");
//        $("#filterMaxPrice").val("");
//        $("#sortByPriceRange").trigger("click");
//    });

//    var infiniteScroll = $(".infinite-scroll");

//    if (infiniteScroll.length > 0) {
//        $(infiniteScroll).jscroll({
//            nextSelector: "#nextPageBlock",
//            callback: onDownloadNextPage
//        });

//        onDownloadNewPage();
//    }

//})(jQuery, document, window);