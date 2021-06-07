$(document).ready(function () {
    var selectorToAmount = '.account-info-container .amount';
    $('.minus-money').click(function () {
        addMoney(-100);
    });

    $('.plus-money').click(function () {
        addMoney(100);
    });

    function addMoney(delta) {
        var startMoney = getMoney();
        var green = delta > 0 ? 3 : 1;
        var red = delta > 0 ? 1 : 3;

        $(selectorToAmount).animate(
            { now: '255' },
            {
                duration: 2 * 1000,
                step: function (now, fx) {
                    var colorVal = `rgb(${255 - (now / red)}, ${255 - (now / green)}, ${255 - now})`;
                    $(selectorToAmount).css('color', colorVal);
                    setMoney(startMoney + (now / 255) * delta);
                },
                complete: function () {
                    $(selectorToAmount).css('color', 'rgb(255, 255, 255)');
                    $(selectorToAmount).css('now', '0');
                }
            });
    }

    function getMoney() {
        return $(selectorToAmount).text().replace(',', '.') - 0;
    }

    function setMoney(money) {
        $(selectorToAmount).text(money.toFixed(2));
    }


});