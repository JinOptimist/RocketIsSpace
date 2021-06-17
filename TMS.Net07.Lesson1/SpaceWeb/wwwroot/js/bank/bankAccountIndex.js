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

    $('.container .form .buttons .make').click(function (env) {

        var amount = $(this).parent().siblings('.amount').val().replace(',', '.') - 0;

        var currentContainer = $(this).closest('.container');

        var activeAccountIndex = GetActiveAccountIndex();

        var activeAccountID = GetActiveAccountId();

        var url = `/Account/UpdateAmount?id=${activeAccountID}&amount=${amount}`;

        $.get(url).done(function (answer) {
            if (answer) {
                console.log('amount updated');

                currentContainer.toggleClass('hide');

                UpdateAmount(activeAccountIndex, amount);

                $('.button-list').toggleClass('hide');
            }
            else {
                console.log('something went wrong');
            }
        })

        env.preventDefault();

    })

    function GetActiveAccountId() {
        return $('.button-list .active-account.id').val() - 0;
    }

    function GetActiveAccountIndex() {
        return $('.button-list .active-account.index').val() - 0;
    }

    function UpdateAmount(activeAccountIndex, amount) {
        var oldAmount = $(`.account-info-container.${activeAccountIndex} .info.amount`).text();
        var newAmount = oldAmount + amount;

        $(`.account-info-container.${activeAccountIndex} .info.amount`).text(newAmount);

        $(`.bank-account-list .bank-account.${activeAccountIndex} .amount`).text(newAmount);
    }

});
