(function () {
    var timeLeft = 10;
    var outputElementId = "countdown-time";
    var keepCounting = true;
    var noTimeLeftMessage = '<font color="red">a few more minutes!</font>';

    function countdown() {
        if (timeLeft < 2) {
            keepCounting = false;
        }

        timeLeft = timeLeft - 1;
    }

    function addLeadingZero(n) {
        if (n.toString().length < 2) {
            return "0" + n;
        } else {
            return n;
        }
    }

    function formatOutput() {
        var hours, minutes, seconds;
        seconds = timeLeft % 60;
        minutes = Math.floor(timeLeft / 60) % 60;
        hours = Math.floor(timeLeft / 3600);

        seconds = addLeadingZero(seconds);
        minutes = addLeadingZero(minutes);
        hours = addLeadingZero(hours);

        return hours + "h " + minutes + "m " + seconds + "s";
    }

    function showTimeLeft() {
        document.getElementById(outputElementId).innerHTML = formatOutput();
    }

    function noTimeLeft() {
        document.getElementById(outputElementId).innerHTML = noTimeLeftMessage;
    }

    APL.countdown = {
        count: function () {
            countdown();
            showTimeLeft();
        },
        timer: function () {
            var self = this;
            this.count.call(self);

            if (keepCounting) {
                setTimeout(function () { self.timer.call(self); }, 1000);
            } else {
                noTimeLeft();
            }
        },
        setTimeLeft: function (t) {
            timeLeft = t;
            if (!keepCounting) {
                this.timer();
            }
        },
        init: function (t, elementId) {
            timeLeft = t;
            outputElementId = elementId;
            this.timer.call(this);
        }
    };
})();