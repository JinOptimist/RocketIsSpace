$(document).ready(function () {
    var animationTime = 2000;
    $('.add-nice').click(function () {
        var basketPostion = $('.basket').position();

        $('[name=RocketIds]:checked').each(function (index, elem) {
            var parent = $(elem)
                .closest('.rocket');
            var copy = parent
                .clone();
            copy.addClass('fly');
            $('.items-shop').append(copy);
            var parentPostion = parent.position();
            copy.css('top', parentPostion.top);
            copy.css('left', parentPostion.left);

            copy.animate(
                {
                    top: basketPostion.top,
                    left: basketPostion.left,
                    opacity: 0.5,
                    width: 20,
                    height: 20,
                    margin: 0 
                },
                animationTime,
                'swing',
                function () {
                    removeCopy(copy);
                });
        });
    });



    function removeCopy(elem) {
        elem.remove();
        var count = $('.basket-count').text() - 0;
        $('.basket-count').text(count + 1);
    }
});