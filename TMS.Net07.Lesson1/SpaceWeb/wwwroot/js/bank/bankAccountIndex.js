$(document).ready(function () {

    var time = 1 * 1000;

    var maxMoneyDigit = 6;

    $('.content-box .button-list .button.show-menu').click(function () {

        var operation = $(this).attr('class').replace('button ', '').replace(' show-menu', '').replace(' noselect', '');

        console.log(operation);

        $('.button-list').toggleClass('hide');

        $(`.${operation}-form.container`).toggleClass('hide');
        //$(`.${operation}-form.container input[type = text]`).focus();

        $(`.${operation}-form.container`).find('input:first').focus();
    })

    $('.container .form .buttons .cancel').click(function () {
        $(this).closest('.container').toggleClass('hide');
        $('.button-list').toggleClass('hide');
        $('.container .form input[type = text], input[type = password]').val('');
    })

    $('.container .form .buttons .make').click(function (env) {

        var currentContainer = $(this).closest('.container');

        var activeAccount = GetActiveAccount();

        if (currentContainer.attr('class').includes('remove')) {
            var passwordInput = $(this).parent().siblings('input[type = password]');

            var password = passwordInput.val();

            console.log(password);

            var url = `/Account/Remove?id=${activeAccount.id}&password=${password}`

            $.get(url).done(function (answer) {
                if (!answer) {
                    AnimateWrongInput(passwordInput);
                }
                else {
                    window.location = answer;
                }
            })

            env.preventDefault();
        }
        else {
            var amountTextForm = $(this).parent().siblings('.amount');

            var submitButton = $(this);

            var input = amountTextForm.val();

            if (input == '') {
                console.log('empty form');
                AnimateWrongButton(submitButton);
            }
            else {
                var amount = input.replace(',', '.') - 0;

                var url = `/Account/UpdateAmount?id=${activeAccount.id}&amount=${amount}`;

                if (currentContainer.attr('class').includes('withdrawal')) {
                    url = WithdrawalUrl(activeAccount, amount);
                }
                else if (currentContainer.attr('class').includes('transfer')) {
                    url = TransferUrl(activeAccount, amount);
                }

                $.get(url).done(function (answer) {
                    if (answer) {
                        console.log('amount updated');

                        currentContainer.toggleClass('hide');

                        UpdateAmount(activeAccount, amount);

                        $('.button-list').toggleClass('hide');

                        amountTextForm.val('');
                    }
                    else {
                        console.log('something went wrong');
                    }
                })
            }
            env.preventDefault();
        }
    })

    $('.content-box .button-list .button.freeze').click(function () {
        var activeAccount = GetActiveAccount();

        var url = `/Account/FreezeAccount?id=${activeAccount.id}`;

        var complexAccounts = $(`.account-info-container.${activeAccount.index}, .bank-account.${activeAccount.index}`);

        $.get(url).done(function (answer) {
            if (answer) {
                if (activeAccount.isFrozen == "false") {
                    $(this).find('.text').toggleClass('hide');

                    $(complexAccounts).toggleClass('frozen');

                    activeAccount.isFrozen = "true";

                    ChangeFrozenStatus(activeAccount);

                    SetActiveAccount(activeAccount);
                }
                else {
                    $(this).find('.text').toggleClass('hide');

                    $(complexAccounts).toggleClass('frozen');

                    activeAccount.isFrozen = "false";

                    ChangeFrozenStatus(activeAccount);

                    SetActiveAccount(activeAccount);
                }
            }
            else {
                console.log("somwthing went wrong");
            }
        })
    })

    $('.container .form input.amount').keydown(function (e) {

        var pressedKey = e.key;

        var input = $(this).val();

        var isAbleToFill = InputFillabilityCheck(input);

        if (pressedKey == 'Backspace'
            || pressedKey == 'Delete'
            || pressedKey == 'Enter') {
            return;
        }
        else if (pressedKey == '.' || pressedKey == ',') {
            if (input == '') {
                $(this).val(0);
            }
            if (input.includes('.') || input.includes(',')) {
                e.preventDefault();
            }
            else {
                return;
            }
        }
        if (isAbleToFill) {
            if (pressedKey >= 1 || pressedKey <= 9) {
                return;
            }
            else if (pressedKey == 0) {
                if (input == '') {
                    $(this).val(0 + '.');
                }
                else {
                    return;
                }
            }
        }
        e.preventDefault();

        AnimateWrongInput($(this));

        function InputFillabilityCheck(input) {

            var myReg = /[^\d]/g;

            var check = myReg.test(input);

            if (check) {
                var splitedInput = input.split(myReg);

                if (splitedInput[0].length > maxMoneyDigit + 1) {
                    return false;
                }
                else if (splitedInput[1].length > 1) {
                    return false;
                }
                else {
                    return true;
                }
            }

            else if (input.length > maxMoneyDigit) {
                return false;
            }

            else {
                return true;
            }
        }
    })

    $('.container .form input.amount').keyup(function (e) {
        AnimateInputBackToDefault($(this));
    })

    function AnimateWrongInput(obj) {
        obj.animate(
            {
                'progress': 100
            },
            {
                duration: time / 2,
                step: function (progress) {
                    obj.css('border-bottom', '2px solid red')
                },
                complete: function () {
                    obj.css('progress', 0);
                    AnimateInputBackToDefault(obj);
                },
                queue: false
            }
        )
    }

    function AnimateInputBackToDefault(obj) {
        obj.animate(
            {
                'progress': 100
            },
            {
                duration: time / 2,
                step: function (progress) {
                    obj.css('border-bottom', '')
                },
                complete: function () {
                    obj.css('progress', 0);
                },
                queue: true
            }
        )
    }

    function AnimateWrongButton(obj) {
        obj.animate(
            {
                'progress': 100
            },
            {
                duration: time / 4,
                step: function (progress) {
                    obj.css('background-color', 'red')

                },
                complete: function () {
                    obj.css('progress', 0);
                    AnimateButtonBackToDefault(obj);
                },
                queue: false
            }
        )
    }

    function AnimateButtonBackToDefault(obj) {
        obj.animate(
            {
                'progress': 100
            },
            {
                duration: time / 4,
                step: function (progress) {
                    obj.css('background-color', '')
                },
                complete: function () {
                    obj.css('progress', 0);
                },
                queue: true
            }
        )
    }

    function UpdateAmount(activeAccount, amount) {
        var oldAmount = $(`.account-info-container.${activeAccount.index} .info .amount`)
            .text().replace(',', '.') - 0;
        console.log("UpdateAmount");
        FullAmountAnimation(activeAccount, oldAmount, amount);
    }

    function FullAmountAnimation(activeAccount, oldAmount, amount) {
        console.log("FullAmountAnimation");
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
                    //queue: true,
                    complete: function () {
                        AnimateAmount(activeAccount, oldAmount, amount)
                    }
                });
    }

    function AnimateAmount(activeAccount, oldAmount, amount) {
        console.log("AnimateAmount");
        var obj = $('.account-carousel');

        obj.animate(
            {
                'progress': 100
            },
            {
                duration: time * 2,
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
                    $('.account-carousel').css('progress', 0);
                    console.log("AnimateAmount complete");
                    HideAmount(activeAccount);
                },
                //queue: true
            })
    }

    function HideAmount(activeAccount) {
        console.log("HideAmount");
        var obj = $(`.account-info-container.${activeAccount.index} .info .changing`);

            obj.animate(
                {
                    'opacity': 0
                },
                {
                    duration: time / 2,
                    //queue: true,
                    complete: function () {
                        $(this).toggleClass('hide');
                    }
                });
    }

    function DepositUrl(activeAccount, amount) {

        var url = `/Account/UpdateAmount?id=${activeAccount.id}&amount=${amount}`;

        return url;
    }

    function WithdrawalUrl(activeAccount, amount) {

        amount = amount * (-1);

        return DepositUrl(activeAccount, amount)
    }

    function TransferUrl(activeAccount, amount) {

        var toAccountNumber = $('.transfer.form .to-account-number').val()

        var url = `/Account/Transfer?fromAccountId=${activeAccount.id}&toAccountNumber=${toAccountNumber}&transferAmount=${amount}`;

        return url;
    }
});
