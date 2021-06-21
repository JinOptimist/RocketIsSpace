$(document).ready(function () {

    $('#flip-flop').each(function (element) {        
        console.log(element);
        element.on('click', () => flip(element));        
    });


    //var element = $('.flip-flop');
    //element.on('click', () => {
    //    flip(element);
    //});
})


function flip(element) {
    var foreground = element.find('.foreground');
    var background = element.find('.background');
    var from = getScaleX(element);
    var to = foreground.hasClass('hide') ? 1 : -1;

    $({ deg: from }).animate({ deg: to }, {
        duration: 500,
        step: function (now) {
            var prev = getScaleX(element);
            if ((prev >= 0 && now <= 0) || (prev <= 0 && now >= 0)) {
                foreground.toggleClass('hide');
                background.toggleClass('show');
            }
            element.css({
                transform: 'scaleX(' + now + ')'
            });
        }
    });

    function getScaleX(element) {
        console.log(element);
        return element.css('transform').replace(/\(|\)|matrix/g, '').split(',')[0];
    }
}