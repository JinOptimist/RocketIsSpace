$(document).ready(function () {
    $('[name=account]').change(function () {
        var self=$(this);
        var accountNumber = $(this).val();
        var amount= $(this).closest('.order-rocket').find('[name=Price]').val();
        var url = '/RocketShop/PayAbilityCheck?accountNumber=' + accountNumber+'&amount='+amount;

        $.get(url)
            .done(function (answer) {
                if (answer) {
                    self.closest('.order-rocket').addClass('can-pay')
                }
                else{
                    self.closest('.order-rocket').addClass('cant-pay')
                }
            })
            .fail(function () {

            });
    });
    
});