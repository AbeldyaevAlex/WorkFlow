var onDownloadNewPage = function () {
    if (window.mobilecheck() !== true) {
        $("#pager").show();
        return;
    } else {
        $("#pager").hide();
    }

    var searchForm = $('#searchForm').first();
    var pageIndex = searchForm.children('#pageIndex').first();
    var totalProducts = $('span#totalProducts').first().text();

    var productsOnPage = 15;
    if ((parseInt(pageIndex.val()) * productsOnPage) < parseInt(totalProducts)) {
        $("#productsList").append('<span id="nextPageBlock" style="display:none;" />');
        DownloadNextPage();
    }
};

function DownloadNextPage() {
    //alert('id: ' + $e.attr('id') + ', class: ' + $e.attr('class') + ', type: ' + $e[0].tagName);
    var searchForm = $('#searchForm').first();
    var pageIndex = searchForm.children('#pageIndex').first();

    var searchFormCopy = searchForm.clone();
    var pageIndexCopy = searchFormCopy.children('#pageIndex').first();
    pageIndexCopy.val(parseInt(pageIndex.val()) + 1);

    //alert('Download started');

    if (searchFormCopy.valid()) {
        //$('input', $(this)).each(function() {$(this).val() == "" && $(this).remove();});
        var params = searchFormCopy.serialize();
        $.ajax({
            url: searchFormCopy.attr('action'),
            type: searchFormCopy.attr('method'),
            dataType: "json",
            data: searchFormCopy.serialize(),
            success: function (result) {
                $("#productsList .next-page-download-waiting-block").remove();

                //$("#filterCheckBoxes").html(result.FilterHtml);
                $("#totalProducts").html(formatNumber(result.TotalProducts));
                //$("#pageNumber").html(result.PageNumber);
                if ($("#productsList #nextPageBlock").first().length === 0) {
                    pageIndex.val(parseInt(result.PageNumber));
                    $("#pageNumber").html(pageIndex.val());

                    $("#productsList .jscroll-inner").append(result.ProductsHtml);
                    var productsOnPage = 15;
                    if ((parseInt(result.PageNumber) * productsOnPage) < parseInt(result.TotalProducts)) {
                        $("#productsList").append('<span id="nextPageBlock" style="display:none;" />');
                        DownloadNextPage();
                    }
                }
                else {
                    $("#productsList .jscroll-inner").append("<div id='nextPageData' style='display:none;'>" + result.ProductsHtml + "</div>");
                }
                $("#pager").html(result.PagerHtml);

                $("img.lazyload").each(function () {
                    $(this).attr("src", $(this).attr("data-original"));
                    $(this).removeAttr("data-original");
                });

                //$("img.lazy").lazyload({ effect: "fadeIn" });
                //hideLinks();

                //alert('Download finished');
            }/*,
            fail: function () {
                alert("Sorry, an error has occurred. Please refresh the page and try your action again.");
            }*/
        });
    }
};

var onDownloadNextPage = function () {
    if (window.mobilecheck() != true) {
        $("#pager").show();
        return;
    } else {
        $("#pager").hide();
    }

    var searchForm = $('#searchForm').first();
    var pageIndex = searchForm.children('#pageIndex').first();
    var totalProducts = $('span#totalProducts').first().text();

    var productsOnPage = 15;
    if ((parseInt(pageIndex.val()) * productsOnPage) < parseInt(totalProducts)) {
        if ($("#productsList #nextPageData").first().length == 0) {
            if ($("#productsList .next-page-download-waiting-block").first().length == 0) {
                $("#productsList .jscroll-inner")
                    .append('<div class="next-page-download-waiting-block"><span class="next-page-download-waiting">Downloading next page..</span></div>');
            }
        } else {
            $("#productsList #nextPageData").first().children().unwrap();

            pageIndex.val(parseInt(pageIndex.val()) + 1);
            $("#pageNumber").html(pageIndex.val());

            if (((parseInt(pageIndex.val())) * productsOnPage) < parseInt(totalProducts)) {
                $("#productsList").append('<span id="nextPageBlock" style="display:none;" />');
                DownloadNextPage();
            }
        }
    }
};

APL.search = APL.search || {};
APL.veh = APL.veh || {};
var isMobile = window.mobilecheck();


