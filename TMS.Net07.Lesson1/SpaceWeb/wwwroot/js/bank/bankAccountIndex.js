var time = 1 * 1000;

$(document).ready(function () {

    var time = 1 * 1000;


    $('.content-box .button-list .button.show-menu').click(function () {

        var operation = $(this).attr('class').replace('button ', '').replace(' show-menu', '');

        console.log(operation);

        $('.button-list').toggleClass('hide');

        $(`.${operation}-form.container`).toggleClass('hide');
    })

    $('.container .form .buttons .cancel').click(function () {
        $(this).closest('.container').toggleClass('hide');
        $('.button-list').toggleClass('hide');
    })


    //$('.button.deposit').click(function () {

    //    $('.button-list').toggleClass('hide');

    //    $('.deposit-form.container').toggleClass('hide');
    //})

    //$('.cancel-deposit.button').click(function (env) {

    //    $('.deposit-form.container').toggleClass('hide');

    //    $('.button-list').toggleClass('hide');

    //    env.preventDefault();
    //})

    //$('.make-deposit.button').click(function (env) {

    //    $('.deposit-form.container').toggleClass('hide');

    //    $('.button-list').toggleClass('hide');

    //    env.preventDefault();
    //})


    //$('.button.withdrawal').click(function () {

    //    $('.button-list').toggleClass('hide');

    //    $('.withdrawal-form.container').toggleClass('hide');
    //})

    //$('.cancel-withdrawal.button').click(function (env) {

    //    $('.withdrawal-form.container').toggleClass('hide');

    //    $('.button-list').toggleClass('hide');

    //    env.preventDefault();
    //})

    //$('.make-withdrawal.button').click(function (env) {

    //    $('.withdrawal-form.container').toggleClass('hide');

    //    $('.button-list').toggleClass('hide');

    //    env.preventDefault();
    //})

});
