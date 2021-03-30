$(document).ready(function () {

    $('.button-login, .login-button-for-start-page').click(function () {
        $('.bank-popup').removeClass('hide');
    });

    $('.link.register, .link.enter').click(function () {
        $('.additional, .bank-submit, .annotation').toggleClass('hide');
    }
    );

    $('.bank-popup .bank-cover').click(function () {
        $('.bank-popup').addClass('hide');
    });

    $('.add-new').click(function () {
        $('.popup').removeClass('hide');
    }
    );

    $('.bank-account-cancel, .bank-account-submit').click(function () {
        $('.popup').addClass('hide');
    }
    );

    $('.bank-account-submit').click(function (e) {
        var currency = $('.popup-content .currency-input').val();
        var amount = $('.popup-content .amount-input').val();
        var copy = $('.bank-account.template').clone();
        copy
            .find('.amount')
            .text(amount);
        copy
            .find('.currency')
            .text(currency);

        copy.removeClass('template');

        //copy.find('.icon.close').click(removeRocket);

        copy.insertBefore('.add-new.background');
        //$('.bank-account-list').append(copy);
        $('.popup-content .currency-input').val('BYN');
        $('.popup-content .amount-input').val('');
        e.preventDefault();
    });
    

});