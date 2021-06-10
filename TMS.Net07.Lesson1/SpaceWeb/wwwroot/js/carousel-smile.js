var carouselModule = (function () {
    var isAnimationActive = false;
    var isSliderHovered = false;
    var time = 2000;
    var defaultTime = 2000;
    var images = [];
    var selector;
    var currentImageIndex = 0;
    var height = 270;
    var width = 500;
    var imgIndex = 0;
    var prevImgIndex = 0;

    var animationFunctions = [];

    function checkAnimation() {
        if (isAnimationActive) {
            return;
        }

        if (isSliderHovered) {
            return;
        }

        var expectedMinTime = defaultTime / animationFunctions.length;
        if (expectedMinTime < time) {
            time = expectedMinTime;
        }
        if (animationFunctions.length == 0) {
            time = defaultTime;
        }

        if (animationFunctions.length > 0) {
            var elem = animationFunctions.splice(0, 1);
            elem[0]();
        }
    }

    function nextBtnClick() {
        animationFunctions.push(function () {
            runAnimation(true);
        });
    }

    function prevBtnClick() {
        animationFunctions.push(function () {
            runAnimation(false);
        });
    }
    function chooseHoverIn() {
        isSliderHovered = true;
    }
    function chooseHoverOut() {
        isSliderHovered = false;
    }

    function runAnimation(goForward) {
        isAnimationActive = true;

        if (goForward) {
            currentImageIndex++;
        } else {
            currentImageIndex--;
        }

        var block = goForward ? 'next-image' : 'prev-image';
        $(`${selector} .${block}`).animate({
            width: width
        }, time);

        $(`${selector} .current-image`).animate({
            width: 0
        }, time, 'swing', goInitState);

        $('.dot').removeClass('active');
        var val = document.getElementById(calcIndex(currentImageIndex) + 1);
        val.className = "dot active";
    }

    function goInitState() {
        currentImageIndex = calcIndex(currentImageIndex);
        var nextIndex = calcIndex(currentImageIndex + 1);
        var prevIndex = calcIndex(currentImageIndex - 1);

        var currentImage = images[currentImageIndex];
        var prevImage = images[prevIndex];
        var nextImage = images[nextIndex];

        $(`${selector} .current-image img`).attr('src', currentImage);
        $(`${selector} .next-image img`).attr('src', nextImage);
        $(`${selector} .prev-image img`).attr('src', prevImage);

        $(`${selector} .next-image`).css({
            width: 0
        });
        $(`${selector} .prev-image`).css({
            width: 0
        });
        $(`${selector} .current-image`).css({
            width: width
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

    function createTags() {
        createButtons();

        $(`${selector}`).append(createWithImg('next-image'));
        var current = createWithImg('current-image');
        current.width(width);
        $(`${selector}`).append(current);
        $(`${selector}`).append(createWithImg('prev-image'));
    }

    function createWithImg(className) {
        var div = $('<div>');
        div.addClass(className);
        var img = $('<img>');
        div.append(img);
        div.height(height);
        return div;
    }

    function createButtons() {
        var buttons = $('<div>');
        buttons.width(width);
        buttons.height(height);
        buttons.addClass('buttons');

        var prevBtn = $('<div>');
        prevBtn.addClass('prev-btn');
        buttons.append(prevBtn);

        var choose = $('<div>');
        choose.addClass('choose');
        buttons.append(choose);

        var next = $('<div>');
        next.addClass('next-btn');
        buttons.append(next);

        $(`${selector}`).append(buttons);
    }

    function initialize(_selector, _images, _options) {
        images = _images;
        selector = _selector;
        if (_options.width) {
            width = _options.width;
        }
        if (_options.height) {
            height = _options.height;
        }
        if (_options.time) {
            defaultTime = _options.time;
            time = _options.time;
        }

        $(`${selector}`).addClass('carousel-smile');
        createTags();

        $(`${selector} .next-btn`).click(nextBtnClick);
        $(`${selector} .prev-btn`).click(prevBtnClick);
        $(`${selector} .choose`).hover(chooseHoverIn, chooseHoverOut);

        goInitState();

        setInterval(checkAnimation, 100);

        if (_options.isAutoMove) {
            setInterval(function () {
                animationFunctions.push(function () {
                    runAnimation(true);
                });
            }, 3000);
        }

        $('.dot').click(function () {
            prevImgIndex = $('.active').attr('id');
            $('.dot').removeClass('active');
            $(this).addClass('active');
            imgIndex = $(this).attr('id');
            picChange(imgIndex, prevImgIndex);
        })                          
    }

    function picChange(cur, prev) {
        if (prev < cur) {
            var steps = cur - prev;
            for (var i = 1; i <= steps; i++) {
                nextBtnClick();
            }
        }
        else {
            var steps = prev - cur;
            for (var i = 1; i <= steps; i++) {
                prevBtnClick();
            }
        }   
    }

    return {
        initialize: initialize
    };
})();