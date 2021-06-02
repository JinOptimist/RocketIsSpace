$(document).ready(function () {
    var images = [
        '/image/bank/card-shopp.jpg',
        '/image/bank/card-mocn.jpg',
        '/image/bank/card-x.jpg'
    ];

    carouselModule.initialize(
        '.animate-block',
        images,
        {
            time: 4000
        });
});
