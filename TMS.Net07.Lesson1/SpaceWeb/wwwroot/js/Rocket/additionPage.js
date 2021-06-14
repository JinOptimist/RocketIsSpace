$(document).ready(function () {

    var time = 300;

    $('.circle').mouseenter(function () {
        if ($('.isActive').length > 0) {
            return;
        }
        $(this).addClass('isActive');

        $(this).animate({
            width: 130 * 2,
            height: 130 * 2
        }, time);
        $(this).removeClass('not-chosen');
        $('.not-chosen').animate({
            width: 130 / 2,
            height: 130 / 2
        }, time);
    });

    function exit() {
        $('.isActive').addClass('not-chosen');
        $('.isActive').removeClass('isActive');
    }

    $('.circle').mouseleave(function () {
        if (!$(this).hasClass('isActive')) {
            return;
        }

        $(this).animate({
            width: 130,
            height: 130
        }, time);
        $('.not-chosen').animate({
            width: 130,
            height: 130
        }, time, '', exit);
        
    })

});
