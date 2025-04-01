(function ($) {
    APL.salesQuote = APL.salesQuote || {};

    APL.salesQuote = {
        dialog: $("#sales-quote-dialog"),
        trigger: $(".create-quote-btn"),
        submitBtn: $("#submit-quote"),
        loader: $(".box-loader"),
        form: $("#quote-form"),
        init: function () {
            var self = this;
            var dialog = this.dialog;
            if (APL.undef(dialog) || !$.fn.neysModal) {
                return;
            }

            dialog.neysModal({
                trigger: self.trigger
            });

            self.trigger.click(function () {
                dialog.show();
                return false;
            });

            function submit() {
                var loader = self.loader;
                loader.show();
                self.form.submit();
            }

            self.submitBtn.on("click", submit);

            function close() {
                dialog.data("neysModal").hide();
            };
        }
    },

    $(document).ready(function () {
        APL.init();
        APL.salesQuote.init();
    });
})(jQuery);