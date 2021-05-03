$(document).ready(function () {
    $('.update-currency-btn').click(function () {
        var currencyInput = $('[name=currency]:checked');
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
});