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

        if (currentContainer.attr('class').indexOf('withdrawal')>=0) {
            amount = amount * (-1);
        }
        else if (currentContainer.attr('class').indexOf('transfer') >= 0) {
            amount = 0;
        }

        var activeAccount = GetActiveAccount();

        var url = `/Account/UpdateAmount?id=${activeAccount.id}&amount=${amount}`;

        $.get(url).done(function (answer) {
            if (answer) {
                console.log('amount updated');

                currentContainer.toggleClass('hide');

                UpdateAmount(activeAccount, amount);

                $('.button-list').toggleClass('hide');
            }
            else {
                console.log('something went wrong');
            }
        })

        env.preventDefault();

    })

    $('.container .form .buttons .remove.make').click(function (env) {

        var activeAccount = GetActiveAccount();

        var url = `/Account/Remove?id=${activeAccount.id}`

        var currentContainer = $(this).closest('.container');

        $.get(url);

            //.done(function (answer) {
            //if (answer) {
            //    console.log('account deleted');

            //    currentContainer.toggleClass('hide');

            //    //AnimateAccountRemoving(activeAccount);

            //    $('.button-list').toggleClass('hide');
            //}
            //else {
            //    console.log('something went wrong');
            //}
    })

    function GetActiveAccount() {

        var obj = {
            index: $('.button-list .active-account.index').val() - 0,
            id: $('.button-list .active-account.id').val() - 0
        }

        return obj;
    }

    function UpdateAmount(activeAccount, amount) {
        var oldAmount = $(`.account-info-container.${activeAccount.index} .info .amount`).text().replace(',', '.') - 0;

        ShowAmount(activeAccount, oldAmount, amount);

        function AnimateAmount(activeAccount, oldAmount, amount) {

            $('.account-carousel .account-info-container').animate(
                {
                'progress':100
                },
                {
                    duration: time*2,
                    step: function (progress) {

                        var stepNewAmount = oldAmount + amount / 100 * progress;

                        var stepChanging = amount - amount / 100 * progress;

                        $(`.account-info-container.${activeAccount.index} .info .amount`)
                            .text(stepNewAmount.toFixed(2).replace('.', ','));

                        $(`.bank-account-list .bank-account.${activeAccount.index} .amount`)
                            .text(stepNewAmount.toFixed(2).replace('.', ','));

                        $(`.account-info-container.${activeAccount.index} .info .changing`)
                            .text(stepChanging.toFixed(2).replace('.', ','))
                    },
                    complete: function () {
                        $('.account-carousel .account-info-container').css('progress', 0);
                        HideAmount(activeAccount);
                    },
                    queue: true
                })
        }

        function ShowAmount(activeAccount, oldAmount, amount) {
            $(`.account-info-container.${activeAccount.index} .info .changing`)
                .text(amount)
                .css('opacity', 0)
                .toggleClass('hide')
                .animate(
                    {
                        'opacity': 1
                    },
                    {
                        duration: time / 2,
                        queue: true,
                        complete: function () {
                            AnimateAmount(activeAccount, oldAmount, amount)
                        }
                    });
        }

        function HideAmount(activeAccount) {
            $(`.account-info-container.${activeAccount.index} .info .changing`)
                .animate(
                    {
                        'opacity': 0
                    },
                    {
                        duration: time / 2,
                        queue: true,
                        complete: function () {
                            $(this).toggleClass('hide');
                        }
                    });
        }
    }
    function Transfer() {

    }
});
