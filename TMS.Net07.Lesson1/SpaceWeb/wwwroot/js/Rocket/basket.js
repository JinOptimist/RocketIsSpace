$(document).ready(function () {
    $('[name=account]').change(function () {
        var self=$(this);
        var accountNumber = $(this).val();
        var amount= $(this).closest('.order-rocket').find('[name=Price]').val();
        var url = '/RocketShop/PayAbilityCheck?accountNumber=' + accountNumber+'&amount='+amount;

        $.get(url)
            .done(function (answer) {
                self.closest('.dropdownblock').find('.icon1').addClass('hideicon')
                self.closest('.dropdownblock').find('.icon2').addClass('hideicon')
                if (answer) {                    
                    self.closest('.dropdownblock').find('.icon2').removeClass('hideicon')
                }
                else {                    
                    self.closest('.dropdownblock').find('.icon1').removeClass('hideicon')
                }
            })
            .fail(function () {

            });
    });
    
});