APL.search.filter = "#vehiclesFilter";
APL.search.loader = ".box-loader";
APL.search.categoryFilterContainer = "#filterCategoriesBlock";
APL.search.manufacturerFilterContainer = "#filterManufacturerBlock";
APL.search.vehicleFilterContainer = "#vehicleFilterContainer";
APL.pageType = $("#pageType").val();
APL.tirePageRequested = APL.pageType === "Tires" || APL.pageType === "Category Results";


APL.mobileHeaderLinks = function () {
    var $menu = $(".mob-header-links-menu");
    $("#mob-header-links-button").on("click", function () {
        if ($menu.hasClass("show")) {
            $menu.hide();
            $menu.removeClass("show");
        } else {
            $menu.addClass("show");
            $menu.show();
        }
    });
};

function initUrl() {
    var formClone = $("#searchForm").clone(true);
    $("input", formClone).each(function() {
         $(this).val() === "" && $(this).remove();
    });

    var params = formClone.serialize();

    var markup = document.documentElement.innerHTML;

    if ($(".side-2").find("input:checkbox:checked").filter(function () { return $(this).attr("id") != "loadOnlyInStock" && $(this).attr("id") != "vf-show-universal" }).length > 0) {
        var markup = document.documentElement.innerHTML;
        var urlPath = location.protocol + '//' + location.host + location.pathname + '?' + params;
        window.history.replaceState(markup, document.title, urlPath);
    } else if ($("#pageNumber").text() > 1) {
        var pageNumber = $("#pageNumber").text();
        var markup = document.documentElement.innerHTML;
        var path = location.protocol + '//' + location.host + location.pathname + '?' + "PFC.PageNumber=" + pageNumber;
        window.history.replaceState(markup, document.title, path);
    }
}

APL.mobileHeaderLinks();

initImagesLazyLoad();

initUrl();

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
    fix: function () {
        // fix of jPanelMenu style that affects other page elements when menu closed
        this.$menu.css("transform", "none");
    }
}

var mobileMenuInit = function () {
    if (!APL.mobileMenu.ready) {
        APL.mobileMenu.init();
    }
};

var resize = function () {
    var width = $(this).width();
    if (width <= 480) {
        mobileMenuInit();
    }
};

var windowResize = function () {
    resize.call(this);
};

var orientationChange = function () {
    resize.call(this);
};

var defaults = {
    selectors: {
        hidden: {
            price: {
                min: "#filterMinPrice",
                max: "#filterMaxPrice"
            },
            vehicle: {
                year: "#filterYear",
                make: "#filterMake",
                model: "#filterModel",
                subModel: "#filterSubModel",
                showUniversal: "#filterShowUniversal"
            }
        },
        inputs: {
            price: {
                min: "#minPrice",
                max: "#maxPrice"
            }
        },
        primaryFilter: "#filterPrimary"
    }
};

initVehicle();
initFilters();

function initCheckboxFilters(typeName, filterSelector, selectedSelector) {
    $(document).on("change", filterSelector + " input[type='checkbox']", function () {
        var primaryFilter = $(defaults.selectors.primaryFilter);
        if (primaryFilter.val() === "None") {
            primaryFilter.val(typeName);
        }

        var selectedFilterItems = $(selectedSelector);
        selectedFilterItems.val($(filterSelector + " input[type='checkbox']:checked").map(function () {
            return $(this).val();
        }).get().join(","));

        if ($(primaryFilter).val() === typeName && selectedFilterItems.val() === "") {
            primaryFilter.val("None");
        }

        submitFilterForm();
    });
}

