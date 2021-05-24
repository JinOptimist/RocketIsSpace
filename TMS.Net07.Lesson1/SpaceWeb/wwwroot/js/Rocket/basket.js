$(document).ready(function () {
    $('[name=account]').change(function () {
        var accountNumber = $(this).val();
        var amount= $(this).parent('div').parent('div').find('.amount-js').text();
        var url = '/RocketShop/PayAbilityCheck?accountId=' + accountNumber+'&amount='+amount;

        $.get(url)
            .done(function (answer) {
                if (answer) {
                    $(this).parent('div').parent('div').addClass('can-pay')
                }
                else{
                    $(this).parent('div').parent('div').addClass('cant-pay')
                }
            })
            .fail(function () {

            });
    });
    
});