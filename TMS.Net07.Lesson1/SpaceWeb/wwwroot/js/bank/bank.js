$(document).ready(function () {

    FreezedAccountCheck();

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

    $('.popup .cover').click(function () {
        $(this).parent().addClass('hide');
    });

    function FreezedAccountCheck() {
        var myReg = /\d/;
        if ($('.frozen-status').text() == "true") {
            $(this).siblings(`.freeze-indication`).toggleClass('hide');
        }
    }
});