$(document).ready(function () {
    var images = [];
    $.get('/Home/ImageForCarousel')
        .done(function (answer) {
            for (i = 0; i < answer.length; i++) {
                var startIndex = answer[i].indexOf('\\image\\carousel\\');
                images[i] = answer[i].slice(startIndex).replace(/\\/g, '/');
            }
            carouselModule.initialize(
                '.animate-block',
                '.animate-block-dots',
                images,
                {
                    time: 2000
                });
        })
        .fail(function () {

        });

    
});
