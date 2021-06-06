$(document).ready(function () {

    var isAnimationActive = false;
    var time = 300;

    $('.circle').hover(function () {
        $(this).children()
            .toggleClass('material-icons-outlined')
            .toggleClass('material-icons');
    });


    $('.circle').mouseenter(function () {
        if (isAnimationActive) {
            return;
        }
        isAnimationActive = true

        $(this).animate({
            width: 130 * 2,
            height: 130 * 2
        }, time)
        $(this).removeClass('not-chosen')
        $('.not-chosen').animate({
            width: 130 / 2,
            height: 130 / 2
        }, time)
    })


    $('.circle').mouseleave(function () {
        $(this).animate({
            width: 130,
            height: 130
        }, time)
        $('.not-chosen').animate({
            width: 130,
            height: 130
        }, time)
        $(this).addClass('not-chosen')

        isAnimationActive = false
    })

});
