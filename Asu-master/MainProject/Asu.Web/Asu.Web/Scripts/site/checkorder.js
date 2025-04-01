(function ($) {
    APL.checkOrder = APL.checkOrder || {};
    APL.checkOrder = {
        $orderNumber: $("#OrderNumber"),
        $ZipCode: $("#ZipCode"),
        init: function () {
            var self = this;
            self.$orderNumber.rules("add", {
                required: true
            });
            self.$ZipCode.rules("add", {
                required: true
            });

            var form = self.$orderNumber.closest("form");
            form.validate();
        }
    }
})(jQuery);