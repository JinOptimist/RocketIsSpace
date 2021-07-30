$(document).ready(function () {
    $('.registration-but').click(function () {
        $('.phone-number-reg-popup-cover').removeClass('hide');
        $('.phone-number-reg').removeClass('hide');
        $('.reg-phone-input').focus();
    });

    $('.phone-number-reg-popup-cover').click(function () {
        $('.phone-number-reg-popup-cover').addClass('hide');
        $('.phone-number-reg').addClass('hide');
    });

    $('.reg-phone-input').keyup(function () {
        if ($('.reg-phone-input').val().length >= 11) {
            $('.confirm-number').attr("disabled", false);
            $('.confirm-number').addClass('confirm-number-open');
        }
        else {
            $('.confirm-number').attr("disabled", true);
            $('.confirm-number').removeClass('confirm-number-open');
        }
    })

    $('.confirm-number').click(function () {
        $('.spinner-reg').removeClass('hide');

        //var login = $('.inputFields.login-for-reg').val();
        var phone = $('.reg-phone-input').val();
        var generatedCode = '';

        var url = '/User/SendingSmsCode?phone=' + phone;
        $.get(url).done(function (generatedCodeAnswer) {
            console.log(generatedCodeAnswer);
            generatedCode = generatedCodeAnswer;
        });

        setTimeout(test, 4000);

        function test() {
            $('.spinner-reg').addClass('hide');



            var arr = ['.code-input1', '.code-input2', '.code-input3', '.code-input4'];
            $('.confirmation-reg-popup-cover').removeClass('hide');
            $('.confirmation-reg').removeClass('hide');
            $('.code-input1').focus();
            for (var i = 0; i < 4; i++) {
                $(`${arr[i]}`).keyup(function () {
                    this.value = this.value.replace(/[^0-9]/g, '');
                    $(this).next().focus();
                    if (IsFilledInputWithCodeFromSms(generatedCode) == true) {
                        $('.confirmation-code-btn').attr("disabled", false);
                        //$('.confirmation-code-btn').addClass('registration-btn-open');
                        $('.confirmation-code-btn').removeClass('hide');
                    }
                    else {
                        $('.confirmation-code-btn').attr("disabled", true);
                        //$('.confirmation-code-btn').removeClass('registration-btn-open');
                        $('.confirmation-code-btn').addClass('hide');
                    }
                })
            }

            function IsFilledInputWithCodeFromSms(generatedCode) {
                var counter = 0;
                var code = '';
                for (var k = 0; k < 4; k++) {
                    if ($(`${arr[k]}`).val().length == 1) {
                        counter++;
                        code = code + $(`${arr[k]}`).val();
                    }
                }

                return (counter == 4 && generatedCode == code) ?? false;
            }
        }
    })

    $('.confirmation-code-btn').click(function () {
        $('.reg-btn .pglogbut').trigger('click');
    })

    $('.confirmation-reg-popup-cover').click(function () {
        $('.confirmation-reg-popup-cover').addClass('hide');
        $('.confirmation-reg').addClass('hide');
    })

    $('.log-in').click(function () {
        $('.login-for-reg').focus();

        var inputsWithInfo = [
            '.login-for-reg',
            '.password-for-reg'];

        $('.for-reg').keyup(function () {
            if ((IsFilledInputWithRegInfo() == true)/* && ($('#for-json-result').html() == '')*/) {
                $('.registration-btn').attr("disabled", false);
                $('.registration-btn').addClass('pglogbut');
            }
            else {
                $('.registration-btn').attr("disabled", true);
                $('.registration-btn').removeClass('pglogbut');
            }
        })

        //$(window).keydown(function (event) {
        //    if (event.keyCode == 13) {
        //        event.preventDefault();
        //        return false;
        //    }
        //});

        function IsFilledInputWithRegInfo() {
            var counter = 0;
            for (var k = 0; k < 2; k++) {
                if ($(`${inputsWithInfo[k]}`).val().length != 0) {
                    counter++;
                }
            }

            return counter == 2 ?? false;
        }
    })

    //$('[name=Login]').change(function () {
    //    var name = $(this).val();

    //    var url = '/User/IsUserExist?name=' + name;

    //    showIcon('spinner');
    //    $.get(url).done(function (answer) {
    //        //Когда придёт ответ
    //        console.log("answer = " + answer);
    //        if (answer) {
    //            showIcon('close');
    //            $('#registration-block .icon.close').text("Пользователь уже есть");
    //        } else {
    //            showIcon('ok');
    //        }
    //    });

    //    function showIcon(iconName) {
    //        $('#registration-block .icon').addClass('hide');
    //        $(`#registration-block .icon.${iconName}`).removeClass('hide');
    //    }
    //});

});