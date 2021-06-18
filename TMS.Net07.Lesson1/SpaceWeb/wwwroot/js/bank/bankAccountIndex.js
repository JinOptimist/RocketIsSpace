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
        var oldAmount = $(`.account-info-container.${activeAccountIndex} .info .amount`).text().replace(',','.') - 0;

        ShowAmount(activeAccountIndex, oldAmount, amount);

        function AnimateAmount(activeAccountIndex, oldAmount, amount) {

            $('.account-carousel .account-info-container').animate(
                {
                'progress':100
                },
                {
                    duration: time*2,
                    step: function (progress) {

                        var stepNewAmount = oldAmount + amount / 100 * progress;

                        var stepChanging = amount - amount / 100 * progress;

                        $(`.account-info-container.${activeAccountIndex} .info .amount`)
                            .text(stepNewAmount.toFixed(2).replace('.', ','));

                        $(`.bank-account-list .bank-account.${activeAccountIndex} .amount`)
                            .text(stepNewAmount.toFixed(2).replace('.', ','));

                        $(`.account-info-container.${activeAccountIndex} .info .changing`)
                            .text(stepChanging.toFixed(2).replace('.', ','))
                    },
                    complete: function () {
                        $('.account-carousel .account-info-container').css('progress', 0);
                        HideAmount(activeAccountIndex);
                    },
                    queue: true
                })
        }

        function ShowAmount(activeAccountIndex, oldAmount, amount) {
            $(`.account-info-container.${activeAccountIndex} .info .changing`)
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
                            AnimateAmount(activeAccountIndex, oldAmount, amount)
                        }
                    });
        }

        function HideAmount(activeAccountIndex) {
            $(`.account-info-container.${activeAccountIndex} .info .changing`)
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
