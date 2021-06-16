var time = 1 * 1000;

$(document).ready(function () {

    var time = 1 * 1000;

    $('.button.deposit').click(function () {

        $('.button-list').toggleClass('hide');

        $('.deposit-form.container').toggleClass('hide');
    })

    $('.cancel-deposit.button').click(function (env) {

        $('.deposit-form.container').toggleClass('hide');

        $('.button-list').toggleClass('hide');

        env.preventDefault();
    })

    $('.make-deposit.button').click(function (env) {

        $('.deposit-form.container').toggleClass('hide');

        $('.button-list').toggleClass('hide');

        env.preventDefault();
    })
});