function initFilters() {
    var selectors = defaults.selectors;
    var filtersSelector = "html, body, .master-wrapper-main .side-2, #searchFilter";
    $(".aside-close-btn").on("click", function () {
        $(filtersSelector).removeClass("filter-opened");
        $(".main-content-overlay").hide();
    });

    initCheckboxFilters("Category", "#filterCategory", "#filterSelectedCategories");
    initCheckboxFilters("Manufacturer", "#filterManufacturer", "#filterSelectedManufacturers");
    initCheckboxFilters("PriceRange", "#filterPriceRange", "#filterSelectedPriceRanges");
    initCheckboxFilters("TirePerformance", "#filterTirePerformance", "#filterSelectedTirePerformanceAttributes");
    initCheckboxFilters("TireLoad", "#filterTireLoad", "#filterSelectedTireLoadAttributes");
    initCheckboxFilters("TireSpeed", "#filterTireSpeed", "#filterSelectedTireSpeedAttributes");
    initCheckboxFilters("TireTreadType", "#filterTireTreadType", "#filterSelectedTreadTypeAttributes");
    initCheckboxFilters("TireSidewall", "#filterTireSidewall", "#filterSelectedSidewallAttributes");
    initCheckboxFilters("TireLoadRange", "#filterLoadRange", "#filterSelectedLoadRangeAttributes");
    initCheckboxFilters("TireUtqg", "#filterUtqg", "#filterSelectedUtqgAttributes");
    initCheckboxFilters("TireServiceDescription", "#filterTireServiceDescription", "#filterSelectedServiceDescriptionTypeAttributes");
    initCheckboxFilters("TireSize", "#filterTireSize", "#filterSelectedTireSizeAttributes");
    initCheckboxFilters("TireRimSize", "#filterTireRimSize", "#filterSelectedTireRimSizeAttributes");

    initAsideFilter(".aside-filter-item-category", "#filterCategory");
    initAsideFilter(".aside-filter-item-manufacturer", "#filterManufacturer");
    initAsideFilter(".aside-filter-item-tire-performance", "#filterTirePerformance");
    initAsideFilter(".aside-filter-item-tire-load", "#filterTireLoad");
    initAsideFilter(".aside-filter-item-tire-speed", "#filterTireSpeed");
    initAsideFilter(".aside-filter-item-tire-treadtype", "#filterTireTreadType");
    initAsideFilter(".aside-filter-item-tire-sidewall", "#filterTireSidewall");
    initAsideFilter(".aside-filter-item-tire-loadrange", "#filterLoadRange");
    initAsideFilter(".aside-filter-item-tire-utqg", "#filterUtqg");
    initAsideFilter(".aside-filter-item-tire-service-description", "#filterTireServiceDescription");
    initAsideFilter(".aside-filter-item-tire-size", "#filterTireSize");
    initAsideFilter(".aside-filter-item-tire-rim-size", "#filterTireRimSize");

    var priceFilter = ".aside-filter-item-price";
    $(document).on("click", priceFilter, function () {
        $(selectors.hidden.price.min).val("");
        $(selectors.hidden.price.max).val("");
        $(selectors.inputs.price.min).val("");
        $(selectors.inputs.price.max).val("");
        $("#sortByPriceRange").trigger("click");
    });

    var subCategoryFilters = $(".sub-category-filter-link");
    if (subCategoryFilters.length > 0) {
        $.each(subCategoryFilters,function () {
            $(this).on("click", function () {
                displayAjaxLoading(true);
                var form = $("#searchForm");
                form.attr('action', $(this).next("a").attr("href"));
                $("#filterSearchCategory").val("100187");
                $("#filterSearchCategory").val("100187");
                $("#filterSelectedCategories").val($("#filterCategory input[type='checkbox']:checked").map(function () { return $(this).val(); }).get().join(","));
                $("#filterSelectedManufacturers").val($("#filterManufacturer input[type='checkbox']:checked").map(function () { return $(this).val(); }).get().join(","));
                $("#filterSelectedPriceRanges").val($("#filterPriceRange input[type='checkbox']:checked").map(function () { return $(this).val(); }).get().join(","));
                submitFilterForm(0);
            });
        });
    }
}

function initAsideFilter(filterSelector, containerSelector) {
    $(document).on("click", filterSelector, function () {
        var id = $(this).data("id");
        var checkBox = containerSelector + " input[type='checkbox'][value='" + id + "'" + "]";
        var label = $(checkBox).closest("label");
        $(label).trigger("click");
    });
}

$("#searchSubmitButton").click(function () {
    hideLoadingScreen();
    $("#topSearchForm").submit();
});

var infiniteScroll = $(".infinite-scroll");

if (infiniteScroll.length > 0) {
    $(infiniteScroll).jscroll({
        nextSelector: "#nextPageBlock",
        callback: onDownloadNextPage
    });

    onDownloadNewPage();
}

function showLoadingScreen() {
    $("#loadScreen").css("display", "block");
};

function hideLoadingScreen() {
    $("#loadScreen").css("display", "none");
};

