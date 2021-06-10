$(document).ready(function () {
    var images = [
        '/image/carousel/ph1.jpg',
        '/image/carousel/ph2.jpg',
        '/image/carousel/ph3.jpg',
        '/image/carousel/ph4.jpg',
        '/image/carousel/ph5.jpg',
        '/image/carousel/ph6.jpg'
    ];

    var val = 1;

    $('.dot').click(function () {
        $('.dot').removeClass('active');
        $(this).addClass('active');
    })

    carouselModule.initialize(
        '.animate-block',
        images,
        {
            time: 2000
        });
});
