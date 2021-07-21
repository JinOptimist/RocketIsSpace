function flip(element) {
    var foreground = element.find('.foreground');
    var background = element.find('.background');
    var from = getScaleX(element);
    var to = foreground.hasClass('hide') ? 1 : -1;

    $({ scale: from }).animate({ scale: to }, {
        duration: 500,
        step: function (now) {
            var current = getScaleX(element);
            if ((current >= 0 && now <= 0) || (current <= 0 && now >= 0)) {
                foreground.toggleClass('hide');
                background.toggleClass('show');
            }
            element.css({
                transform: 'scaleX(' + now + ')'
            });
        }
    });

    function getScaleX(element) {
        return element.css('transform').replace(/\(|\)|matrix/g, '').split(',')[0];
    }
}