function submitFilterForm(pageIndex = 0) {
    displayAjaxLoading (true);
    $("#pageIndex").val(pageIndex);
    if ($("#searchForm").length !== 0) {
        $("#searchForm").submit();
    } else {
        displayAjaxLoading ();
    }
};

$(document).on("change", "#vf-show-universal", function () {
    displayAjaxLoading (true);
    var suLabel = $("#vf-show-universal").parents("label.checkbox");
    var filter = $("#filterShowUniversal");
    if (suLabel.length > 0) {
        filter.val(suLabel.hasClass("checked"));
    }
});

if ($("#loadOnlyInStock").length > 0) {
    $(document).on("change", "#loadOnlyInStock", function () {
        displayAjaxLoading (true);
        $("#loadOutStockProducts").val(!$("#loadOnlyInStock").prop('checked'));
        submitFilterForm();
    });
}

var brandSearchFilter =  $("#brand-search-infilter");
if (brandSearchFilter.length > 0) {
    brandSearchFilter.on("input", function () {
        var term = $(this).val().toLowerCase();
        var brandElements = $("#filterManufacturer > li");
        if (term === "") {
            brandElements.show();
        }

        brandElements.each(function () {
            var that = $(this);
            var manufacturerName = that.find("a").text().toLowerCase();
            if (manufacturerName.indexOf(term) !== 0) {
                that.hide();
            } else {
                that.show();
            }
        });
    });
}

$(document).on("click", "#vClear", clearVehicleFilter);
$(document).on("click", ".aside-filter-item-vehicle", function () {
    displayAjaxLoading (true);
    $(this).remove();
    $("#vClear").trigger("click");
});

function clearVehicleFilter() {
    var selectors = defaults.selectors.hidden.vehicle;
    $(selectors.year).val("");
    $(selectors.make).val("");
    $(selectors.model).val("");
    $(selectors.submodel).val("");
    $(selectors.showUniversal).val("");
    $(".su-container").hide();
}

$(document).on("click", "#sortByPriceRange", function (e) {
    displayAjaxLoading (true);
    e.preventDefault();
    if (!validatePrice($("#minPrice").val()) || !validatePrice($("#maxPrice").val())) return;
    var minPrice = parseFloat($("#minPrice").val());
    var maxPrice = parseFloat($("#maxPrice").val());
    if (minPrice > maxPrice) return;
    $("#filterMinPrice").val(minPrice);
    $("#filterMaxPrice").val(maxPrice);
    if ($("#filterPrimary").val() === "PriceRange") { $("#filterPrimary").val("None"); }
    $("#filterSelectedPriceRanges").val("");
    submitFilterForm(0);
});

$(document).on("change", "#filterPriceRange input[type='checkbox']", function () {
    displayAjaxLoading (true);
    if ($("#filterPrimary").val() === "None") {$("#filterPrimary").val("PriceRange");}
    $("#filterSelectedPriceRanges").val($("#filterPriceRange input[type='checkbox']:checked").map(function () {return $(this).val();}).get().join(","));
    if ($("#filterPrimary").val() === "PriceRange" && $("#filterSelectedPriceRanges").val() === "") {$("#filterPrimary").val("None");}
    submitFilterForm(0);
});

//$(document).on("change", "#filterTireCategory input[type='checkbox']", function () {
//    displayAjaxLoading (true);
//    if ($("#filterPrimary").val() === "None") { $("#filterPrimary").val("Tires"); }
//    $("#filterSelectedPriceRanges").val($("#filterTireCategory input[type='checkbox']:checked").map(function () { return $(this).val(); }).get().join(","));
//    if ($("#filterPrimary").val() === "Tires" && $("#filterSelectedCategoryAttributes").val() === "") { $("#filterPrimary").val("None"); }
//    submitFilterForm(0);
//});

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

