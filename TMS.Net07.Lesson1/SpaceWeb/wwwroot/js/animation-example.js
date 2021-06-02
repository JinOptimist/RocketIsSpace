$(document).ready(function () {
    var isAnimationActive = false;
    var isSliderHovered = false;
    var time = 2000;
    var images = [
        '/image/bank/card-shopp.jpg',
        '/image/bank/card-mocn.jpg',
        '/image/bank/card-x.jpg'
    ];
    var currentImageIndex = 0;

    var animationFunctions = [];

    setInterval(checkAnimation, 100);

    setInterval(function () {
        animationFunctions.push(function () {
            runAnimation(true);
        });
    }, 3000);

    function checkAnimation() {
        if (isAnimationActive) {
            return;
        }

        if (isSliderHovered) {
            return;
        }

        var expectedMinTime =  2000 / animationFunctions.length;
        if (expectedMinTime < time) {
            time = expectedMinTime;
        }
        if (animationFunctions.length == 0) {
            time = 2000;
        }

        if (animationFunctions.length > 0) {
            var elem = animationFunctions.splice(0, 1);
            elem[0]();
        }
    }

    $('.animate-block .next-btn').click(function () {
        animationFunctions.push(function () {
            runAnimation(true);
        });
    });

    $('.animate-block .prev-btn').click(function () {
        animationFunctions.push(function () {
            runAnimation(false);
        });
    });

    $('.animate-block .choose').hover(
        function () {
            isSliderHovered = true;
        },
        function () {
            isSliderHovered = false;
        }
    );

    function runAnimation(goForward) {
        isAnimationActive = true;

        if (goForward) {
            currentImageIndex++;
        } else {
            currentImageIndex--;
        }

        var block = goForward ? 'next-image' : 'prev-image';
        $(`.animate-block .${block}`).animate({
            width: 500
        }, time);

        $('.animate-block .current-image').animate({
            width: 0
        }, time, 'swing', goInitState);
        console.log('Run anim. cu Index = ' + currentImageIndex);
    }

    function goInitState() {
        currentImageIndex = calcIndex(currentImageIndex);
        var nextIndex = calcIndex(currentImageIndex + 1);
        var prevIndex = calcIndex(currentImageIndex - 1);

        console.log('init state . cu Index = ' + currentImageIndex);

        var currentImage = images[currentImageIndex];
        var prevImage = images[prevIndex];
        var nextImage = images[nextIndex];

        $('.animate-block .current-image img').attr('src', currentImage);
        $('.animate-block .next-image img').attr('src', nextImage);
        $('.animate-block .prev-image img').attr('src', prevImage);

        $('.animate-block .next-image').css({
            width: 0
        });
        $('.animate-block .prev-image').css({
            width: 0
        });
        $('.animate-block .current-image').css({
            width: 500
        });

        isAnimationActive = false;
    }

    function calcIndex(index) {
        if (index >= images.length) {
            index = 0;
        }
        if (index < 0) {
            index = images.length - 1;
        }
        return index;
    }
});
