$(document).ready(function () {
    var final = 1000;
    var nowCount = 0;
    var selectorToAmount = '.account-info-container .amount';
    $('.minus-money').click(function () {
        addMoney(-100);

        
    });

    $('.plus-money').click(function () {
        addMoney(100);
    });

    function addMoney(delta) {
        if (delta > 0) {
            animateColor(2 * 1000, 0, 255, 0, delta);
        } else {
            animateColor(2 * 1000, 255, 0, 0, delta);
        }

        var accoutNumber = $('.account-info-container .accountNumber').text();
        var url = `/Account/UpdateAmount?accoutNumber=${accoutNumber}&delta=${delta}`
        $.get(url);
    }

    function getMoney() {
        return $(selectorToAmount).text().replace(',', '.') - 0;
    }

    function setMoney(money) {
        $(selectorToAmount).text(money.toFixed(2));
    }

    function animateCustom(speed, step1, step2) {
        var propName = `now${nowCount++}`;
        var prop = {};
        prop[propName] = `${final}`;

        var options = {
            duration: speed,
            step: function (now) {
                step1(now);
                step2(now);
            },
            complete: function () {
                /*$(selectorToAmount).css(`now${nowCount}`, '0');*/
            }
        };
        $(selectorToAmount)
            .animate(
                prop,
                options);
    }

    function animateColor(speed, redNew, greenNew, blueNew, delta) {
        var colors =
            $(selectorToAmount)
                .css('color')
                .replace(' ', '')
                .replace('rgba(', '')
                .replace('rgb(', '')
                .replace(')', '')
                .split(',')
                .map(x => x - 0);
        //[123, 30, 9]
        var redOld = colors[0];
        var greenOld = colors[1];
        var blueOld = colors[2];

        var redDelta = redNew - redOld;
        var greenDelta = greenNew - greenOld;
        var blueDelta = blueNew - blueOld;

        var colorStep = function (now) {
            var redStep = redOld + redDelta * now / final;
            var greenStep = greenOld + greenDelta * now / final;
            var blueStep = blueOld + blueDelta * now / final;
            var colorVal = `rgb(${redStep}, ${greenStep}, ${blueStep})`;
            $(selectorToAmount).css('color', colorVal);
        }

        var startMoney = getMoney();
        var textStep = function (now) {
            setMoney(startMoney + (now / final) * delta);
        }

        animateCustom(speed, textStep, colorStep);
    }
});