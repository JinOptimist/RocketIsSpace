var FlipFLopModule = (function () {

    var selector;
    var image;

    function initialize(_selector, _image) {
        selector = _selector;
        image = _image;
        $(`${selector}`).addClass('flip-flop');
        $('.flip-flop').click(flip);
        console.log(selector);
        console.log('init');
    }

    function flip() {
        //$(`${selector}`).animate({
        //    transform: 100
        //}, 2000);
        console.log('click');
    }

    return {
        initialize: initialize
    };

})();