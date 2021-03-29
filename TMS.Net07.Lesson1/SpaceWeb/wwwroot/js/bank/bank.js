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

    

});