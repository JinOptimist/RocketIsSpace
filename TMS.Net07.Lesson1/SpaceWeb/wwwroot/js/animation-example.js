$(document).ready(function () {
    $.get('/Home/ImageForCarousel')
        .done(function (answer) {
            var images = [];
            for (i = 0; i < answer.length; i++) {
                var startIndex = answer[i].indexOf('\\image\\carousel\\');
                images[i] = answer[i].slice(startIndex).replace(/\\/g, '/');
            }
        })
        .fail(function () {

        });

    

    carouselModule.initialize(
        '.animate-block',
        images,
        {
            time: 2000
        });
});
