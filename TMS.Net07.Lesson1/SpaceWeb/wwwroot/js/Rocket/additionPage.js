$(document).ready(function () {

    var isAnimationActive = false;
    var ismouseleave = false;
    var time = 300;

    $('.circle').hover(function () {
        if (isAnimationActive) {
            return;
        }
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

        //isAnimationActive = false
    })


    $('.circle').mouseleave(function () {
        //if (isAnimationActive) {
        //    return;
        //}
        //isAnimationActive = true
        if (ismouseleave) {
            return;
        }
        ismouseleave = true
        
        $(this).animate({
            width: 130,
            height: 130
        }, time)
        $('.not-chosen').animate({
            width: 130,
            height: 130
        }, time, '', function () { isAnimationActive = false, ismouseleave = false})
        $(this).addClass('not-chosen')

    })

});
