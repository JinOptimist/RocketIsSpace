$(document).ready(function () {

    var time = 1 * 1000;

    var maxMoneyDigit = 6;

    $('.add-new.background').css({
        'box-shadow': '3px 3px 6px 0px rgb(150, 150, 150)',
        'background-color': 'rgb(190, 190, 190)'
    });

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
})