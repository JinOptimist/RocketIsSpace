$(document).ready(function () {
    $('[name=account]').change(function () {
        var self = $(this);
        var accountNumber = $(this).val();
        var amount = $(this).closest('.order-rocket').find('[name=Price]').val();
        var currency = $(this).closest('.order-rocket').find('[name=Currency]').val();
        if (currency == "-1") {
            return;
        }
        var url = '/RocketShop/ChangeCurrency?accountNumber=' + accountNumber
            + '&amount=' + amount
            + '&currency=' + currency;

        $.get(url)
            .done(function (answer) {
                var str = answer.money + " " + answer.currency;
                self.closest('.order-rocket').find('.amount-js').text(str);
                $('[name=Currency]').val(answer.currency);
                $('[name=Price]').val(answer.money);
            })
            .fail(function () {

            });
    });
    
});