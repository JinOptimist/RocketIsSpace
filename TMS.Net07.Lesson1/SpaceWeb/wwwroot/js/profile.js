$(document).ready(function () {
    var images = ['/image/avatars/1.jpg', '/image/avatars/10002.jpg', '/image/avatars/2.jpg'];
    carouselModule.initialize('#avatar-carousel', images, {
        width: 1000,
        height: 500,
    });

    $('.currency-option .text').click(function () {
        var self = this;
        var currencyInput = $(self).parent('label').find('input');
        var currency = currencyInput.val();
        var url = '/User/UpdateFavCurrency?currency=' + currency;

        $.get(url)
            .done(function (answer) {
                if (answer) {
                    $('.currency-block label').removeClass('active');
                    currencyInput.parent('label').addClass('active');
                }
            })
            .fail(function () {

            });
    });

    $('[name=CurrentLang]').change(function () {
        var lang = $(this).val();
        var url = '/User/UpdateLang?Lang=' + lang;
        $.get(url);
    });
});