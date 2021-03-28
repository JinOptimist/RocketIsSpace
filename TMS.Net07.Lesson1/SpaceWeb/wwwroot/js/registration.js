$(document).ready(function () {
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