function clearFilter() {
    displayAjaxLoading (true);
    $("#filterPrimary").val("None");
    $("#filterSelectedCategories").val("");
    $("#filterSelectedManufacturers").val("");
    $("#filterSelectedPriceRanges").val("");

    var categoryId = $("#filterSearchCategory").val();
    if (APL.pageType === "Tires" || categoryId == 17 || categoryId == 214 || categoryId == 7636) {
        var tireAttributeField = $("#filterSelectedTirePerformanceAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedTireLoadAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedTireSpeedAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedTreadTypeAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedSidewallAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedLoadRangeAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedUtqgAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedServiceDescriptionAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedTireSizeAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }

        tireAttributeField = $("#filterSelectedTireRimSizeAttributes");
        if (!isUndefined(tireAttributeField)) {
            tireAttributeField.val("");
        }
    }

    $("#filterMinPrice").val("");
    $("#filterMaxPrice").val("");
    submitFilterForm();
};

function isUndefined(obj) {
    return typeof obj === "undefined";
}

var filterSectionCollapsed = true,
    filterCategoryCollapsed = true,
    filterManufacturerCollapsed = true,
    filterPriceCollapsed = true,
    filterSortCollapsed = true;

$(document).ready(function () {
    $(window).on("resize", windowResize);
    $(window).on("orientationchange", orientationChange);
    resize($(window).width());

    if (getCookie("WC.Vehicle.Name.Cookie") !== "" && window.mobilecheck() === true) {
        $("#vehiclesFilter").addClass("collapsed");
    }

    if (APL.currentStore.vehicleSupported) {
        var vehicleFilter = APL.veh.filter;
        $("#filterYear").val(vehicleFilter.year);
        $("#filterMake").val(vehicleFilter.make);
        $("#filterModel").val(vehicleFilter.model);
        $("#filterSubModel").val(vehicleFilter.submodel);
        $("#filterShowUniversal").val(vehicleFilter.universal);
    }

    var filtersSelector = "html, body, .master-wrapper-main .side-2, #searchFilter";
    $(".aside-close-btn").on("click", function () {
        $(filtersSelector).removeClass("filter-opened");
        $(".main-content-overlay").hide();
    });

    $(".mobile-filter-open-btn").on("click", function () {
        $(filtersSelector).addClass("filter-opened");
        $(".main-content-overlay").show();
        $("html, body").scrollTop(0);
        $("#searchFilter").show();
    });

    $(document).on("change", "input[name='sorter']:radio", function () {
        setSortOption($("input[name='sorter']:checked").val());
    });

    $("#products-orderby").selectmenu({
        change: function () {
            setSortOption($(this).val());
        }
    });

    function setSortOption(type) {
        $("#productsOrderBy").val(type);
        submitFilterForm();
    };

    $(document).on("keyup", "#minPrice", function (event) {
        if (event.keyCode == 13) {
            $("#sortByPriceRange").click();
        }
    });

    $(document).on("keyup", "#maxPrice", function (event) {
        if (event.keyCode == 13) {
            $("#sortByPriceRange").click();
        }
    });

    $(document).on("keypress", "#minPrice", function (event) {
        return isNumericKey(event);
    });

    $(document).on("keypress", "#maxPrice", function (event) {
        var value = $("#" + event.target.id)[0].value;
        var price = value + String.fromCharCode(event.keyCode);
        if (event.keyCode === 46) {
            return validatePrice(price, /^\d{0,8}\.$/);
        }
        return isNumericKey(event) && validatePrice(price);
    });

    function isNumericKey(event) {
        var keyCode = (event.which) ? event.which : event.keyCode;
        if (keyCode !== 46 && keyCode > 31 && (keyCode < 48 || keyCode > 57)) {
            return false;
        }

        return true;
    }

    if (APL.tirePageRequested) {
        APL.tireConfigurator.init();
    }

    //var isVehicleSeoPage = $("#filterIsVehicleSeoPage").val() === "True" ? true : false;
    //if (!isVehicleSeoPage) {
    //     showVehicleFilterPopup();
    //}
});

function initImagesLazyLoad() {
    $("html, body").animate({ scrollTop: 0 }, "normal");
    $("img.lazy").lazyload({ effect: "fadeIn" });
};

$(function () {
    $('#searchForm').submit(function () {
        if ($(this).valid()) {
            displayAjaxLoading (true);

            if (getCookie("vehEnteredGa") !== "") {
                var vehicleFilter = defaults.selectors.hidden.vehicle;
                $("#filterYear").val($(vehicleFilter.year).val());
                $("#filterMake").val($(vehicleFilter.make).val());
                $("#filterModel").val($(vehicleFilter.model).val());
                $("#filterSubModel").val($(vehicleFilter.subModel).val());
                $("#filterShowUniversal").val($(vehicleFilter.showUniversal).val());
            }
        }
    });
});

function initMobileFilterMenu() {
    try {
        initRadioButtons();
        if (!filterSectionCollapsed) {
            $("#sub-category-list").removeClass("collapsed");
            $("#sub-category-list").prev(".filterName").removeClass("plus").addClass("minus");
        } else {
            $("#sub-category-list").prev(".filterName").removeClass("minus").addClass("plus");
        }

        if (!filterCategoryCollapsed) {
            $("#filterCategory").removeClass("collapsed");
            $("#filterCategory").prev(".filterName").removeClass("plus").addClass("minus");
        } else {
            $("#filterCategory").prev(".filterName").removeClass("minus").addClass("plus");
        }

        if (!filterManufacturerCollapsed) {
            $("#filterManufacturer").removeClass("collapsed");
            $("#filterManufacturer").prev(".filterName").removeClass("plus").addClass("minus");
        } else {
            $("#filterManufacturer").prev(".filterName").removeClass("minus").addClass("plus");
        }

        if (!filterPriceCollapsed) {
            $("#filterPriceInput").removeClass("collapsed");
            $("#filterPriceInput").prev(".filterName").removeClass("plus").addClass("minus");
        } else {
            $("#filterPriceInput").prev(".filterName").removeClass("minus").addClass("plus");
        }

        if (!filterSortCollapsed) {
            $("#sort-by").removeClass("collapsed");
            $("#sort-by").prev(".filterName").removeClass("plus").addClass("minus");
        } else {
            $("#sort-by").prev(".filterName").removeClass("minus").addClass("plus");
        }
    } catch (e) {
    }
}

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}

