(function () {
    APL.veh = APL.veh || {};
    APL.veh.data = {
        cookie: "WC.Vehicle.Name.Cookie",
        exists: false,
        year: 0,
        make: "",
        model: "",
        init: function () {
            var cookie = APL.getCookie(this.cookie);
            if (cookie === "") {
                this.exists = false;
                return;
            }

            this.exists = true;
            var parts = cookie.split("|");
            if (parts.length < 4) {
                return;
            }

            this.year = parts[0];
            this.make = parts[1];
            this.model = parts[2];
        }
    };

    APL.veh.data.init();
})();

