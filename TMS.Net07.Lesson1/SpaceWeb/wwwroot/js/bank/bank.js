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

    //$('.add-new').click(function () {
    //    $('.popup.add').removeClass('hide');
    //}
    //);

    //$('.bank-account-cancel, .bank-account-submit').click(function () {
    //    $('.popup.add').addClass('hide');
    //}
    //);

    //$('.menu.top .bank-account').click(function () {
    //    $('.popup.remove').removeClass('hide');
    //    var accountNumber = $(this).find('.account-number').text();
    //    $('.account-remove-number').val(accountNumber);
    //}
    //);
    //$('.account-remove-cancel, .account-remove-submit').click(function () {
    //    $('.popup.remove').addClass('hide');
    //});

    $('.popup .cover').click(function () {
        $(this).parent().addClass('hide');
    });
});