function applySeoChanges(pageNumber, hasNextPage, hasPreviousPage) {
    try {
        var path = location.protocol + '//' + location.host + location.pathname;
        var $canonical = $("link[rel='canonical']");
        var $next = $("link[rel='next']");
        var $prev = $("link[rel='prev']");

        if (pageNumber > 1) {
            $canonical.attr("href", path + "?PFC.PageNumber=" + pageNumber);
        } else {
            $canonical.attr("href", path);
        }

        if (hasNextPage) {
            var nextLink = path + "?PFC.PageNumber=" + (pageNumber + 1);
            if ($next.length > 0) {
                $next.attr("href", nextLink);
            } else {
                $("head").append('<link rel="next" href="' + nextLink + '">');
            }
        } else {
            if ($next.length > 0) {
                $next.remove();
            }
        }

        if (hasPreviousPage) {
            var prevLink = path + "?PFC.PageNumber=" + (pageNumber - 1);
            if ($prev.length > 0) {
                $prev.attr("href", prevLink);
            } else {
                $("head").append('<link rel="prev" href="' + prevLink + '">');
            }
        } else {
            if ($prev.length > 0) {
                $prev.remove();
            }
        }
    } catch (e) {
        console.log(e);
    }
};

/* Vehicle filter popup */

function closeVehicleFilterPopup() {
    $("#vehiclesFilterPopup").hide();
    $("#vehiclesFilter").appendTo("#homepagelagebanner");
    $('#homepagelagebanner').show();
}

function showVehicleFilterPopup() {
    if (parseInt($('#totalProducts').val()) < 30) {
        return;
    }

    if (APL.veh.filter && (APL.veh.filter.isSet())) {
         return;
    }

    $("#vehiclesFilter").appendTo("#vehiclesFilterPopupContent");
    $("#vehiclesFilter").addClass("vehicles-filter");
    $("#vehiclesFilterPopup").show();
};

function validatePrice(price, pattern) {
    var regex = new RegExp(/^\d{0,8}(\.\d{1,4})?$/);
    if (pattern !== typeof "undefined") {
        regex = new RegExp(pattern);
    }

    return regex.test(price);
};

function closeVehicleFilterWindow() {
    closeVehicleFilterPopup();
    window.setCookie("AP_VehicleFilterPopup", "1", 1);
    return false;
};

window.setCookie = function (name, value, days) {
    var date = new Date(), expires = "";

    if (days) {
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = ";expires=" + date.toUTCString();
    }

    document.cookie = name + "=" + value + expires + ";path=/";
};

window.getCookie = function (cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) return c.substring(name.length, c.length);
    }
    return '';
};

