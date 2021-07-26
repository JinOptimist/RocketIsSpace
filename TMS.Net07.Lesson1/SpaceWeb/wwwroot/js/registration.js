$(document).ready(function () {
    $('.registration-but').click(function () {
        $('.phone-number-reg-popup-cover').removeClass('hide');
        $('.phone-number-reg').removeClass('hide');
    });

    $('.phone-number-reg-popup-cover').click(function () {
        $('.phone-number-reg-popup-cover').addClass('hide');
        $('.phone-number-reg').addClass('hide');
    });

    $('.reg-phone-input').keyup(function () {
        if ($('.reg-phone-input').val().length >= 11) {
            $('.btn-sending-phone').attr("disabled", false);
        }
        else {
            $('.btn-sending-phone').attr("disabled", true);
        }
    })




    $('[name=Login]').change(function () {
        var name = $(this).val();

        var url = '/User/IsUserExist?name=' + name;

        showIcon('spinner');
        $.get(url).done(function (answer) {
            //Когда придёт ответ
            console.log("answer = " + answer);
            if (answer) {
                showIcon('close');
                $('#registration-block .icon.close').text("Пользователь уже есть");
            } else {
                showIcon('ok');
            }
        });

        function showIcon(iconName) {
            $('#registration-block .icon').addClass('hide');
            $(`#registration-block .icon.${iconName}`).removeClass('hide');
        }
    });

});