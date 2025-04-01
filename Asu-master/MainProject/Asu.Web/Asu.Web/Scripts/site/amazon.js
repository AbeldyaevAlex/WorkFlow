(function() {
    APL.amazon = APL.amazon || {};

    APL.amazon.login = function() {
        APL.amazon.authWindow = APL.openWindow("https://" + location.host + "/LwaPopup", APL.currentStore.companyName + " - Login With Amazon", 800, 600, true);
    }
})();