$(document).ready(function () {
    $('.currency-option .text').click(function () {
        var currencyInput = $(this).parent('label').find('input');
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

        var urlChangingAllMoney = '/User/UpdateAllMoney?currency=' + currency;

        $.get(urlChangingAllMoney)
            .done(function (answerFromUpdatingAllMoney) {
                if (answerFromUpdatingAllMoney) {
                    $('.money-count').text(answerFromUpdatingAllMoney.count);
                    $('.default-currency').text(answerFromUpdatingAllMoney.currency);
                }
            })
    });

    $('[name=CurrentLang]').change(function () {
        var lang = $(this).val();
        var url = '/User/UpdateLang?Lang=' + lang;
        $.get(url);
    });
});