//we bind only to the rateit controls within the products div
$(document).on('rated', '#rateit-product .rateit', function (e) {
    var ri = $(this);

    //if the use pressed reset, it will get value: 0 (to be compatible with the HTML range control), we could check if e.type == 'reset', and then set the value to  null .
    var value = ri.rateit('value');
    var productId = ri.data('productid'); // if the product id was in some hidden field: ri.closest('li').find('input[name="productid"]').val()

    //maybe we want to disable voting?
    ri.rateit('readonly', true);

    $.ajax({
        url: 'Customization/UpdateProductRatingData', //your server side script
        data: { ProductId: productId, Score: value }, //our data
        type: 'POST',
        success: function (data) {
            var json = $.parseJSON(JSON.stringify(data));
            if (json.ResponseCode == 0) {
                ri.rateit('value', json.RatingScore);
            }
            $('#response').append('<li>' + json.ResponseText + '</li>');
        },
        error: function (jxhr, msg, err) {
            $('#response').append('<li style="color:red">' + msg + '</li>');
        }
    });
});

function displayAjaxLoading(display) {
    if (display) {
        showLoadingScreen();
    }
    else {
        hideLoadingScreen();
    }
};

function initVehicle() {
    if (APL.currentStore.vehicleSupported) {
        APL.veh.filter = $(APL.search.filter).neysVehicle({
            clear: "#vClear",
            ready: function () {
                displayAjaxLoading (true);
            },
            changed: function () {
                if (getCookie("vehEnteredGa") !== "") {
                    var vehicleFilter = $("#vehiclesFilter");
                    $("#filterYear").val(vehicleFilter.find("select[data-year]").val());
                    $("#filterMake").val(vehicleFilter.find("select[data-make]").val());
                    $("#filterModel").val(vehicleFilter.find("select[data-model]").val());
                    $("#filterSubModel").val(vehicleFilter.find("select[data-submodel]").val());
                }

                submitFilterForm();
                displayAjaxLoading (true); 
            },
            loading: function () {
                displayAjaxLoading (true);
            },
            done: function () {
                displayAjaxLoading (); 
            }
        });

    } else {
        $(APL.search.loader).hide();
    }
}

APL.tireConfigurator = {
    default: "Search by Part #",
    $sectionDd: $("#section"),
    $aspectDd: $("#aspect"),
    $rimDd: $("#rim"),
    $form: $("#tire-configurator-form"),
    $btn: $("#tFindTireBtn"),
    $changeSizeBtn: $("#changeSizeBtn"),
    $modalWnd: $("#tire-configurator"),
    init: function () {
        var that = this;
        var options = {};
        //this.$form.on("submit", function () {
        //    var text = that.$input.val();
        //    return !(APL.nullOrEmpty(text) || text === that.default);
        //});
        var modalWnd = this.$modalWnd;

        modalWnd.neysModal({
            trigger: that.$changeSizeBtn
        });

        this.$changeSizeBtn.on("click", function () {
            modalWnd.show();
            that.$aspectDd.selectmenu({
                change: function () {
                    getRimValues(that.$sectionDd, that.$aspectDd, that.$rimDd);
                }
            });

            that.$rimDd.selectmenu(options);

            that.$sectionDd.selectmenu({
                change: function (event, ui) {
                    getAspectValues(that.$sectionDd, that.$aspectDd, that.$rimDd);
                }
            });

            getSectionValues(that.$sectionDd, that.$aspectDd, that.$rimDd);

            return false;
        });

        function close() {
            modalWnd.data("neysModal").hide();
        }

        this.$btn.click(function () {
            var section = that.$sectionDd.val();
            var aspect = that.$aspectDd.val();
            var rim = that.$rimDd.val();
            if (section === "" && aspect === "" && rim === "") {
                that.$sectionDd.selectmenu("open");
                return;
            }

            if (section !== "" && aspect === "" && rim === "") {
                that.$aspectDd.selectmenu("open");
                return;
            }

            if (section !== "" && aspect !== "" && rim === "") {
                that.$rimDd.selectmenu("open");
                return;
            }

            that.$form.submit();
        });
    }
};

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
        data: { sectionValue: sectionDefaultValue, target: "aspect" },
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
        data: { sectionValue: sectionDefaultValue, aspectValue: aspectDefaultValue, target: "rim" },
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

